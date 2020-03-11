using System.Collections.Generic;

namespace acaShare.WebAPI.Models.StructureTraversal
{
    public class ListViewModel<TListItemViewModel>
    {
        public ICollection<TListItemViewModel> Items { get; set; }
        public bool IsWithSubtitles { get; set; }
        public int HelperId { get; set; }
    }
}
