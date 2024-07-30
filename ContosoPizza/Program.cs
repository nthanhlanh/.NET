using System.Text;
using ContosoPizza.Controllers;
using ContosoPizza.Data;
using ContosoPizza.Helprs;
using ContosoPizza.Repositories;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Đăng ký Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
// .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//     });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Pizza", Version = "V2" });
    // Thêm thông tin bảo mật
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    // Nếu bạn muốn sử dụng XML comments, hãy thêm dòng này
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Thêm dịch vụ tùy chỉnh
// Đăng ký các repository
builder.Services.AddScoped<IMyEntityRepository, MyEntityRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IUserGroupRepository, UserGroupRepository>();

// Đăng ký các service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMyEntityService, MyEntityService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IUserGroupService, UserGroupService>();

// Cấu hình JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
#pragma warning disable CS8604 // Possible null reference argument.
var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]); // Replace with your secret key
#pragma warning restore CS8604 // Possible null reference argument.

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddAuthorization();
// Cấu hình Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("D:/Temp/logs/log.txt", rollingInterval: RollingInterval.Day) // Ghi log vào file
    .CreateLogger();

// Thay thế LoggerFactory mặc định bằng Serilog
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();


var app = builder.Build();

// Kích hoạt các dịch vụ tĩnh
// app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
   {
       c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Authentication");
       c.SwaggerEndpoint("/swagger/v2/swagger.json", "API APP");
       c.RoutePrefix = string.Empty; // Đặt Swagger UI ở trang gốc

   });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.MapControllers();

app.UseAuthentication();//Middleware UseMiddleware <TokenMiddleware>()
app.UseAuthorization();
app.UseMiddleware<AuthorizationMiddleware>(); // Đăng ký middleware tùy chỉnh



app.Run();
