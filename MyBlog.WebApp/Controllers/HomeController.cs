using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.DataAccessLayer.Abstract;
using MyBlog.WebApp.Models;
using MyBlog.WebApp.ViewModels;

namespace MyBlog.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private INoteRepository noteRepository;

        public HomeController(INoteRepository repository)
        {
            noteRepository = repository;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();

            //model.HomeNotes = noteRepository.GetAll().Where(x => x.isApproved == true && x.isHome == true).ToList();
            model.HomeNotes = noteRepository.GetAll().Where(x => x.isApproved && x.isHome).ToList();
            model.SliderNotes = noteRepository.GetAll().Where(x => x.isApproved && x.isSlider).ToList();

            return View(model);
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

    }
}
