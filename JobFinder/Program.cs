using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Core.Configuration;
using Core.Services;
using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using MediaBrowser.Model.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Swashbuckle.AspNetCore.SwaggerGen;
using Persistance.Interfaces;
using Core.Interfaces;
using Persistance.Interceptors;
using Persistance.DatabaseContext.WriteDbContext;
using Persistance.DatabaseContext.ReadDbContext;
using Application;
using Domain;
using Persistance;
using JobFinder.Domain.Common.Entities;
using JobFinder.Common;
using JobFinder.Services;
using JobFinder.Filters;
using JobFinder.MiddleWare;
using Microsoft.AspNetCore.Mvc.Versioning;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//{
//    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//});
builder.Services.AddControllers().AddJsonOptions(options =>
{

    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddAntiforgery(x =>
{
    x.HeaderName = "X-CSRF-TOKEN"; // Set the header name for CSRF token
    x.Cookie.Name = "X-CSRF-TOKEN"; // Set the cookie name for CSRF token
    x.SuppressXFrameOptionsHeader = false;
    x.Cookie.HttpOnly = false;
});
//    .AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
c =>
{
    c.SchemaGeneratorOptions = new SchemaGeneratorOptions
    {
        SchemaIdSelector = type => type.FullName
    };
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Karjoo", Version = "V1" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
}
);


builder.Services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();
builder.Services.AddScoped<ICommunicationOrchestrator, CommunicationOrchestrator>(); // Adjust implementation class as needed



builder.Services.AddDbContext<Persistance.DatabaseContext.LogContext.ExceptionContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LoggingConnection"));
    if (builder.Environment.IsDevelopment())
        options.EnableDetailedErrors().EnableSensitiveDataLogging()
            .ConfigureWarnings(x => x.Default(WarningBehavior.Log));
});

Console.WriteLine($"Creating {Directory.GetCurrentDirectory().GetType().Assembly.FullName} In Directory...{Directory.GetCurrentDirectory()}");

builder.Services.AddScoped<AuditSaveChangesInterceptor>();
string writeConnectionString = builder.Configuration.GetConnectionString("WorkWriteDB");
string readConnectionString = builder.Configuration.GetConnectionString("WorkReadDB");
builder.Services.AddDbContext<WriteDbContext>(options =>
{
    var provider = builder.Services.BuildServiceProvider();
    // var interceptor = provider.GetRequiredService<AuditSaveChangesInterceptor>();
    options.UseSqlServer(writeConnectionString);
    //   .AddInterceptors(interceptor);
    if (builder.Environment.IsDevelopment())
        options.EnableDetailedErrors().EnableSensitiveDataLogging().ConfigureWarnings(x => x.Default(WarningBehavior.Log));
}
);


//.AddInterceptors(new AuditSaveChangesInterceptor(httpContextAccessor, dateTimeProvider));
builder.Services.AddDbContext<ReadDbContext>(options =>
{
    options.UseSqlServer(readConnectionString);
    if (builder.Environment.IsDevelopment())
        options.EnableDetailedErrors().EnableSensitiveDataLogging()
            .ConfigureWarnings(x => x.Default(WarningBehavior.Log));
});



//DbSerilog For Sensitive Data Logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Capture Information and above
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console() // Console logging for debugging
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("LoggingConnection"),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "SerilogTbl",
            AutoCreateSqlTable = true,
            SchemaName = "dbo"
        },
        restrictedToMinimumLevel: LogEventLevel.Information // Log Information and above
    )
    .CreateLogger();


///First Way of Cacheing like in jwtService memoryCache
builder.Services.AddMemoryCache();



//Second Way of Cacheing like in jwtService redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.InstanceName = builder.Configuration["Redis:InstanceName"];
    options.Configuration = builder.Configuration["ConnectionStrings:RedisConnection"];
});

builder.Services.AddApplication()
            .ConfigureInfrastructureRegistrationServices(builder.Configuration)
            .ConfigureDomainRegistrationServices(builder.Configuration)
            .AddPersistanceServices(builder.Configuration);

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 100;
        options.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;
        options.QueueLimit = 70;
        options.Window = TimeSpan.FromMinutes(1);
    });
});
builder.Services.AddCors(options =>
{

    options.AddPolicy("UnifiedPolicy", policy =>
    {

        policy.WithOrigins(
            "http://localhost:5173",
            "http://localhost:4200",
            "http://localhost:4300",
            "http://localhost:3000",
            "http://localhost:5029",
            "http://localhost:52930",
            "http://localhost:50683"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); // Optional, use only if needed
    });
});

builder.Services.ConfigureSwaggerGen(options =>
{
    options.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
       }
   });
});

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = true;

})
.AddRoles<IdentityRole>() //adds roles 
.AddRoleManager<RoleManager<IdentityRole>>() //be able to use of role manager 
.AddEntityFrameworkStores<WriteDbContext>() //providing our context 
.AddSignInManager<SignInManager<User>>() ////make use of sigin manager 
.AddUserManager<UserManager<User>>() //make use of usemanager  to create user
.AddDefaultTokenProviders(); //be abe to create tokens for email confimation 


//builder.Services.AddIdentityCore<User>(options =>
//{
//    options.Password.RequiredLength = 6;
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.SignIn.RequireConfirmedEmail = true;

//})
//.AddRoles<IdentityRole>() //adds roles 
//.AddRoleManager<RoleManager<IdentityRole>>() //be able to use of role manager 
//.AddEntityFrameworkStores<ReadDbContext>() //providing our context 
//.AddSignInManager<SignInManager<User>>() ////make use of sigin manager 
//.AddUserManager<UserManager<User>>() //make use of usemanager  to create user
//.AddDefaultTokenProviders(); //be abe to create tokens for email confimation 


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

});

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(options =>
{

    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"]
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully.");
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            return Task.CompletedTask;
        }
    };
});



builder.Services.AddAuthorization();

builder.Services.AddOptions<RateLimitOptions>()
        .Bind(builder.Configuration.GetSection("RateLimiting"))
        .ValidateDataAnnotations();

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddInMemoryRateLimiting();

builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules = new List<RateLimitRule> {
        new RateLimitRule()
        {
            Endpoint = "*",
            Limit= 100,
            Period = "1m"
        }
    };
});
builder.Services.Configure<RateLimitOptions>(options =>
{
    options.GeneralRules.Add(new RateLimitRule()
    {
        Endpoint = "*",
        Limit = 100,
        Period = "1m",
        MonitorMode = true,
        PeriodTimespan = TimeSpan.FromSeconds(60),
    });
});

// Add API behavior filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
    // options.Filters.Add<ErrorHandlingFilterAttribute>();
}).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());


builder.Services.AddInMemoryRateLimiting();

//Data Initialization Of The Feature Price And Details in DbCOntext


builder.Host.UseSerilog();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders(); // Remove default providers
    loggingBuilder.AddSerilog(); // Use Serilog for Microsoft.Extensions.Logging
});
builder.Services.RegisterAppJobServicesApp(builder.Configuration);

//builder.Services.AddHostedService<PeriodicDatabaseSyncService>();
builder.Services.AddScoped<PushNotificationService>();
builder.Services.AddSingleton<ProblemDetailsFactory, JobSekeerProblemDetailFactory>();


builder.Services.AddApiVersioning(
          opt =>
          {
              opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
              opt.AssumeDefaultVersionWhenUnspecified = true;
              opt.ReportApiVersions = true;
              opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                              new HeaderApiVersionReader("x-api-version"),
                                                              new MediaTypeApiVersionReader("x-api-version"));
          }
      );
var app = builder.Build();

app.UseCors("UnifiedPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIs V 01");
        app.UseDeveloperExceptionPage();
        //c.InjectStylesheet("/Content/Swagger.css");
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIs V 01");
        app.UseDeveloperExceptionPage();
        //c.InjectStylesheet("/Content/Swagger.css");
    });
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.Use(async (context, next) =>
//{
//    var origin = context.Request.Headers["Origin"].ToString();
//    var allowedOrigins = new[] { "http://localhost:3000/","http://localhost:4200", "http://localhost:5029", 
//                                 "http://localhost:49234", "http://localhost:52930","http://localhost:50683","http://localhost:4300" };

//    if (!string.IsNullOrEmpty(origin) && !allowedOrigins.Contains(origin))
//    {
//        context.Response.StatusCode = StatusCodes.Status403Forbidden;
//        await context.Response.WriteAsync($"Origin is not allowed ${origin}");
//        return;
//    }
//    await next();
//});
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseIpRateLimiting();
app.UseRateLimiter();
//app.UseCors("AppOrigins");
app.MapControllers();

//await SeedData.InitializeAsync(app.Services);
//await SeedData.SeedAsync(app.Services);
//await SeedData.Initialize(app.Services);

app.Run();
