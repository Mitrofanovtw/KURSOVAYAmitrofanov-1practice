using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudioStatistic;
using StudioStatistic.Middleware;
using StudioStatistic.Models;
using StudioStatistic.Repositories;
using StudioStatistic.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudioStatistic API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<APIDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IEngineersRepository, EngineersRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IEngineerService, EngineerService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudioStatistic API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<APIDBContext>();
    db.Database.Migrate();

    if (!db.Users.Any())
    {
        var adminUser = new User
        {
            Username = "superadmin",
            Email = "superadmin@studio.ru",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Super123!"),
            Role = UserRole.Admin
        };
        db.Users.Add(adminUser);
        await db.SaveChangesAsync();

        db.Admins.Add(new Admin
        {
            Id = adminUser.Id,
            Name = "Супер Админ",
            Email = "superadmin@studio.ru",
            PasswordHash = adminUser.PasswordHash
        });

        var engineerUser = new User
        {
            Username = "engineer1",
            Email = "engineer1@studio.ru",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Eng123!"),
            Role = UserRole.Engineer
        };
        db.Users.Add(engineerUser);
        await db.SaveChangesAsync();

        db.Engineers.Add(new Engineers
        {
            Id = engineerUser.Id,
            FirstName = "Алексей",
            LastName = "Звукореж",
            WorkExp = "5 лет",
            AboutHimself = "Люблю тяжёлый рок",
            Adress = "г. Москва, студия 'Звук'"
        });
        await db.SaveChangesAsync();

        var clientUser = new User
        {
            Username = "client1",
            Email = "client1@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Client123!"),
            Role = UserRole.Client
        };
        db.Users.Add(clientUser);
        await db.SaveChangesAsync();

        db.Clients.Add(new Client
        {
            Id = clientUser.Id,
            FirstName = "Иван",
            LastName = "Иванов",
            QuantityOfVisits = 3
        });

        await db.SaveChangesAsync();
    }

    if (!db.Services.Any())
    {
        db.Services.AddRange(
            new Service { Name = "Запись вокала", Price = 5000 },
            new Service { Name = "Сведение", Price = 8000 },
            new Service { Name = "Мастеринг", Price = 3000 }
        );
        await db.SaveChangesAsync();
    }

    if (!db.Requests.Any())
    {
        var clientId = db.Clients.FirstOrDefault()?.Id;
        var engineerId = db.Engineers.FirstOrDefault()?.Id;
        var vocalServiceId = db.Services.FirstOrDefault(s => s.Name == "Запись вокала")?.Id;
        var masteringServiceId = db.Services.FirstOrDefault(s => s.Name == "Мастеринг")?.Id;

        if (clientId != null && engineerId != null && vocalServiceId != null && masteringServiceId != null)
        {
            db.Requests.AddRange(
                new Request
                {
                    ClientId = clientId.Value,
                    EngineerId = engineerId.Value,
                    ServiceId = vocalServiceId.Value,
                    DateOfVisit = DateTime.UtcNow.AddDays(3),
                    Description = "Запись демо-трека"
                },
                new Request
                {
                    ClientId = clientId.Value,
                    EngineerId = engineerId.Value,
                    ServiceId = masteringServiceId.Value,
                    DateOfVisit = DateTime.UtcNow.AddDays(7),
                    Description = "Мастеринг альбома"
                }
            );
            await db.SaveChangesAsync();
        }
    }
}

app.Run();