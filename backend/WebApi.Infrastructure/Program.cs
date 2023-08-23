using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Services.Abstractions.ServiceAbractions;
using WebApi.Business.src.Services.ServicesImplementations;
using WebApi.Business.src.Shared;
using WebApi.Infrastructure.src.Database;
using WebApi.Domain.src.Abstractions;
using WebApi.Infrastructure.src.Middleware;
using WebApi.Infrastructure.src.Repositories.Implementation;
using WebApiDemo.Business.src.Implementations;
using WebApi.Infrastructure.src.Repositories;
// Build
var builder = WebApplication.CreateBuilder(args);

// Add database into the application
builder.Services.AddDbContext<DatabaseContext>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>()
.AddScoped<IUserRepo, UserRepo>()
.AddScoped<IUserService, UserService>()
.AddScoped<IAuthService, AuthService>()
.AddScoped<ILoanService,LoanService>()
.AddScoped<ILoanRepository,LoanRepository>()
.AddScoped<IReviewService,ReviewService>()
.AddScoped<IReviewRepository,ReviewRepository>();


// Add Automapper DI
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryManagement", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
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
                Array.Empty<string>()
            },
    });
});

builder.Services
.AddSingleton<ErrorHandlerMiddleware>();

//Config route
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

// Config the authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    // string jwtConfig = builder.Configuration.GetSection("JwtConfig:JwtKey").Value!;
    // string issuer = builder.Configuration.GetSection("JwtConfig:JwtIssuer").Value!;
    // string audience = builder.Configuration.GetSection("JwtConfig:JwtAudience").Value!;
    string jwtConfig = "my-secrete-key-jsdguyfsdgcjsdbchjsdb";
    string issuer = "libraryManagement";
    string audience = "resource";
    var key = Encoding.ASCII.GetBytes(jwtConfig);

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        RequireExpirationTime = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        ClockSkew = TimeSpan.Zero
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseSwagger();
// app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
