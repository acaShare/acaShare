using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories
{
    public interface IMaterialRepository : IRepository<Material>
    {
        void AddDeleteRequest(DeleteRequest deleteRequest);
        void AddEditRequest(EditRequest editRequest);
        ICollection<Material> GetMaterialsToApprove();
        Material GetMaterialToApprove(int materialId);
        ICollection<ChangeReason> GetChangeReasons(ChangeType changeType);
        ICollection<DeleteRequest> GetDeleteRequests(RequestState requestState);
        DeleteRequest GetDeleteRequest(int deleteRequestId);
        void UpdateDeleteRequest(DeleteRequest deleteRequest);
        ICollection<EditRequest> GetEditRequests();
        EditRequest GetEditRequest(int editRequestId);
        void DeleteEditRequest(EditRequest editRequest);
        void UpdateEditRequest(EditRequest editRequest);
    }
}
