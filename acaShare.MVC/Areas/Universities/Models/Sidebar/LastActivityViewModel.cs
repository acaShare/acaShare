﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Universities.Models.Sidebar
{
    public class LastActivityVM
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string When { get; set; }
        public LastActivityType Type { get; set; }
    }

    public enum LastActivityType
    {
        MATERIAL_ADD, COMMENT
    }
}