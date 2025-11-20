using System.Net;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace StudioStatistic.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        /// <summary>
        /// Перехватывает все необработанные исключения и ошибки аутентификации
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                UnauthorizedAccessException => new ErrorResponse
                {
                    Error = "Доступ запрещён",
                    Message = "У вас нет прав для выполнения этого действия",
                    Status = 403
                },
                SecurityTokenException or SecurityTokenExpiredException => new ErrorResponse
                {
                    Error = "Недействительный или истёкший токен",
                    Message = "Токен недействителен или его срок действия истёк",
                    Status = 401
                },
                InvalidOperationException ex when ex.Message.Contains("already exists") => new ErrorResponse
                {
                    Error = "Пользователь уже существует",
                    Message = ex.Message,
                    Status = 400
                },
                DbUpdateException dbEx => new ErrorResponse
                {
                    Error = "Ошибка базы данных",
                    Message = GetDatabaseErrorMessage(dbEx),
                    Details = _environment.IsDevelopment() ? dbEx.ToString() : null,
                    Timestamp = DateTime.UtcNow,
                    Status = 500
                },
                _ => new ErrorResponse
                {
                    Error = "Внутренняя ошибка сервера",
                    Message = GetUserFriendlyMessage(exception),
                    Details = _environment.IsDevelopment() ? exception.ToString() : null,
                    Timestamp = DateTime.UtcNow,
                    Status = 500
                }
            };

            context.Response.StatusCode = response.Status;

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(json);
        }

        private static string GetDatabaseErrorMessage(DbUpdateException dbEx)
        {
            var innerException = dbEx.InnerException;

            if (innerException?.Message != null)
            {
                if (innerException.Message.Contains("23505"))
                    return "Нарушение уникальности: запись с такими данными уже существует";
                if (innerException.Message.Contains("23503"))
                    return "Нарушение внешнего ключа: связанная запись не найдена";
                if (innerException.Message.Contains("23502"))
                    return "Обязательное поле не заполнено";
                if (innerException.Message.Contains("23514"))
                    return "Нарушение проверочного ограничения";

                return innerException.Message;
            }

            return "Произошла ошибка при сохранении данных в базу данных";
        }

        private static string GetUserFriendlyMessage(Exception exception)
        {
            return exception.InnerException?.Message ?? exception.Message;
        }
    }

    public class ErrorResponse
    {
        public string Error { get; set; } = null!;
        public string? Message { get; set; }
        public string? Details { get; set; }
        public DateTime? Timestamp { get; set; }
        public int Status { get; set; }
    }
}