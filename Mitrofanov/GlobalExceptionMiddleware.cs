using System.Net;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace StudioStatistic.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
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
                _ => new ErrorResponse
                {
                    Error = "Внутренняя ошибка сервера",
                    Message = exception.Message,
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
    }

    public class ErrorResponse
    {
        public string Error { get; set; } = null!;
        public string? Message { get; set; }
        public DateTime? Timestamp { get; set; }
        public int Status { get; set; }
    }
}