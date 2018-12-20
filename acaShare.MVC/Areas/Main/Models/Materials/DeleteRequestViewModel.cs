using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class DeleteRequestViewModel
    {
        [Required]
        public int MaterialId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać jedną z przyczyn")]
        [Display(Name = "Przyczyna")]
        public int ReasonId { get; set; }
        
        [MaxLength(500, ErrorMessage = "Maksymalna długość dodatkowego komentarza to {1} znaków")]
        public string AdditionalComment { get; set; }

        public string MaterialName { get; set; }
        public IList<ChangeReason> Reasons { get; set; }
    }
}
