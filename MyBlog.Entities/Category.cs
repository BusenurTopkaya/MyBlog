using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        //her bir kategorinin birden fazla notu olabilir.
        public List<Note> Notes { get; set; }
    }
}
