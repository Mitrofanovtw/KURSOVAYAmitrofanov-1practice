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
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JwtConfiguration>(
                builder.Configuration.GetSection("Jwt"));
        }
    }
}
 
