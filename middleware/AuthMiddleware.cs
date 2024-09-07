namespace class_work.middleware
{
    public class AuthMiddleware
    {
        readonly RequestDelegate next;
        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (string.IsNullOrWhiteSpace(token))
            {
                context.Response.StatusCode = 403;
            }
            else
            {
                await next.Invoke(context);
            }
        }

    }
}
