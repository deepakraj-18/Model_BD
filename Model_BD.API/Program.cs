using Microsoft.EntityFrameworkCore;
using Model_BD.BAL.IService;
using Model_BD.BAL.Service;
using Model_BD.API.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Model_BD.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Claims;


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
            ValidateIssuer = false,
            ValidateAudience = false,
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

                // Find the "Role" claim and split it by commas
                var roleClaim = user.FindFirst(claim => claim.Type == "Role")?.Value;
                if (!string.IsNullOrEmpty(roleClaim))
                {
                    var roles = roleClaim.Split(',');
                    var claims = roles.Select(role => new Claim(ClaimTypes.Role, role.Trim()));

                    // Add individual role claims to the identity
                    var identity = user.Identity as ClaimsIdentity;
                    identity.AddClaims(claims);
                }

                return Task.CompletedTask;
            }
        
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<spamanagementContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDbContext<spamanagementContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("SpaManagement")));


builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IAgentService, AgentService>();
builder.Services.AddTransient<IModelService, ModelService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<Cryptography>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
