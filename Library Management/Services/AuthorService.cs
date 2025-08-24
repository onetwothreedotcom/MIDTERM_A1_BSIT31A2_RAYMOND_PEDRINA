using System;
using System.Collections.Generic;
using System.Linq;
using Library_Management_Domain.Entities;
using BookEntity = Library_Management_Domain.Entities.Book;
using AuthorEntity = Library_Management_Domain.Entities.Author;
using AuthorModel = Library_Management.Models.Author;

namespace Library_Management.Services
{
    public class AuthorService
    {
        private static readonly AuthorService _instance = new AuthorService();
        public static AuthorService Instance => _instance;

        private readonly BookService _bookService = BookService.Instance;
        private readonly List<AuthorModel> _authors = new List<AuthorModel>();

        private AuthorService()
        {
            SeedData();
        }

        private void SeedData()
        {
            var sampleAuthors = new List<AuthorModel>
            {
                new AuthorModel
                {
                    AuthorId = Guid.NewGuid(),
                    Name = "George Orwell",
                    Biography = "Eric Arthur Blair, known by his pen name George Orwell, was an English novelist, essayist, journalist, and critic. His work is characterised by lucid prose, social criticism, opposition to totalitarianism, and support of democratic socialism.",
                    BirthDate = new DateTime(1903, 6, 25),
                    ProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7e/George_Orwell_press_photo.jpg"
                },
                new AuthorModel
                {
                    AuthorId = Guid.NewGuid(),
                    Name = "J.K. Rowling",
                    Biography = "Joanne Rowling, known by her pen name J. K. Rowling, is a British author and philanthropist. She wrote Harry Potter, a seven-volume children's fantasy series published from 1997 to 2007.",
                    BirthDate = new DateTime(1965, 7, 31),
                    ProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/5d/J._K._Rowling_2010.jpg"
                },
                new AuthorModel
                {
                    AuthorId = Guid.NewGuid(),
                    Name = "Harper Lee",
                    Biography = "Nelle Harper Lee was an American novelist best known for her 1960 novel To Kill a Mockingbird. It won the 1961 Pulitzer Prize and has become a classic of modern American literature.",
                    BirthDate = new DateTime(1926, 4, 28),
                    ProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/c/c9/Harper_Lee_%282962187889%29_%28cropped%29.jpg"
                },
                new AuthorModel
                {
                    AuthorId = Guid.NewGuid(),
                    Name = "J.R.R. Tolkien",
                    Biography = "John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic, best known as the author of the high fantasy works The Hobbit and The Lord of the Rings.",
                    BirthDate = new DateTime(1892, 1, 3),
                    ProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d4/J._R._R._Tolkien%2C_ca._1925.jpg"
                },
                new AuthorModel
                {
                    AuthorId = Guid.NewGuid(),
                    Name = "Agatha Christie",
                    Biography = "Dame Agatha Mary Clarissa Christie was an English writer known for her sixty-six detective novels and fourteen short story collections, particularly those revolving around fictional detectives Hercule Poirot and Miss Marple.",
                    BirthDate = new DateTime(1890, 9, 15),
                    ProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/c/cf/Agatha_Christie.png"
                }
            };

            _authors.AddRange(sampleAuthors);
        }

        public List<AuthorModel> GetAuthors() => _authors.Where(a => !a.IsArchived).ToList();
        public List<AuthorModel> GetArchivedAuthors() => _authors.Where(a => a.IsArchived).ToList();
        public AuthorModel GetAuthorById(Guid id) => _authors.FirstOrDefault(a => a.AuthorId == id);

        public void AddAuthor(AuthorModel author) => _authors.Add(author);

        public void UpdateAuthor(AuthorModel updated)
        {
            var author = GetAuthorById(updated.AuthorId);
            if (author != null)
            {
                author.Name = updated.Name;
                author.Biography = updated.Biography;
                author.BirthDate = updated.BirthDate;
                author.ProfileImageUrl = updated.ProfileImageUrl;
            }
        }

        public void DeleteAuthor(Guid id)
        {
            var author = GetAuthorById(id);
            if (author != null)
            {
                // Check if author has books associated
                if (author.Books.Count > 0)
                {
                    // Consider archiving instead of deleting if there are books
                    throw new InvalidOperationException("Cannot delete author with associated books. Consider archiving instead.");
                }
                _authors.RemoveAll(a => a.AuthorId == id);
            }
        }

        public void ArchiveAuthor(Guid id)
        {
            var author = GetAuthorById(id);
            if (author != null) author.IsArchived = true;
        }

        public void RestoreAuthor(Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.AuthorId == id && a.IsArchived);
            if (author != null) author.IsArchived = false;
        }

        public List<Library_Management_Domain.Entities.Book> GetBooksByAuthor(Guid authorId)
        {
            var author = GetAuthorById(authorId);
            if (author == null) return new List<Library_Management_Domain.Entities.Book>();

            // Get books from BookService that match this author's name
            var books = _bookService.GetBooks(includeArchived: true)
                .Where(b => b.AuthorName?.Equals(author.Name, StringComparison.OrdinalIgnoreCase) == true)
                .Select(b => new Library_Management_Domain.Entities.Book
                {
                    Id = b.BookId,
                    Title = b.Title ?? string.Empty,
                    ISBN = b.ISBN ?? string.Empty,
                    Description = b.Description ?? string.Empty,
                    Genre = b.Genre ?? string.Empty,
                    PublishedDate = b.PublishedDate,
                    IsArchived = b.IsArchived,
                    AuthorName = b.AuthorName ?? string.Empty,
                    CoverImageUrl = b.CoverImageUrl ?? string.Empty
                })
                .ToList();

            return books;
        }
    }
}
