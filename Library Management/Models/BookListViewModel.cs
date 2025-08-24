using Library_Management_Domain.Entities;
using System;
using System.Collections.Generic;

namespace Library_Management.Models
{
    public class BookListViewModel
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorProfileImageUrl { get; set; }
        public string? Genre { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public bool IsArchived { get; set; }

        public List<BookCopy> Copies { get; set; } = new List<BookCopy>();
    }
}
