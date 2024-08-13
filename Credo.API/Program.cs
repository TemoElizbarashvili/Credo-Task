using Credo.API.Helpers;
using Credo.API.Middleware;
using Credo.API.Modules.Auth.Validatos;
using Credo.Application.Modules.User.Handlers;
using Credo.Domain.Aggregates;
using Credo.Domain.RepositoriesContracts;
using Credo.Domain.Services;
using Credo.Infrastructure.DB;
using Credo.Infrastructure.Messaging;
using Credo.Infrastructure.Repositories;
using Credo.Infrastructure.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// Configure Authentication and Authorization
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
    };
});
builder.Services.AddAuthorization();

// Configure Mediator, Validators, AutoMapper
builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(GetUserByIdQueryHandler))!);
    cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure RabbitMQ services
builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMqConfiguration"));
builder.Services.AddSingleton<IMessageQueueService>(sp =>
{
    var config = sp.GetRequiredService<IOptions<RabbitMqConfiguration>>().Value;
    return new RabbitMQService(config);
});
builder.Services.AddSingleton<IHostedService, OutboxPublisherService>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<OutboxPublisherService>());
builder.Services.AddHostedService<OutboxConsumerService>();

// Register other services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<LoanApplicationsService>();
builder.Services.AddScoped<IOutboxRepository, OutboxRepository>();
builder.Services.AddScoped<LoanApplicationAggregate>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();

// Configure MVC and Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    x.OperationFilter<SecurityRequirementsOperationFilter>();
    x.DescribeAllParametersInCamelCase();
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "CREDO API", Version = "v1" });
    x.SchemaFilter<EnumSchema>();
});

var app = builder.Build();

// Configure middleware
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Allow any CORS policy
app.UseCors(policy =>
{
    policy.WithOrigins();
    policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.MapControllers();
app.Run();
