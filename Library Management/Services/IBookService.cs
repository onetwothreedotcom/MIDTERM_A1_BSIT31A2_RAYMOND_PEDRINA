using Library_Management.Models;

public interface IBookService
{
    void AddBook(AddBookViewModel book);
    void AddBookCopy(AddBookCopyViewModel vm);
    void ArchiveBook(Guid id);
    void DeleteBook(Guid id);
    BookDetailsViewModel GetBook(Guid id);
    EditBookViewModel GetBookById(Guid id);
    IEnumerable<BookListViewModel> GetBooks(bool includeArchived = false);
    bool PulloutCopy(Guid copyId, string reason);
    void RestoreBook(Guid id);
}

public class BookDBService : IBookService
{
    public void AddBook(AddBookViewModel book)
    {
        throw new NotImplementedException();
    }

    public void AddBookCopy(AddBookCopyViewModel vm)
    {
        throw new NotImplementedException();
    }

    public void ArchiveBook(Guid id)
    {
        throw new NotImplementedException();
    }

    public void DeleteBook(Guid id)
    {
        throw new NotImplementedException();
    }

    public BookDetailsViewModel GetBook(Guid id)
    {
        throw new NotImplementedException();
    }

    public EditBookViewModel GetBookById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BookListViewModel> GetBooks(bool includeArchived = false)
    {
        throw new NotImplementedException();
    }

    public bool PulloutCopy(Guid copyId, string reason)
    {
        throw new NotImplementedException();
    }

    public void RestoreBook(Guid id)
    {
        throw new NotImplementedException();
    }
}
