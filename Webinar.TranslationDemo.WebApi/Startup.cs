using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webinar.TranslationDemo.Application.AutoMapper;
using Webinar.TranslationDemo.Application.Services;
using Webinar.TranslationDemo.Domain.Repositories;
using Webinar.TranslationDemo.Domain.Services;
using Webinar.TranslationDemo.Infrastructure.Common;
using Webinar.TranslationDemo.Infrastructure.Data.Repositories;

namespace Webinar.TranslationDemo.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            RegisterServices(services, Configuration);
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Adding dependencies from another layers (isolated from Presentation)

            //Mapper
            services.AddSingleton<IMapper>(AutoMapperConfig.RegisterMappings());

            //Application
            services.AddSingleton<IInventoryApplicationService, InventoryApplicationService>();

            //Domain
            services.AddSingleton<IInventoryDomainService, InventoryDomainService>();
            services.AddSingleton<ITranslationDomainService, TranslationDomainService>();
            services.AddSingleton<ITermTranslationRepository, TermTranslationRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = DefinitionCulture.DefineCulture();

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(supportedCultures.First()),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            };

            System.Threading.Thread.CurrentThread.CurrentCulture = supportedCultures.First();

            app.UseRequestLocalization(options);

            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
