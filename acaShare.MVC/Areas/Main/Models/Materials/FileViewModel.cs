using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class FileViewModel
    {
        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public string ContentType { get; set; }
        public bool IsImage => ContentType.StartsWith("image");

        public string IconFileName
        {
            get
            {
                switch (ContentType)
                {
                    // Text
                    case BLL.Models.FileFormat.Text.Css:
                        return "css.png";
                    case BLL.Models.FileFormat.Text.Txt:
                        return "txt.png";
                    case BLL.Models.FileFormat.Text.Html:
                        return "html.png";
                    case BLL.Models.FileFormat.Text.Csv:
                        return "csv.png";
                    case BLL.Models.FileFormat.Text.Ics:
                        return "ics.png";

                    // Application
                    case BLL.Models.FileFormat.Application.Pdf:
                        return "pdf.png";
                    case BLL.Models.FileFormat.Application.Doc:
                    case BLL.Models.FileFormat.Application.Docx:
                        return "doc.png";
                    case BLL.Models.FileFormat.Application.Rtf:
                        return "rtf.png";
                    case BLL.Models.FileFormat.Application.Xls:
                    case BLL.Models.FileFormat.Application.Xlsx:
                        return "xls.png";
                    case BLL.Models.FileFormat.Application.Ppt:
                    case BLL.Models.FileFormat.Application.Pptx:
                        return "ppt.png";
                    case BLL.Models.FileFormat.Application.Vsd:
                        return "vsd.png";
                    case BLL.Models.FileFormat.Application.Zip:
                        return "zip.png";
                    case BLL.Models.FileFormat.Application.Rar:
                        return "rar.png";
                    case BLL.Models.FileFormat.Application.SevenZip:
                        return "sevenZip.png";
                    case BLL.Models.FileFormat.Application.Jar:
                        return "jar.png";
                    case BLL.Models.FileFormat.Application.Javascript:
                        return "js.png";
                    case BLL.Models.FileFormat.Application.Json:
                        return "json.png";
                    case BLL.Models.FileFormat.Application.Xhtml:
                    case BLL.Models.FileFormat.Application.Xml:
                        return "xml.png";
                    case BLL.Models.FileFormat.Application.Typescript:
                        return "ts.png";
                    case BLL.Models.FileFormat.Application.Sh:
                        return "sh.png";
                    case BLL.Models.FileFormat.Application.Azw:
                    case BLL.Models.FileFormat.Application.Epub:
                        return "book.png";
                }

                // If we got there it means we have either an audio or a font or a video file
                if (ContentType.StartsWith("audio"))
                {
                    return "audio.png";
                }
                else if (ContentType.StartsWith("video"))
                {
                    return "video.png";
                }
                else if (ContentType.StartsWith("font"))
                {
                    return "font.png";
                }
                else
                {
                    return "document.png";
                }
            }
        }
    }
}
