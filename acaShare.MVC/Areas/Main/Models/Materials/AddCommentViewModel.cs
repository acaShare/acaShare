using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class AddCommentViewModel
    {
        [Required(ErrorMessage = "Uzupełnij treść komentarza przed wysłaniem")]
        [MaxLength(512, ErrorMessage = "Maksymalna długość komentarza to {1} znaków")]
        public string NewComment { get; set; }

        [Required(ErrorMessage = "Coś poszło nie tak przy wykonywaniu zapytania. Spróbuj ponownie.")]
        [Range(1, int.MaxValue, ErrorMessage = "Coś poszło nie tak przy wykonywaniu zapytania. Spróbuj ponownie.")]
        public int MaterialId { get; set; }
    }
}
