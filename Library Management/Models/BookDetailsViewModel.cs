public class BookDetailsViewModel
{
    public Guid BookId { get; set; }
    public string? Title { get; set; }
    public string? ISBN { get; set; }
    public string? AuthorName { get; set; }
    public string? AuthorProfileImageUrl { get; set; }
    public string? Genre { get; set; }
    public DateTime PublishedDate { get; set; }
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }

    public List<BookCopyViewModel> Copies { get; set; } = new();
}

public class BookCopyViewModel
{
    public Guid CopyId { get; set; }
    public string? Condition { get; set; }
    public string? Source { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime? PulloutDate { get; set; }
    public string? PulloutReason { get; set; }
    public bool IsPulledOut => PulloutDate.HasValue;
}
