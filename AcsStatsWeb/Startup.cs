using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using AcsHttpClient;
using AcsRepository;
using AcsRepository.Repositories;
using AcsStatsWeb.AcsHttpClient;
using AcsStatsWeb.Api.cqrs;
using AcsStatsWeb.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Serilog;
using Services;
using Services.AcsServices;
using Services.Remote;
using SimpleInjector;
// ReSharper disable InconsistentNaming

namespace AcsStatsWeb
{
  public class Startup
  {
    private const int HttpClientTimeoutSeconds = 3000;
    
    private readonly Container container = new SimpleInjector.Container();
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      
      services.AddControllersWithViews();

      services.AddLogging();
      services.AddLocalization();
      RegisterHttpProxyWithPolicy(services);

      services.AddSimpleInjector(container, options =>
      {
        options.AddAspNetCore()
          .AddControllerActivation()
          .AddViewComponentActivation();
        
        options.AddLogging();
        options.AddLocalization();
        options.CrossWire<AcsDbContext>();
        options.CrossWire<IServiceProvider>();
      });
      
      InitializeContainer();
      
      
      services.AddDbContext<AcsDbContext>(options =>
      {
        options.UseMySQL(
          Configuration.GetConnectionString("acsstats"));
      });

    }

    private void InitializeContainer()
    {
      RegisterRepositories();
      RegisterServices();

      container.Register<IEfUnitOfWork, EfUnitOfWork>(Lifestyle.Scoped);
      container.Register<Messages>(Lifestyle.Transient);

    }

    private void RegisterServices()
    {

      var repositoryAssembly = typeof(MatchesService).Assembly;
      
      
      var registrations =
        from type in repositoryAssembly.GetExportedTypes()
        where type.Namespace.StartsWith("Services.AcsServices") 
        from service in type.GetInterfaces()
        select new { service, implementation = type };

      foreach (var reg in registrations)
      {
        container.Register(reg.service, reg.implementation, Lifestyle.Scoped);
      }

      registrations =
        from type in repositoryAssembly.GetExportedTypes()
        where type.Namespace.StartsWith("Services.Remote") 
        from service in type.GetInterfaces()
        select new { service, implementation = type };

      foreach (var reg in registrations)
      {
        container.Register(reg.service, reg.implementation, Lifestyle.Scoped);
      }
      
    }

    private void RegisterRepositories()
    {
      
      var repositoryAssembly = typeof(MatchesRepository).Assembly;
      
      
      var registrations =
        from type in repositoryAssembly.GetExportedTypes()
        where type.Namespace.StartsWith("AcsRepository.Repositories")
        from service in type.GetInterfaces()
        select new { service, implementation = type };

      foreach (var reg in registrations)
      {
        container.Register(reg.service, reg.implementation, Lifestyle.Scoped);
      }
    }

    private static void RegisterHttpProxyWithPolicy(IServiceCollection services)
    {
      var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(HttpClientTimeoutSeconds);

      services.AddHttpClient<IHttpClientProxy, HttpClientProxy>(client =>
        {
          client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        })
        .AddPolicyHandler(timeoutPolicy)
        .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
        {
          TimeSpan.FromSeconds(1),
          TimeSpan.FromSeconds(5)
        }));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // UseSimpleInjector() finalizes the integration process.
      app.ApplicationServices.UseSimpleInjector(container);
      
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseMiddleware<ExceptionHandler>(Log.Logger);
      
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
      });
      
      // Always verify the container
      container.Verify();
    }
  }
}