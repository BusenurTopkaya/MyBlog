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
            //DbContext'i uygulamam�za tan�t�yoruz
            //Configuration nesnesi arac�l���yla ConnectionString ile DatabaseContext'i ili�kilendirdik
            //b=> b.MigrationsAssembly("MyBlog.WebApp") : Migration'�n kurulaca�� proje
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b=> b.MigrationsAssembly("MyBlog.WebApp")));

            //MVC aktif
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //AddTransient metodu ile ICategoryRepository �a��r�ld��� zaman bana EFCategoryRepository g�nder
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

            //Durum sayfalar�n�n g�sterilmesi i�in
            app.UseStatusCodePages();

            //wwwroot i�erisindeki static dosyalar� eri�ilebilir yapar
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Static metot oldu�u i�in class'a direk ula�abildik.
            SeedData.Seed(app);
        }
    }
}
