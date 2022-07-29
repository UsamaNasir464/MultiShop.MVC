using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.DataAccess.Infrastructure.Repository;
using MultiShop.Mvc.DataAccess.ServiceBus.EmailService;
using MultiShop.Mvc.Models.ViewModels;

namespace MultiShop.MVC
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
            //services.AddIdentity<RegisterNewUser, IdentityRole>();

            services.AddHttpClient<ICategoryConsumeApi, CategoryConsumeApi>();
            services.AddHttpClient<IProducts, Products>();
            services.AddControllersWithViews();

            services.AddScoped<ICategoryConsumeApi, CategoryConsumeApi>();
            services.AddScoped<IProducts, Products>();
            services.AddScoped<IOrderConsumeApi, OrderConsumeApi>();
            services.AddScoped<IEmailSending, EmailSending>();
            services.AddScoped<IUserAccount, UserAccount>();
           

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
