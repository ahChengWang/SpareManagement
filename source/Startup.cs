using Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpareManagement.DomainService;
using SpareManagement.Repository;
using System;

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
            services.AddSingleton<IBasicInformationDomainService, BasicInformationDomainService>();
            services.AddSingleton<ISerialNumberDomainService, SerialNumberDomainService>();
            services.AddSingleton<IWarehouseDomainService, WarehouseDomainService>();
            services.AddSingleton<IReleaseDomainService, ReleaseDomainService>();
            services.AddSingleton<IExpendablesDomainService, ExpendablesDomainService>();
            services.AddSingleton<IComponentsDomainService, ComponentsDomainService>();
            services.AddSingleton<IJigsDomainService, JigsDomainService>();
            services.AddSingleton<IWirePanelDomainService, WirePanelDomainService>();
            services.AddSingleton<IInspectDomainService, InspectDomainService>();
            services.AddSingleton<IReturnDomainService, ReturnDomainService>();
            services.AddSingleton<IFixDomainService, FixDomainService>();
            services.AddSingleton<IHistoryDomainService, HistoryDomainService>();
            services.AddSingleton<IAccountDomainService, AccountDomainService>();
            services.AddSingleton<ISampleDomainService, SampleDomainService>();

            services.AddSingleton<IBasicInformationRepository, BasicInformationRepository>();
            services.AddSingleton<IExpendablesRepository, ExpendablesRepository>();
            services.AddSingleton<IComponentsRepository, ComponentsRepository>();
            services.AddSingleton<IJigsRepository, JigsRepository>();
            services.AddSingleton<IWirePanelRepository, WirePanelRepository>();
            services.AddSingleton<IHistoryRepository, HistoryRepository>();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ISampleRepository, SampleRepository>();

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
