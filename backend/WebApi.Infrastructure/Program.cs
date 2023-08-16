using WebApi.Business.src.Services.Abstractions.RepoAbstractions;
using WebApi.Business.src.Services.Abstractions.ServiceAbractions;
using WebApi.Business.src.Services.ServicesImplementations;
using WebApi.Infrastructure.src.Database;
using WebApi.Infrastructure.src.Repositories.Implementation;
// Build
var builder = WebApplication.CreateBuilder(args);



// Add database into the application
builder.Services.AddDbContext<DatabaseContext>();

// Add Automapper DI
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
