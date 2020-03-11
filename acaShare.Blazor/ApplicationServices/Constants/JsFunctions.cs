using System;

namespace acaShare.Blazor.ApplicationServices.Constants
{
    public static class JsFunctions
    {
        private const string _prefix = "JsFunctions";

        public static readonly string InitializeJs = CreateAbsoluteIdentifier("initializeJs");
        public static readonly string InitializeNotifications = CreateAbsoluteIdentifier("initializeNotifications");
        public static readonly string SetDocumentTitle = CreateAbsoluteIdentifier("setDocumentTitle");
        public static readonly string OpenModal = CreateAbsoluteIdentifier("openModal");
        private static string CreateAbsoluteIdentifier(string v)
        {
            return $"{_prefix}.{v}";
        }
    }
}
