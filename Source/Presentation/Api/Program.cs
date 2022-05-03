using ECommerce.Application.Validators.Products;
using ECommerce.Infrastructure.Filters;
using ECommerce.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceService();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
     policy.WithOrigins("http://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
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

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
