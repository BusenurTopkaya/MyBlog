using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.DataAccessLayer.Abstract;
using MyBlog.Entities;

namespace MyBlog.WebApp.Controllers
{
    public class NoteController : Controller
    {
        private INoteRepository _noteRepository;
        private ICategoryRepository _categoryRepository;

        public NoteController(INoteRepository noteRepo, ICategoryRepository categoryRepo)
        {
            _noteRepository = noteRepo;
            _categoryRepository = categoryRepo;
        }

        public IActionResult Index(int? id, string arama)
        {
            var query = _noteRepository.GetAll().Where(x => x.isApproved);

            if (id != null)
            {
                query = query.Where(x => x.CategoryId == id);
            }

            //eğer arama null ya da boşluk karakterine eşit değilse
            if (!string.IsNullOrEmpty(arama))
            {
                query = query.Where(x => x.Title.Contains(arama) || x.Description.Contains(arama) || x.Body.Contains(arama));
            }

            return View(query.OrderByDescending(x=>x.CreatedDate));
        }

        public IActionResult List()
        {
            return View(_noteRepository.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_noteRepository.GetById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            //View'e bir SelectList göndermemiz gerekiyor
            //dataValueField: CategoryId: post işleminde GÖNDERİLECEK bilgi
            //dataTextField: Name : GÖSTERİLECEK
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");
            return View();
        }

        //IFormFile kullanımında action'ımızın asenkron olması gerekiyor, asenkron olduğu için bir Task<> döndürmesi gerekiyor
        [HttpPost]
        public async Task<IActionResult> Create(Note entity, IFormFile file)
        {
            entity.CreatedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);

                    using (var stream = new FileStream(path,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    entity.Image = file.FileName;
                }

                _noteRepository.AddNote(entity);
                return RedirectToAction("List");
            }

            //model geçerli değilse
            return View(entity);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");
            return View(_noteRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Note entity, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    //Path.Combine: dosyanın kaydedileceği path'i oluşturuyoruz
                    //Directory.GetCurrentDirectory() : uygulamamızın ana dizinini alıyoruz
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);

                    //bir Stream oluşturuyoruz
                    //FileStream sayesinde senkron ve asenkron olarak okuma/yazma operasyonlarını gerçekleştirebiliyoruz.
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        //Stream'i (dosyamızı) CopyToAsync() ile fiziksel olarak wwwroot\img dizinine kayıt ediyoruz.
                        await file.CopyToAsync(stream);
                    }

                    entity.Image = file.FileName;
                }

                _noteRepository.UpdateNote(entity);
                TempData["message"] = $"{entity.Title} güncellendi.";
                return RedirectToAction("List");
            }

            //model geçerli değilse
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");
            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_noteRepository.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int NoteId)
        {
            _noteRepository.DeleteNote(NoteId);
            TempData["message"] = $"{NoteId} numaralı not kayıt silindi.";
            return RedirectToAction("List");
        }
    }
}