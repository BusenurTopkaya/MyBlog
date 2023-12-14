using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Entities
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool isApproved { get; set; }
        public bool isHome { get; set; }
        public bool isSlider { get; set; }        

        //foreing key - yabancı anahtar
        public int CategoryId { get; set; }

        //her not'un bir kategorisi olur
        public Category Category { get; set; }
    }
}
