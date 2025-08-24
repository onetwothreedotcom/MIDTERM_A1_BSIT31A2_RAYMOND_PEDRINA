using Library_Management.Models;
using Library_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService = AuthorService.Instance;

        public IActionResult Index() => View(_authorService.GetAuthors());

        public IActionResult Details(Guid id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null) return NotFound();
            return View(author);
        }

        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid) return View(author);
            _authorService.AddAuthor(author);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null) return NotFound();
            return View(author);
        }
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid) return View(author);
            _authorService.UpdateAuthor(author);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null) return NotFound();
            return View(author);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _authorService.DeleteAuthor(id);
            return RedirectToAction("Index");
        }

        // Archiving
        public IActionResult Archive(Guid id)
        {
            _authorService.ArchiveAuthor(id);
            return RedirectToAction("Index");
        }
        public IActionResult ArchivedList() => View(_authorService.GetArchivedAuthors());
        public IActionResult Restore(Guid id)
        {
            _authorService.RestoreAuthor(id);
            return RedirectToAction("ArchivedList");
        }
    }
}
