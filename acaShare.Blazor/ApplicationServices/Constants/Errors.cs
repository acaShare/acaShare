
namespace acaShare.Blazor.ApplicationServices.Constants
{
    public static class Errors
    {
        public const string MaterialNotFound = "Materiał o podanym id nie istnieje.";
        public static readonly string MaterialToApproveNotValid = "Materiał o podanym id nie ma statusu Oczekujący na zatwierdzenie lub nie istnieje.";
        public static readonly string DeleteSuggestionNotFound = "Sugestia usunięcia o podanym id nie istnieje.";
        public static readonly string EditSuggestionNotFound = "Sugestia edycji o podanym id nie istnieje.";
        public static readonly string UniversityNotFound = "Uczelnia o podanym id nie istnieje.";
        public static readonly string UniversityDuplicate = "Uczelnia o takiej nazwie lub skrócie już istnieje.";
    }
}
