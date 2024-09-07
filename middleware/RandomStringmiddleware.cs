namespace class_work.middleware
{
    public class RandomStringmiddleware
    {
        private readonly RequestDelegate next;

        public RandomStringmiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRandomString randomString)
        {
            
           if(context.Request.Path == "/random")
            {
                await context.Response.WriteAsync($"<h1>Random: {randomString.GetRandomString()}</h1>");
            }
        
            await next.Invoke(context);
        }
    }
}
