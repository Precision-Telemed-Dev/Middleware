using Precision.API.BAL.CommonServices;
using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.BAL.LabServices;
using Precision.API.BAL.LabServices.Interfaces;
using Precision.API.BAL.PharmacyServices;
using Precision.API.BAL.PharmacyServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommonMethods, CommonMethods>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
