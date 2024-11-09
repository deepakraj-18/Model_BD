using Microsoft.EntityFrameworkCore;
using Model_BD.BAL.IService;
using Model_BD.BAL.Service;
using Model_BD.DAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<spamanagementContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IAgentService, AgentService>();
builder.Services.AddTransient<IModelService, ModelService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    if (app.Environment.IsDevelopment())
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Model API"); // For Dev
    }
    else
    {
        c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Model API"); // For Prod
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
