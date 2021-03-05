using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Webinar.TranslationDemo.Application.AutoMapper;
using Webinar.TranslationDemo.Application.Services;
using Webinar.TranslationDemo.Domain.Repositories;
using Webinar.TranslationDemo.Domain.Services;
using Webinar.TranslationDemo.Infrastructure.Data.Repositories;

namespace Webinar.TranslationDemo.Lambda
{
    public class Startup
    {
        public readonly IConfigurationRoot Configuration; //Para el Environment
        public readonly ServiceProvider ServiceProvider; //Para DependecyInjection

        public Startup()
        {
            #region Environment Management
            string environment = Environment.GetEnvironmentVariable("Environment");

            if (!string.IsNullOrEmpty(environment) && environment == "PDN")
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Production.json", true, true).Build();
            }
            else
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json", true, true).Build();
            }
            #endregion

            IServiceCollection services = new ServiceCollection();

            //Acá se asocian las interfaces con las clases que las implementan
            
            //Application
            services.AddSingleton<IInventoryApplicationService, InventoryApplicationService>();
            
            //Domain
            services.AddSingleton<IInventoryDomainService, InventoryDomainService>();
            services.AddSingleton<ITranslationDomainService, TranslationDomainService>();
            services.AddSingleton<ITermTranslationRepository, TermTranslationRepository>();

            services.AddSingleton<IConfiguration>(Configuration);
            
            //Mapper
            services.AddSingleton<IMapper>(AutoMapperConfig.RegisterMappings());

            ServiceProvider = services.BuildServiceProvider();


        }
    }
}
