using Microsoft.EntityFrameworkCore;
using PayLink.Data;
using PayLink.Models;
using PayLink.Services; 
using DotNetEnv;

//Cargar las variables de entorno del archivo .env
Env.Load(); 

var builder = WebApplication.CreateBuilder(args);

//Leer la cadena de conexión desde las variables de entorno
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

//Acá empezamos la configuracion de los servicios

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddDbContext<PayLinkDbContext>(options =>
    options.UseInMemoryDatabase("PayLinkDB"));*/

builder.Services.AddDbContext<PayLinkDbContext>(options =>
    options.UseSqlServer(connectionString));
    
//Inyección de Dependencias
builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

//Se termina la configuracion de los servicios

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
