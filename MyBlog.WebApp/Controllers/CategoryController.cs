using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccessLayer.Abstract;
using MyBlog.Entities;

namespace MyBlog.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository repoCategory)
        {
            _categoryRepository = repoCategory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(_categoryRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category entity)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.AddCategory(entity);
                return RedirectToAction("List");
            }

            //model geçerli değilse
            return View(entity);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_categoryRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Category entity)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.UpdateCategory(entity);
                TempData["message"] = $"{entity.Name} güncellendi.";
                return RedirectToAction("List");
            }

            //model geçerli değilse
            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_categoryRepository.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int CategoryId)
        {
            _categoryRepository.DeleteCategory(CategoryId);
            TempData["message"] = $"{CategoryId} numaralı kategori silindi.";
            return RedirectToAction("List");
        }
    }
}