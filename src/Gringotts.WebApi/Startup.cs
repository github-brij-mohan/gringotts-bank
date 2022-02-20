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
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Newtonsoft.Json.Converters;
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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://dev-augl2m5i.us.auth0.com/";
                options.Audience = "https://localhost:44311/";
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gringotts", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } });
            });

            services.AddDbContextPool<BankDbContext>(options => options.UseSqlServer("Data Source=JARVIS;" +
                                                                                     "Initial Catalog=gringotts_bank;" +
                                                                                     "Integrated Security=true;"));

            //service registrations
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransactionService, TransactionService>();

            //validator registrations
            services.AddTransient<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
            services.AddTransient<IValidator<CreateAccountRequest>, CreateAccountRequestValidator>();
            services.AddTransient<IValidator<CreateTransactionRequest>, CreateTransactionRequestValidator>();

            //manager registrations
            services.AddTransient<ICustomerManager, CustomerManager>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<ITransactionManager, TransactionManager>();

            //repository registrations
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();

            //dal registrations
            services.AddTransient<ICustomerDal, CustomerDal>();
            services.AddTransient<IAccountDal, AccountDal>();
            services.AddTransient<ITransactionDal, TransactionDal>();
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
