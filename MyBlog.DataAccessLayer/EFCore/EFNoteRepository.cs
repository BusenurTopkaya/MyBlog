using MyBlog.DataAccessLayer.Abstract;
using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccessLayer.EFCore
{
    public class EFNoteRepository : INoteRepository
    {
        private DatabaseContext context;

        public EFNoteRepository(DatabaseContext _context)
        {
            context = _context;
        }


        public void AddNote(Note entity)
        {
            context.Notes.Add(entity);
            context.SaveChanges();
        }

        public void DeleteNote(int noteId)
        {
            var note = context.Notes.FirstOrDefault(x => x.NoteId == noteId);
            if (note != null)
            {
                context.Notes.Remove(note);
                context.SaveChanges();
            }
        }

        public IQueryable<Note> GetAll()
        {
            return context.Notes;
        }

        public Note GetById(int noteId)
        {
           return context.Notes.FirstOrDefault(x => x.NoteId == noteId);
        }

        public void UpdateNote(Note entity)
        {
            var note = GetById(entity.NoteId);

            if (note != null)
            {
                note.Title = entity.Title;
                note.Description = entity.Description;
                note.CategoryId = entity.CategoryId;
                note.Image = entity.Image;
                note.isApproved = entity.isApproved;
                note.isHome = entity.isHome;
                note.isSlider = entity.isSlider;

                context.SaveChanges();
            }
        }
    }
}
