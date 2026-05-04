using SkyRouteAPI.Interfaces;
using SkyRouteAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Register application services for dependency injection.
// Singleton is used because these services are stateless and can be shared across the entire application.
// They do not depend on a database context or request-specific data.
builder.Services.AddSingleton<IAirportServices, AirportServices>();
builder.Services.AddSingleton<IAirlineServices, GlobalAirServices>();
builder.Services.AddSingleton<IAirlineServices, BudgetWingsServices>();
builder.Services.AddSingleton<IFlightSearchServices, FlightSearchServices>();


// configura CORS para permitir que tu frontend en React pueda llamar a tu API de .NET. (página web puede llamar a una API que está en otro origen.)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "https://localhost:5173", "http://192.168.1.50:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowReact");
app.UseAuthorization();

app.MapControllers();

app.Run();
