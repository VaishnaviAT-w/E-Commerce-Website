using E_Commerce_Project.Data.DAL_Data_Access_Layer_.Repository;
using E_Commerce_Website.BI.Mapper;
using E_Commerce_Website.BI.Service;
using E_Commerce_Website.Core.Contract.IRepository;
using E_Commerce_Website.Core.Contract.IService;
using E_Commerce_Website.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection"))
);

builder.Services.AddScoped<IUsersRepo, UserRepo>();   
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserMapper>();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {token}"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
