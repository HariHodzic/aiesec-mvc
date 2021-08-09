using System;
using Aiesec.Data.Model.IdentityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using System.Reflection;
using Aiesec.Data.Context;
using Aiesec.Repository;
using Aiesec.Web.Configuration.RoleSeed;
using Aiesec.Web.Helper.SelectListService;
using Aiesec.Web.Mail;
using Microsoft.AspNetCore.Mvc.Authorization;
using Aiesec.Repository.IServices;
using Aiesec.Repository.Services;
using Aiesec.Web.Hubs;
using Aiesec.Repository.IRepository;
using Aiesec.Repository.Repository;

namespace Aiesec.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
                options.EnableEndpointRouting = false;
            });
            services.AddDbContext<AiesecDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("local")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;

                    config.Password.RequiredLength = 8;
                    config.Password.RequireDigit = true;
                    config.Password.RequireLowercase = true;
                    config.Password.RequireUppercase = true;
                    config.Password.RequiredUniqueChars = 0;
                    config.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<AiesecDbContext>()
                .AddRoles<ApplicationRole>()
                .AddDefaultTokenProviders();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Aiesec.Cookie";
                config.LoginPath = "/Account/Login";
            });
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddSignalR();
            services.AddScoped<IMailSender, MailSender>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IOfficeService, OfficeService>();
            services.AddTransient<ISelectListService, SelectListService>();
            services.AddTransient<ILocalCommitteeService, LocalCommitteeService>();
            services.AddTransient<IFunctionalFieldService, FunctionalFieldService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chatHub");
            });
            RoleSeed.CreateRoles(serviceProvider).Wait();
        }
    }
}