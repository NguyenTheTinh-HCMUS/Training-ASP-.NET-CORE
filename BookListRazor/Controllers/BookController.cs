using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async  Task<IActionResult> GetAll()
        {
            return Json(

              new { data = await  _db.Book.ToListAsync() }
            );
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var BookfromBb = await _db.Book.FindAsync(id);
            if (BookfromBb == null)
            {
                return Json(new { 
                success=false,
                message="Error while Deleting"
                });
            }
            _db.Book.Remove(BookfromBb);
            await _db.SaveChangesAsync();
            return Json(new
            {
                success = true,
                message = "Delete successful"
            });
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
