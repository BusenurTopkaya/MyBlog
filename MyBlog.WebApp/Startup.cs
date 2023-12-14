using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.DataAccessLayer.Abstract;
using MyBlog.DataAccessLayer.EFCore;

namespace MyBlog.WebApp
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
            //DbContext'i uygulamamýza tanýtýyoruz
            //Configuration nesnesi aracýlýðýyla ConnectionString ile DatabaseContext'i iliþkilendirdik
            //b=> b.MigrationsAssembly("MyBlog.WebApp") : Migration'ýn kurulacaðý proje
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b=> b.MigrationsAssembly("MyBlog.WebApp")));

            //MVC aktif
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //AddTransient metodu ile ICategoryRepository çaðýrýldýðý zaman bana EFCategoryRepository gönder
            services.AddTransient<ICategoryRepository, EFCategoryRepository>();
            services.AddTransient<INoteRepository, EFNoteRepository>();
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
            }

            //Durum sayfalarýnýn gösterilmesi için
            app.UseStatusCodePages();

            //wwwroot içerisindeki static dosyalarý eriþilebilir yapar
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Static metot olduðu için class'a direk ulaþabildik.
            SeedData.Seed(app);
        }
    }
}
