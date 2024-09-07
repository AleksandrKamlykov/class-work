namespace class_work.middleware
{
    public class CounterMiddleware
    {
        private readonly RequestDelegate next;

        public CounterMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICounter counter)
        {
           if(context.Request.Path == "/counter-get")
            {
                await context.Response.WriteAsync($"<h1>Counter: {counter.Value}</h1>");
            }
            else
            {
                await next.Invoke(context);
            }
            await next.Invoke(context);
        }
    }
}
