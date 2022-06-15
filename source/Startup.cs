using Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpareManagement.DomainService;
using SpareManagement.Helper;
using SpareManagement.Repository;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace SpareManagement
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
            services.AddSingleton<BasicInformationDomainService>();
            services.AddSingleton<SerialNumberDomainService>();
            services.AddSingleton<WarehouseDomainService>();
            services.AddSingleton<ReleaseDomainService>();
            services.AddSingleton<ExpendablesDomainService>();
            services.AddSingleton<ComponentsDomainService>();
            services.AddSingleton<JigsDomainService>();
            services.AddSingleton<WirePanelDomainService>();
            services.AddSingleton<InspectDomainService>();
            services.AddSingleton<ReturnDomainService>();
            services.AddSingleton<FixDomainService>();
            services.AddSingleton<HistoryDomainService>();
            services.AddSingleton<AccountDomainService>();

            services.AddSingleton<BasicInformationRepository>();
            services.AddSingleton<ExpendablesRepository>();
            services.AddSingleton<ComponentsRepository>();
            services.AddSingleton<JigsRepository>();
            services.AddSingleton<WirePanelRepository>();
            services.AddSingleton<HistoryRepository>();
            services.AddSingleton<AccountRepository>();

            services.Add(new ServiceDescriptor(typeof(MSSqlDBHelper), new MSSqlDBHelper(Configuration)));

            //services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] 
            //{
            //    UnicodeRanges.BasicLatin,
            //    UnicodeRanges.CjkUnifiedIdeographs 
            //}));

            //services.AddSingleton<ISystemSettingDomainService, SystemSettingDomainService>();

            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.LoginPath = "/Account/Index";
                x.ExpireTimeSpan = TimeSpan.FromMinutes(540);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler("/Home/Error");
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            //app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
