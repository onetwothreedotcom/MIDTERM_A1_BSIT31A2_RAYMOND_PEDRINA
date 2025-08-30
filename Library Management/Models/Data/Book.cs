using System;
using System.Collections.Generic;

namespace Library_Management.Models.Data;

public partial class Book
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public DateTime PublishedDate { get; set; }

    public bool IsArchived { get; set; }

    public string AuthorName { get; set; } = null!;

    public string CoverImageUrl { get; set; } = null!;
}
