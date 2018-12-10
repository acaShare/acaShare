using Microsoft.AspNetCore.Identity;

namespace acaShare.MVC.Areas.Moderator.Models
{
    public class ModeratorManagementViewModel
    {
        public IdentityUser[] Administrators { get; set; }

        public IdentityUser[] MainModerators { get; set; }

        public IdentityUser[] Moderators { get; set; }

        public IdentityUser[] Members { get; set; }

        public DisplayUserViewModel DefaultUser => new DisplayUserViewModel();
    }
}
