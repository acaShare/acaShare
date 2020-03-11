namespace acaShare.WebAPI.Models.StructureTraversal
{
    public interface IListItemViewModel
    {
        int Id { get; set; }
        string TitleOrFullName { get; set; }
        string SubtitleOrAbbreviation { get; set; }
    }
}
