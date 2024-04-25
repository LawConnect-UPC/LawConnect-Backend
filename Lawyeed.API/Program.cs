using dotenv.net;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Mapping;
using Lawyeed.API.Lawyeed.Persistence.Contexts;
using Lawyeed.API.Lawyeed.Persistence.Repositories;
using Lawyeed.API.Lawyeed.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DotEnv.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000"; // Fallback al puerto 5000 si PORT no está definido
builder.WebHost.UseUrls($"http://*:{port}");

// Add Database Connection
var connectionString = $"server={Environment.GetEnvironmentVariable("DB_HOST")}; " +
                       $"user={Environment.GetEnvironmentVariable("DB_USER")}; " +
                       $"password={Environment.GetEnvironmentVariable("DB_PASSWORD")}; " +
                       $"database={Environment.GetEnvironmentVariable("DB_NAME")}; " +
                       $"port={Environment.GetEnvironmentVariable("DB_PORT")};";


builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging(false)
        .EnableDetailedErrors());

// Add lower cases routes
builder.Services.AddRouting(
    options => options.LowercaseUrls = true);

// Dependency Injection Configuration
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonLawyerRepository, PersonLawyerRepository>();
builder.Services.AddScoped<IPersonLawyerService, PersonLawyerService>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IPersonPlanRepository, PersonPlanRepository>();
builder.Services.AddScoped<IPersonPlanService, PersonPlanService>();
builder.Services.AddScoped<IConsultRepository, ConsultRepository>();
builder.Services.AddScoped<IConsultService, ConsultService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();