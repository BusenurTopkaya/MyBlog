using Microsoft.EntityFrameworkCore;
using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.DataAccessLayer.EFCore
{
    public class DatabaseContext : DbContext
    {
        //DatabaseContext yapıcı metotu dışarıdan bir options alacak, bu options bize ConnectionString'i getirecek.
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
