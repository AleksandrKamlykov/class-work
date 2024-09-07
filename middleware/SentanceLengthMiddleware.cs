namespace class_work.middleware
{
    public class SentanceLengthMiddleware
    {
        readonly RequestDelegate next;
        public SentanceLengthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string? value = context.Request.Query["value"];
            if (value != null)
            {
                await context.Response.WriteAsync($"Value length: {value.Length}");
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }
    }
}
