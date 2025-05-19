using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public required string Name { get; set; }

        [Required]
        [Range(1, 100)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;

        [ValidateNever]
        public Genre? Genre { get; set; } //navigation property

        public int GenreId { get; set; } // foreign key
        public DateOnly ReleaseDate { get; set; }
    }
}
