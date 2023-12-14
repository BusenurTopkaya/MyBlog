using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApp.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private ICategoryRepository _categoryRepository;

        public CategoryMenuViewComponent(ICategoryRepository repoCategory)
        {
            _categoryRepository = repoCategory;
        }

        public IViewComponentResult Invoke()
        {
            //RouteData aracılığı ile URL de ki id bilgisine ulaştık.
            ViewBag.SelectedCategory = RouteData?.Values["id"];

            return View(_categoryRepository.GetAll());
        }
    }
}
