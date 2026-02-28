using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Features.States.Queries.GetAllStates;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;
using TarihWebAPI.Persistence.Context;
using TarihWebAPI.Persistence.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR — ApplicationAndDomain assembly'indeki tüm handler'ları otomatik tarar
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetAllStatesQueryHandler).Assembly));

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// HttpContext — BaseHandler için
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// DB oluştur (migration yoksa EnsureCreated)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tarih Web API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();