using Mitrofanov.Controllers;
using Mitrofanov.Models;
using Mitrofanov.Repositories;
using Mitrofanov.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mitrofanov
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TestMiddleware>();
            app.MapControllers();


            app.Run();
        }
    }
}
 
