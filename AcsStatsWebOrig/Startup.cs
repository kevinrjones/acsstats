using System;
using System.Net.Http;
using System.Net.Http.Headers;
using AcsHttpClient;
using AcsRepository;
using AcsStatsWeb.AcsHttpClient;
using AcsStatsWeb.Formatter;
using AcsStatsWeb.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Serilog;
using Services;
using Services.AcsServices;
using Services.Remote;

// ReSharper disable InconsistentNaming

namespace AcsStatsWeb
{
    public class Startup
    {
        private const int HttpClientTimeoutSeconds = 3000;

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
            services.AddMvc(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.OutputFormatters.Add(new CsvOutputFormatter());
            });
            
            services.AddControllersWithViews();

            services.AddLogging();
            services.AddLocalization();
            
            RegisterHttpProxyWithPolicy(services);
            
            InitializeContainer(services);
            
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddDbContext<AcsDbContext>(options =>
            {
                options.UseMySQL(
                    Configuration.GetConnectionString("acsstats"));
            });
        }

        private void InitializeContainer(IServiceCollection services)
        {
            
            RegisterServices(services);

        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRemoteTeamsService, RemoteTeamsService>();
            services.AddScoped<IRemotePlayerService, RemotePlayerService>();
            services.AddScoped<IRemoteBattingRecordsService, RemoteBattingRecordsService>();
            services.AddScoped<IRemoteFieldingRecordsService, RemoteFieldingRecordsService>();
            services.AddScoped<IRemoteBowlingRecordsService, RemoteBowlingRecordsService>();
            services.AddScoped<IRemotePartnershipsRecordsService, RemotePartnershipRecordsService>();
            services.AddScoped<IRemoteScorecardsService, RemoteScorecardService>();
            services.AddScoped<IRemoteMatchesService, RemoteMatchesService>();
            services.AddScoped<IGroundsService, GroundsService>();
            services.AddScoped<IValidation, Validation>();
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

        }
    }
}