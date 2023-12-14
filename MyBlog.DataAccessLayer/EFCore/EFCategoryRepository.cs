using MyBlog.DataAccessLayer.Abstract;
using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccessLayer.EFCore
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private DatabaseContext context;

        public EFCategoryRepository(DatabaseContext _context)
        {
            //dışarıdan gelecek olan _context context değişkenine aktarılacak
            context = _context;
        }

        public void AddCategory(Category entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories;
        }

        public Category GetById(int categoryId)
        {
            return context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
        }

        public void UpdateCategory(Category entity)
        {
            //context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            var cat = GetById(entity.CategoryId);

            if (cat != null)
            {
                cat.Name = entity.Name;

                context.SaveChanges();
            }

        }
    }
}
