﻿using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories
{
    public interface IMaterialRepository : IRepository<Material>
    {
        void CreateDeleteRequest(Material material);
        void CreateUpdateRequest(Material material);
        ICollection<Material> GetMaterialsToApprove();
        Material GetMaterialToApprove(int materialId);
    }
}
