namespace class_work.middleware
{
    class TimeMessageMiddleware
    {
        private readonly RequestDelegate next;


        public TimeMessageMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext context, ITimeService timeService)
        {
           if(context.Request.Path == "/time-get")
            {
                await context.Response.WriteAsync($"<h1>Time: {timeService.GetTime()}</h1>");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }

}
