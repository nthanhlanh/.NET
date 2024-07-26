using System.Text;
using ContosoPizza.Data;
using ContosoPizza.Helprs;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
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

// Thêm dịch vụ tùy chỉnh
builder.Services.AddScoped<IMyEntityService, MyEntityService>();
builder.Services.AddScoped<IPizzaService, PizzaService>();

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

app.UseRouting();
app.MapControllers();

app.UseAuthentication();//Middleware UseMiddleware <TokenMiddleware>()
app.UseAuthorization();
app.UseMiddleware<AuthorizationMiddleware>(); // Đăng ký middleware tùy chỉnh



app.Run();
