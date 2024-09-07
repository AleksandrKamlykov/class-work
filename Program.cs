using class_work.middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITimeService, TimeService>();
builder.Services.AddSingleton<ICounter, CounterService>();
builder.Services.AddScoped<IRandomString, RandomString>();

var app = builder.Build();

app.UseMiddleware<TimeMessageMiddleware>();
app.UseMiddleware<CounterMiddleware>();
app.UseMiddleware<RandomStringmiddleware>();

app.MapGet("/time-get", async (context) => {

    var timeService = context.RequestServices.GetRequiredService<ITimeService>();
    await context.Response.WriteAsync($"<h1>Time: {timeService.GetTime()}</h1>");
});

app.MapGet("/counter-get", async (context) => {

    var counter = context.RequestServices.GetRequiredService<ICounter>();
    await context.Response.WriteAsync($"<h1>Counter: {counter.Value}</h1>");
});

app.MapGet("/random-get", async (context) => {

    var randomString = context.RequestServices.GetRequiredService<IRandomString>();
    await context.Response.WriteAsync($"<h1>Random: {randomString.GetRandomString()}</h1>");
});


app.Run();

public interface ITimeService
{
    string GetTime();
}

public class TimeService : ITimeService
{

    private readonly DateTime _time = DateTime.Now;

    public string GetTime()
    {
        return _time.ToString("dd.MM hh:mm:ss");
    }
}

//-------------
public interface ICounter
{
    public int Value { get;  }
}
public class CounterService : ICounter
{
    private int _value = 0;
    public int Value { get { 
            _value++;
            return _value;
        }
    }
}
//------------
public interface IRandomString
{
    string GetRandomString();
}
public class RandomString : IRandomString
{
    public string GetRandomString()
    {
        return Guid.NewGuid().ToString();
    }
}