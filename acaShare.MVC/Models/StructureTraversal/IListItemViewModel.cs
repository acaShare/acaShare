using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Models.StructureTraversal
{
    public interface IListItemViewModel
    {
        int Id { get; set; }
        string TitleOrFullName { get; set; }
        string SubtitleOrAbbreviation { get; set; }
    }
}
