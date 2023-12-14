using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccessLayer.Abstract
{
    public interface ICategoryRepository
    {
        //Generic repository kullanımadık, EN BASİT YÖNTEMİYLE repository kullanıyoruz.

        //DB işlemlerinde ihtiyacımız olacak en temel metotları oluşturduk.
        Category GetById(int categoryId);
        IQueryable<Category> GetAll();
        void AddCategory(Category entity);
        void UpdateCategory(Category entity);
        void DeleteCategory(int categoryId);
    }
}
