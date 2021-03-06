﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MainPanelController : ControllerBase
    {
        private readonly IMainPanelService _mainPanelService;

        public MainPanelController(IMainPanelService mainPanelService)
        {
            _mainPanelService = mainPanelService;
        }

        [HttpGet]
        public ActionResult<MainPanelViewModel> Index()
        {
            var availableUniversities = _mainPanelService.GetAvailableUniversities();

            var vm = new MainPanelViewModel
            {
                Universities = availableUniversities
            };

            return vm;
        }
    }
}