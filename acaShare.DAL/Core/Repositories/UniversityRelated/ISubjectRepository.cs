﻿using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories.UniversityRelated
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Subject GetByNaming(string name, string abbreviation);
    }
}
