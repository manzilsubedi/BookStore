using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class BookModel
    {

        public int Id { get; set; }


        [StringLength(100, MinimumLength =3)]
        [Required(ErrorMessage ="Enter Title of the Book")]
        public string Title { get; set; }
        [Required (ErrorMessage ="Enter Author of the Book")]
        public string Author { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string CoverPhotoUrl { get; set; }

        [Required (ErrorMessage ="Enter Total Pages in the Book")]
        [Display(Name ="Total Pages")]
        public int? TotalPages { get; set; }

        [Display(Name ="Insert Cover Photo")]
        [Required]
        public IFormFile CoverPhoto { get; set; }


        [Display(Name = "Insert Sample Pictures of the book")]
        [Required]
        public IFormFileCollection SamplePhoto { get; set; }


        public List<PhotosModel> Photos { get; set; }

    }
}
