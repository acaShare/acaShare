using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace acaShare.BLL.Models
{
    public static class FileFormat
    {
        public static class Text
        {
            public const string Txt = "text/plain"; 
            public const string Ics = "text/calendar";
            public const string Html = "text/html";
            public const string Csv = "text/csv";
            public const string Css = "text/css";
        }

        public static class Application
        {
            public const string Pdf = "application/pdf";
            public const string Doc = "application/msword";
            public const string Docx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            public const string Rtf = "application/rtf";
            public const string Xls = "application/vnd.ms-excel";
            public const string Xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string Zip = "application/zip";
            public const string Json = "application/json";
            public const string Xml = "application/xml";
            public const string Javascript = "application/javascript";
            public const string Ppt = "application/vnd.ms-powerpoint";
            public const string Pptx = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            public const string Rar = "application/x-rar-compressed";
            public const string Sh = "application/x-sh";
            public const string Typescript = "application/typescript";
            public const string Vsd = "application/vnd.visio";
            public const string Xhtml = "application/xhtml+xml";
            public const string SevenZip = "application/x-7z-compressed";
            public const string Jar = "application/java-archive";
            public const string Epub = "application/epub+zip";
            public const string Azw = "application/vnd.amazon.ebook";
        }

        public static class Image
        {
            public const string Jpeg = "image/jpeg";
            public const string Jpg = "image/jpeg";
            public const string Png = "image/png";
            public const string Tiff = "image/tiff";
            public const string Gif = "image/gif";
            public const string Svg = "image/svg+xml";
            public const string Ico = "image/x-icon";
            public const string Bmp = "image/bmp";
        }
    }
}
