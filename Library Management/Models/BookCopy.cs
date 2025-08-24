using System;

namespace Library_Management.Models
{
    public class BookCopy
    {
        public Guid CopyId { get; set; } = Guid.NewGuid();
        public Guid BookId { get; set; }
        public string? Condition { get; set; }
        public string? Source { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? PulloutDate { get; set; }
        public string? PulloutReason { get; set; }
    }
}
