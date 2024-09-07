using Humanizer;

namespace class_work.middleware
{
    public class NumberDeterminantMiddleware
    {
        readonly RequestDelegate next;
        public NumberDeterminantMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (int.TryParse(context.Request.Query["value"], out int result))
            {
              if(result  >=0 && result <= 10000)
                {
                    context.Response.Headers["number"] = result.ToWords();
                    context.Response.ContentType = "text/html";
                }
                await next.Invoke(context);

                //  await  context.Response.WriteAsync(result.ToWords());
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
