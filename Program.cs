using class_work.middleware;
using class_work.Context;
using class_work.Models;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

var dbContext = app.Services.GetService<DataBaseContext>();

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

app.MapGet("/api/products", async (context) =>
{
    var products = await dbContext.Products.ToListAsync();
    await context.Response.WriteAsJsonAsync(products);

});

app.MapPost("/api/products", async (context) =>
{
    var product = await context.Request.ReadFromJsonAsync<Product>();
    await dbContext.Products.AddAsync(product);
    await dbContext.SaveChangesAsync();
    await context.Response.WriteAsJsonAsync(product);
});

app.MapPut("/api/products/{id}", async (context) =>
{
    var id = context.Request.RouteValues["id"] as string;
    var product = await context.Request.ReadFromJsonAsync<Product>();
    product.Id = Guid.Parse(id);
    dbContext.Products.Update(product);
    await dbContext.SaveChangesAsync();
    await context.Response.WriteAsJsonAsync(product);
});

app.MapDelete("/api/products/{id}", async (context) =>
{
    var id = context.Request.RouteValues["id"] as string;
    var product = await dbContext.Products.FindAsync(Guid.Parse(id));
    dbContext.Products.Remove(product);
    await dbContext.SaveChangesAsync();
    await context.Response.WriteAsJsonAsync(product);

});

app.Run();

public enum Environment
{
    Development,
    Production,
    Ololo
}