using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Work20210608.Data;
using Work20210608.Interfaces;
using Work20210608.Services;
using Work20210608.Repositories;
using Microsoft.AspNetCore.Http;
using Work20210608.Wappers;

namespace Work20210608
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

            #region Session引用 https://ithelp.ithome.com.tw/articles/10194799
            // 將 Session 存在 ASP.NET Core 記憶體中
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = ".Work20210608.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(180);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ISessionWapper, SessionWapper>();
            #endregion

            services.AddDbContext<Work20210608Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Work20210608Context")));

            //相依性注入
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IMessageService, MessageService>();
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

            #region Session引用 https://ithelp.ithome.com.tw/articles/10194799
            // SessionMiddleware 加入 Pipeline
            app.UseSession();
            app.Use(async (context, next) =>
            {
                context.Session.SetString("SessionKey", "SessoinValue");
                await next.Invoke();
            });
            #endregion

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
