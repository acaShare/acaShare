using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.BLL.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ModeratorPanelController : Controller
    {
        private readonly IModeratorPanelService _moderatorPanelService;

        public ModeratorPanelController(IModeratorPanelService moderatorPanelService)
        {
            _moderatorPanelService = moderatorPanelService;
        }

        [HttpPost]
        public void AddUniversity(University university)
        {
            _moderatorPanelService.AddUniversity(university);
        }
    }
}