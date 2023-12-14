using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.DataAccessLayer.EFCore
{
    public static class SeedData
    {
        //Seed metodu dışarıdan IApplicationBuilder alacak
        public static void Seed(IApplicationBuilder app)
        {
            // uygulama içerisindeki context'e GetRequiredService ile ulaştık
            DatabaseContext context = app.ApplicationServices.GetRequiredService<DatabaseContext>();

            //otomatik migration - bekleyen migration'ları DB'e aktarır.
            context.Database.Migrate();

            //herhangi bir kategori eklenmemişse çalışşsın
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category() { Name="Category 1"},
                    new Category() { Name="Category 2"},
                    new Category() { Name="Category 3"}
                    );
                context.SaveChanges();
            }

            if (!context.Notes.Any())
            {
                context.Notes.AddRange(
                    new Note() { Title="Note title 1",Description="Note Description", Body="Note Body 1", Image="1.jpg", CreatedDate= DateTime.Now.AddDays(-5), isApproved=true,CategoryId=1},
                    new Note() { Title = "Note title 2", Description = "Note Description", Body = "Note Body 2", Image = "2.jpg", CreatedDate = DateTime.Now.AddDays(-15), isApproved = true, CategoryId = 2 },
                    new Note() { Title = "Note title 3", Description = "Note Description", Body = "Note Body 3", Image = "3.jpg", CreatedDate = DateTime.Now.AddDays(-25), isApproved = true, CategoryId = 2 },
                    new Note() { Title = "Note title 4", Description = "Note Description", Body = "Note Body 4", Image = "4.jpg", CreatedDate = DateTime.Now.AddDays(-35), isApproved = true, CategoryId = 3 }
                    );

                context.SaveChanges();
            }
        }
    }
}
