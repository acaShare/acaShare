using acaShare.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace acaShare.WebAPI.Models
{
    public class ModeratorManagementViewModel
    {
        public IdentityUser[] Administrators { get; set; }

        public IdentityUser[] MainModerators { get; set; }

        public IdentityUser[] Moderators { get; set; }

        public IdentityUser[] Members { get; set; }

        public IList<University> Universities { get; set; }

        public IList<MainModeratorAppIdIdentityIdViewModel> UniversitiesMainModeratorsWithIdentityId { get; set; }

        public int UniversityId { get; set; }

        public string UserId { get; set; }

        public DisplayUserViewModel DefaultUser => new DisplayUserViewModel();
    }
}
