using acaShare.MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace acaShare.MVC.Areas.Moderator.Models
{
    public class DisplayUserViewModel
    {
        [Display(Name = "Id użytkownika")]
        public int UserId { get; set; }

        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Rola")]
        public string Roles { get; }
    }
}
