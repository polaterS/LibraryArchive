using Serilog.Context;

namespace LibraryArchive.API.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userName = context.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(userName))
            {
                LogContext.PushProperty("UserName", userName);
            }

            await _next(context);
        }
    }

}
