using FluentValidation;
using Gringotts.Core;
using Gringotts.DAL;
using Gringotts.DAL.Interfaces;
using Gringotts.Models.Interfaces;
using Gringotts.Repository;
using Gringotts.Services;
using Gringotts.Services.Contracts;
using Gringotts.Services.Interfaces;
using Gringotts.Services.Validators;
using Gringotts.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gringotts
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
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gringotts", Version = "v1" });
            });

            services.AddDbContextPool<BankDbContext>(options => options.UseSqlServer("Data Source=JARVIS;Initial Catalog=gringotts_bank;Integrated Security=true;"));

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IValidator<CreateCustomerRequest>, CustomerRequestValidator>();
            services.AddTransient<IValidator<CreateAccountRequest>, CreateAccountRequestValidator>();
            services.AddTransient<ICustomerManager, CustomerManager>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICustomerDal, CustomerDal>();
            services.AddTransient<IAccountDal, AccountDal>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gringotts v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
