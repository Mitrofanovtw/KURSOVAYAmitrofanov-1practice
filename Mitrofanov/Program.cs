using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudioStatistic.Models;
using StudioStatistic.Repositories;
using StudioStatistic.Services;
using System.Text;
using AutoMapper;


namespace StudioStatistic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<APIDBContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IRequestRepository, RequestRepository>();
            builder.Services.AddScoped<IEngineersRepository, EngineersRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<APIDBContext>();
                db.Database.Migrate();

                if (!db.Clients.Any())
                {
                    var clients = new[]
                    {
            new Client { Name = "Иван Иванов", QuantityOfVisits = 3 },
            new Client { Name = "Пётр Петров", QuantityOfVisits = 1 },
            new Client { Name = "Мария Сидорова", QuantityOfVisits = 5 },
            new Client { Name = "Алексей Кузнецов", QuantityOfVisits = 2 },
            new Client { Name = "Елена Морозова", QuantityOfVisits = 4 }
        };
                    db.Clients.AddRange(clients);

                    var engineers = new[]
                    {
            new Engineers { Name = "Сергей", Adress = "ул. Ленина, 10", WorkExp = "5 лет" },
            new Engineers { Name = "Дмитрий", Adress = "пр. Мира, 25", WorkExp = "8 лет" },
            new Engineers { Name = "Ольга", Adress = "ул. Пушкина, 5", WorkExp = "3 года" },
            new Engineers { Name = "Андрей", Adress = "ул. Гагарина, 12", WorkExp = "10 лет" },
            new Engineers { Name = "Татьяна", Adress = "ул. Чехова, 8", WorkExp = "6 лет" }
        };
                    db.Engineers.AddRange(engineers);

                    var services = new[]
                    {
            new Service { Name = "Запись трека", Price = 5000 },
            new Service { Name = "Сведение", Price = 3000 },
            new Service { Name = "Мастеринг", Price = 2000 },
            new Service { Name = "Аранжировка", Price = 7000 },
            new Service { Name = "Консультация", Price = 1000 }
        };
                    db.Services.AddRange(services);

                    var admins = new[]
                    {
            new Admin { Name = "Admin1" },
            new Admin { Name = "Admin2" },
            new Admin { Name = "Admin3" },
            new Admin { Name = "Admin4" },
            new Admin { Name = "Admin5" }
        };
                    db.Admins.AddRange(admins);

                    db.SaveChanges();

                    var requests = new[]
                    {
            new Request { ClientId = clients[0].Id, EngineerId = engineers[0].Id, ServiceId = services[0].Id, Cost = 5000, DateOfVisit = DateTime.Today.AddDays(1), Description = "Запись вокала" },
            new Request { ClientId = clients[1].Id, EngineerId = engineers[1].Id, ServiceId = services[1].Id, Cost = 3000, DateOfVisit = DateTime.Today.AddDays(2) },
            new Request { ClientId = clients[2].Id, EngineerId = engineers[2].Id, ServiceId = services[2].Id, Cost = 2000, DateOfVisit = DateTime.Today.AddDays(3) },
            new Request { ClientId = clients[3].Id, EngineerId = engineers[3].Id, ServiceId = services[3].Id, Cost = 7000, DateOfVisit = DateTime.Today.AddDays(4) },
            new Request { ClientId = clients[4].Id, EngineerId = engineers[4].Id, ServiceId = services[4].Id, Cost = 1000, DateOfVisit = DateTime.Today.AddDays(5) }
        };
                    db.Requests.AddRange(requests);

                    db.SaveChanges();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();


            app.Run();
        }
    }
}
