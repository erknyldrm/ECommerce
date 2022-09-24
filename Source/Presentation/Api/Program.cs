using ECommerce.Application;
using ECommerce.Application.Validators.Products;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Enums;
using ECommerce.Infrastructure.Filters;
using ECommerce.Infrastructure.Services.Storage.Local;
using ECommerce.Persistence;
using FluentValidation.AspNetCore;
using Api.Extensions;
using ECommerce.SignalR;
using ECommerce.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceService();
builder.Services.AddInfrastructureService();
builder.Services.AddApplicationService();
builder.Services.AddSignalRService();

builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage(StorageType.Local);
//builder.Services.AddStorage(StorageType.Azure);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
     policy.WithOrigins("http://localhost:4200", "http://localhost:4200")
     .AllowAnyHeader()
     .AllowAnyMethod()
     .AllowCredentials()
));

builder.Services.AddControllers(opt => opt.Filters.Add<ValidationFilter>())
                                 .AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())    
                                 .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHubs();

app.Run();
