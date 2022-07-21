using System.Text;
using AcsCommands.Query;
using AcsRepository;
using AcsRepository.Util;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog;
using Services;
using Services.AcsServices;

namespace AcsStatsWeb;

public static class Program
{
  public static void Main(string[] args)
  {
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    Log.Logger = new LoggerConfiguration()
      .ReadFrom.Configuration(builder.Configuration)
      .CreateLogger();

    Log.Information("App starting");
    
    ConfigureServices(builder.Services, builder.Configuration);

    var app = builder.Build();
    
    Configure(app);

    app.Run();
  }

  private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
  {
    services.AddLogging();

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddScoped<IMatchesService, MatchesService>();
    services.AddScoped<IPlayersService, PlayersService>();
    services.AddScoped<ICountriesService, CountriesService>();
    services.AddScoped<ITeamsService, TeamsService>();
    services.AddScoped<IPartnershipService, PartnershipService>();
    services.AddScoped<IGroundsService, GroundsService>();
    services.AddScoped<IValidation, Validation>();
    services.AddMediatR(typeof(GroundsQuery).Assembly);
    services.AddScoped<IEfUnitOfWork, EfUnitOfWork>();


    services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });

    services.AddDbContext<AcsDbContext>(options =>
    {
      options.UseMySQL(
        configuration.GetConnectionString("acsstats"));
    });


    var commandsConnectionString =
      new CommandsConnectionString(configuration.GetConnectionString("commands"));
    var queriesConnectionString = new QueriesConnectionString(configuration.GetConnectionString("queries"));

    services.AddSingleton(commandsConnectionString);
    services.AddSingleton(queriesConnectionString);

    // todo: when we need ids
    // ConfigureIdentityServer(services, configuration);
  }

  private static void Configure(WebApplication app)
  {
// Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
      // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      app.UseHsts();
    }
    else
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();

// todo: not sure which of these two 
    app.MapControllerRoute(
      name: "default",
      pattern: "{controller}/{action=Index}/{id?}");
//app.MapControllers();

    app.MapFallbackToFile("index.html");
  }

  private static void ConfigureIdentityServer(IServiceCollection services, ConfigurationManager configuration)
  {
    var configSection = configuration.GetSection("IdentityServer");

    services.Configure<IdentityServerConfiguration>(configSection);

    IdentityServerConfiguration idsConfig = configSection.Get<IdentityServerConfiguration>();

    services.AddAuthentication(options =>
      {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        options.Authority = idsConfig.Authority;
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters.ValidAudiences = new[] { idsConfig.ApiResourceName };
        options.SaveToken = true;
      })
      .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
      {
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.Name = "acsstats";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";

        options.Events.OnRedirectToAccessDenied = context =>
        {
          if (context.Request.Path.StartsWithSegments("/api"))
          {
            context.Response.StatusCode = 403;
          }

          return Task.CompletedTask;
        };

        options.Events.OnRedirectToLogin = context =>
        {
          if (!context.Request.Path.StartsWithSegments("/api"))
          {
            context.Response.Redirect(context.RedirectUri);
          }

          return Task.CompletedTask;
        };
      })
      .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
      {
        options.UsePkce = true;
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.ResponseMode = OpenIdConnectResponseMode.Query;
        options.Authority = idsConfig.Authority;
        options.ClientId = idsConfig.ClientId;
        options.ClientSecret = idsConfig.ClientSecret;
        options.Scope.Add(idsConfig.ApiScope);
        options.SaveTokens = true;

        options.GetClaimsFromUserInfoEndpoint = true;

        options.Events.OnRedirectToIdentityProvider = context =>
        {
          if (context.Request.Path.StartsWithSegments("/api"))
          {
            context.Response.StatusCode = 401;
            context.HandleResponse();
          }

          return Task.CompletedTask;
        };
      });
  }

  private static void ConfigureSecurityHeaders(IApplicationBuilder app, IWebHostEnvironment env)
  {
    StringBuilder cspBuilder = new StringBuilder();
    string scriptSrc = "script-src 'self'";
    string styleSrc = "style-src 'self' 'unsafe-inline'";
    string defaultSrc = "default-src 'self'";
    string fontSrc = "font-src 'self'";
    string imgSrc = "img-src 'self' data:";
    string objectSrc = "object-src 'none'";
    string connectSrc = "";
    // Angular trusted types not yet supported with lazy loading modules
    // Once available, can prepend trustedTypes with "trusted-types angular; "
    string trustedTypes = "require-trusted-types-for 'script'";
    // Angular uses web sockets for hot reload
    // Angular requires 'unsafe-inline' during dev. The offending function is removed during prod build
    if (env.IsDevelopment())
    {
      scriptSrc += " 'unsafe-inline'";
      connectSrc = "connect-src 'self' ws: wss:";
    }

    cspBuilder.AppendJoin("; ", defaultSrc, scriptSrc, styleSrc, fontSrc, imgSrc, objectSrc, connectSrc, trustedTypes);
    string cspRule = cspBuilder.ToString();
    app.Use(async (context, next) =>
    {
      //XSS
      context.Response.Headers.Add("Content-Security-Policy", cspRule);
      //Man in the middle
      context.Response.Headers.Add("Strict-Transport-Security", "max-age=63072000; preload");
      //Clickjacking
      context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
      //MIME-sniff
      context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
      //Referrer data leak
      context.Response.Headers.Add("Referrer-Policy", "same-origin");
      await next();
    });
  }
}

internal class IdentityServerConfiguration
{
  public string? Authority { get; set; }
  public string? ClientId { get; set; }
  public string? ClientSecret { get; set; }
  public string ApiScope { get; set; }
  public string ApiResourceName { get; set; }
}