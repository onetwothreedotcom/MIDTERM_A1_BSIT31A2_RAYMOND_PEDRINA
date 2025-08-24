using Library_Management_Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class Author
    {
        public Guid AuthorId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Author name is required")]
        [Display(Name = "Author Name")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Biography")]
        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters")]
        public string Biography { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth date is required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Profile Image URL")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string ProfileImageUrl { get; set; } = string.Empty;

        public bool IsArchived { get; set; } = false;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
