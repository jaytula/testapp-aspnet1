using Newtonsoft.Json;

var port = int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "8080");

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(port);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/Environment", () =>
{
    return new EnvironmentInfo();
});

app.MapPost("/AuthLogin", context =>
{
    var data = new
    {
        Message = "Hello, World!",
        Timestamp = DateTime.UtcNow
    };

    string json = JsonConvert.SerializeObject(data);
    return context.Response.WriteAsJsonAsync(json);
});

app.Run();
