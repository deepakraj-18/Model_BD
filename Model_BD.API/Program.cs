using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model_BD.API.Helper;
using Model_BD.BAL.Helpers;
using Model_BD.BAL.IService;
using Model_BD.BAL.Service;
using Model_BD.DAL.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Enable issuer validation for added security
        ValidateAudience = true, // Enable audience validation for added security
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var user = context.Principal;
            var roleClaim = user.FindFirst(claim => claim.Type == "Role")?.Value;
            if (!string.IsNullOrEmpty(roleClaim))
            {
                var roles = roleClaim.Split(',');
                var claims = roles.Select(role => new Claim(ClaimTypes.Role, role.Trim()));
                var identity = user.Identity as ClaimsIdentity;
                identity.AddClaims(claims);
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", b =>
    {
        b.WithOrigins("http://localhost:3000", "http://localhost:3001", "https://localhost:4200")
         .AllowAnyMethod()
         .AllowAnyHeader();
    });
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<spamanagementContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddTransient<Cryptography>();
builder.Services.AddTransient<JWT>();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IAgentService, AgentService>();
builder.Services.AddTransient<IModelService, ModelService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IStatusService, StatusService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Model API");
});
app.UseCors("cors");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
