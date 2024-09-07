namespace class_work.middleware
{
    public class ToHTMLMiddleware
    {
        readonly RequestDelegate next;
        public ToHTMLMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            await next.Invoke(context);

            if (context.Response.ContentType == "text/html")
            {
               

                var num = context.Response.Headers["number"];
                await context.Response.WriteAsync($"</body>You number is: {num} </html>");
            }
            else
            {
                await next.Invoke(context);
            }

         
        }
    }
}
