var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IPersona>(provider => new Persona("jonathan", 19));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder => builder.WithOrigins("http://localhost:4200") // Cambia por tu URL si es diferente
                              .AllowAnyMethod()
                              .AllowAnyHeader());
    });

    services.AddControllers();
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseCors("AllowSpecificOrigin");
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}

public class Program1
{
    public void Main(){
        
    }
}