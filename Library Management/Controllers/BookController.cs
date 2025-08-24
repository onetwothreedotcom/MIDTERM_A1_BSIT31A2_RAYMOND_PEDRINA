using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            var books = BookService.Instance.GetBooks();
            return View(books);
        }

        public IActionResult AddModal()
        {
            return PartialView("_AddBookPartial");
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("AddModal", vm); // Re-render the modal with validation errors
            }

            BookService.Instance.AddBook(vm);

            return RedirectToAction("Index");
        }


        public IActionResult EditModal(Guid id)
        {
            var editBookViewModel = BookService.Instance.GetBookById(id);
            if (editBookViewModel == null)
                return NotFound();

            return PartialView("_EditBookPartial", editBookViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookService.Instance.UpdateBook(vm);
            return Ok();
        }

        // ✅ UPDATED DeleteModal and Delete
        public IActionResult DeleteModal(Guid id)
        {
            var book = BookService.Instance.GetBookById(id);
            if (book == null)
                return NotFound();

            return PartialView("_DeleteBookPartial", book); // ✅ updated partial name
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var book = BookService.Instance.GetBookById(id);
            if (book == null)
                return NotFound();

            BookService.Instance.DeleteBook(id);
            return Ok(); // You can return a redirect if not using AJAX
        }

        public IActionResult Details(Guid id)
        {
            var book = BookService.Instance.GetBook(id);
            if (book == null)
                return NotFound();

            return View(book);
        }
        private readonly BookService _bookService = BookService.Instance;
        [HttpGet]
        public IActionResult AddCopy(Guid bookId)
        {
            var vm = new AddBookCopyViewModel { BookId = bookId };
            return PartialView("_AddCopyPartial", vm); // load partial
        }

        [HttpPost]
        public IActionResult AddCopy(AddBookCopyViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_AddCopyPartial", vm);

            _bookService.AddBookCopy(vm);
            return RedirectToAction("Details", new { id = vm.BookId });
        }
        public IActionResult PulloutModal(Guid copyId)
        {
            var model = new PulloutBookCopyViewModel
            {
                CopyId = copyId
            };
            return PartialView("PulloutModal", model);
        }

        [HttpPost]
        public IActionResult ConfirmPullout(PulloutBookCopyViewModel model)
        {
            if (ModelState.IsValid)
            {
                BookService.Instance.PulloutCopy(model.CopyId, model.PulloutReason);
                return Ok();
            }
            return BadRequest("Invalid request");
        }

        // Archiving functionality
        public IActionResult Archive(Guid id)
        {
            BookService.Instance.ArchiveBook(id);
            return RedirectToAction("Index");
        }

        public IActionResult ArchivedList()
        {
            var archivedBooks = BookService.Instance.GetBooks(includeArchived: true)
                .Where(b => b.IsArchived);
            return View(archivedBooks);
        }

        public IActionResult Restore(Guid id)
        {
            BookService.Instance.RestoreBook(id);
            return RedirectToAction("ArchivedList");
        }

    }
}
