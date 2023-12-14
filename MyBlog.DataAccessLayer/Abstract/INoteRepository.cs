using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccessLayer.Abstract
{
    public interface INoteRepository
    {
        //Generic repository kullanımadık, EN BASİT YÖNTEMİYLE repository kullanıyoruz.

        Note GetById(int noteId);
        IQueryable<Note> GetAll();
        void AddNote(Note entity);
        void UpdateNote(Note entity);
        void DeleteNote(int noteId);
    }
}
