using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NJsonSchema;
using NSwag.AspNetCore;
using System.Reflection;
using WalletSampleApi.Dac.Contract;
using WalletSampleApi.Dac;
using Newtonsoft.Json.Serialization;

namespace WalletSampleApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            // Register the Swagger services
            services.AddSwagger();

             var dbConfig = Configuration.GetSection(nameof(DatabaseConfigurations)).Get<DatabaseConfigurations>();
            services.AddTransient<DatabaseConfigurations>(svc => dbConfig);
            services.AddTransient<ICustomerWalletDac, CustomerWalletDac>();
            services.AddTransient<ICoinPriceUpdateDac, CoinPriceUpdateDac>();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
