using HR.LeaveManagement.API.Middleware;
using HR.LeaveManagement.API.Services;
using HR.LeaveManagement.Application;
using HR.LeaveManagement.Infrastructure;
using HR.LeaveManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add projects' Services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);

// Add API Services
builder.Services.AddScoped<ILeaveTypesService, LeaveTypesService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
                      policy => policy.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// use Middleware
app.UseMiddleware<ExceptionMiddleware>();

// use cors
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();