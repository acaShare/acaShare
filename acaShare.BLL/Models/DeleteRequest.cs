using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class DeleteRequest
    {
        public int DeleteRequestId { get; set; }
        
        public int Reason { get; set; }

        public int MaterialToDeleteId { get; set; }
        public Material Material { get; set; }

        public int DeleterId { get; set; }
        public User User { get; set; }
    }
}
