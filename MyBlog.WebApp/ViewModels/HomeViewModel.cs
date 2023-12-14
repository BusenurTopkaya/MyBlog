using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApp.ViewModels
{
    public class HomeViewModel
    {
        public List<Note> SliderNotes { get; set; }
        public List<Note> HomeNotes { get; set; }
    }
}
