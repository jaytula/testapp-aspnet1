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

app.MapPost("/AuthLogin", async context =>
{
    // Read the request body into a string    
    MyData? postData = await context.Request.ReadFromJsonAsync<MyData>();

    var data = new
    {
        PostData = postData,
        Message = "Hello, World!",
        Timestamp = DateTime.UtcNow
    };

    string json = JsonConvert.SerializeObject(data);


    // Set the response status code and content
    context.Response.StatusCode = 200;
    context.Response.ContentType = "application/json";

    // Write the response to the body
    await context.Response.WriteAsync(json);

});

app.Run();


public class MyData
{
    public string? authdata { get; set; }
}