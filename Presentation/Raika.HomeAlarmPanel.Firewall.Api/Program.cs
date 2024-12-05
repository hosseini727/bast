using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Raika.Common.SharedApplicationServices.Behaviours;
using Raika.Common.SharedApplicationServices.Security;
using Raika.Common.SharedApplicationServices.Services;
using Raika.Common.SharedInfrastructure.DateTimeHelper;
using Raika.Common.SharedInfrastructure.Email;
using Raika.Common.SharedInfrastructure.SMS;
using Raika.Common.SharedKernel;
using Raika.Common.SharedKernel.Interfaces;
using Raika.HomeAlarmPanel.ApiBase.Headers;
using Raika.HomeAlarmPanel.ApiBase.Middlewares;
using Raika.HomeAlarmPanel.ApiBase.Services;
using Raika.HomeAlarmPanel.Application;
using Raika.HomeAlarmPanel.Application.Services.StoreServices.Commands.AddStore;
using Raika.HomeAlarmPanel.Infrastructure;
using Raika.HomeAlarmPanel.Infrastructure.DbContexts;
using Serilog;
using System.Globalization;
using System.Net;
using System.Text;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

{
    //
    // Connection Strings                                
    //
    builder.Services.AddDbContext<CommandDbContext>(
        options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("MainDbConnection")
        ).EnableDetailedErrors()
    );

    //
    // Common
    //
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(config =>
    {
        config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });
        config.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] { }
            }
        });
        config.OperationFilter<LocalizationHeaderFilter>();
    });

    //
    // Custome common
    //
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
    builder.Services.AddScoped<IDateTimeHelper, DateTimeHelper>();
    builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
    builder.Services.AddSingleton<ICurrentApplicationService, CurrentApplicationServices>();
    builder.Services.AddSingleton<ISecurityServices, SecurityServices>();
    builder.Services.AddSingleton<ISMSService, SMSService>();
    builder.Services.AddSingleton<IEmailService, EmailService>();
    builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();

    //
    // Projects
    //
    builder.Services.AddInfrastructure();
    builder.Services.AddApplicationServices();

    //
    // Hangfire
    //
    builder.Services.AddHangfire(x => x
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings().
        UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireDbConnection"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));
    builder.Services.AddHangfireServer();

    //
    // Add serilog
    //
    builder.Services.AddSingleton(Log.Logger);
    var _logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
    builder.Logging.AddSerilog(_logger);

    //
    // Cors
    //
    builder.Services.AddCors(opt =>
    {
        //var frontEndUrl = config["FrontEndUrl"];
        opt.AddPolicy("DEFAULT", policy =>
        {
            policy.WithOrigins("http://110.120.102.54")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            policy.WithOrigins("http://110.120.102.60")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
    });

    //
    // Localization
    //
    builder.Services.AddLocalization();
    builder.Services.AddRequestLocalization(x =>
    {
        var supportedCulture = new[]
        {
                    new CultureInfo("en-US"),
                    new CultureInfo("en"),
                    new CultureInfo("fa"),
                    new CultureInfo("fa-IR")
                };
        x.DefaultRequestCulture = new RequestCulture("fa");
        x.ApplyCurrentCultureToResponseHeaders = true;
        x.SupportedCultures = supportedCulture;
        x.SupportedUICultures = supportedCulture;
    });

    builder.Services.AddDetection();

    //
    // Exception handling
    //
    builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

    //
    //Authentication & Authorization
    //
    //var jwtSettings = config.GetSection("Jwt");
    var key = Encoding.UTF8.GetBytes("Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr8g5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx");

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "JWTAuthenticationServer",
                ValidAudience = "JWTServiceClient",
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.HttpContext.Request.Cookies["authtoken"];
                    if (!string.IsNullOrEmpty(token))
                        context.Token = token;
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Append("IS-TOKEN-EXPIRED", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });
    builder.Services.AddAuthorization();
    builder.Services.AddValidatorsFromAssemblyContaining<AddStoreCommand>();
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
}

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<CommandDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var exception = context.Features.Get<IExceptionHandlerFeature>();
            if (exception != null && exception.Error is ValidationException validationException)
            {
                var result = validationException.Message;  // The message is already a JSON string
                await context.Response.WriteAsync(result);
            }
        });
    });
    //app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseDetection();
    app.UseRequestLocalization();
    app.UseRouting();
    app.UseCors("DEFAULT");
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    app.MapControllers();
    app.UseHangfireDashboard("/TasksDashboard", new DashboardOptions
    {
        Authorization = new[] { new HangfireAuthorizationFilter() },
        IsReadOnlyFunc = (DashboardContext context) => true
    });
    app.Run();
}
