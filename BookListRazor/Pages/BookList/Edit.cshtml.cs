using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Book Book { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;

        }
        public async Task OnGet(int id)
        {

            this.Book =  await _db.Book.FindAsync(id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookFromDb = await _db.Book.FindAsync(this.Book.Id);
                bookFromDb.Name = this.Book.Name;
                bookFromDb.Author = this.Book.Author;
                bookFromDb.ISBN = this.Book.ISBN;
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            else
            {
                return RedirectToPage();

            }
        }
    }
}