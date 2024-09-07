using class_work.middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<ToHTMLMiddleware>();

app.UseMiddleware<NumberDeterminantMiddleware>();
//app.UseMiddleware<SentanceLengthMiddleware>();
//app.UseMiddleware<AuthMiddleware>();
//app.UseMiddleware<RoutingMiddleware>();

//app.Environment.EnvironmentName = Environment.Ololo.ToString();

//if (app.Environment.IsDevelopment())
//{
//    app.Run(async context =>
//    {
//        await context.Response.WriteAsync("Test");
//    });
//}else if (app.Environment.IsEnvironment(Environment.Ololo.ToString()))
//{
//    app.Run(async context =>
//    {
//        await context.Response.WriteAsync("Ololo");
//    });
//}
//else
//{
//    app.Run(async context =>
//    {
//        await context.Response.WriteAsync("Prod");
//    });
//}
app.Run();

public enum Environment
{
    Development,
    Production,
    Ololo
}