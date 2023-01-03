using System;
using System.IO;
using System.Web;
using System.Web.Hosting; // Return correct path
using System.Collections.Generic;
using System.Linq;

namespace AppInventaire.Utils
{
    public class ProjectVariables
    {
        // Get absolute path
        // C:\Program Files (x86)\IIS Express: "E:\\Santatra\\ASP.NET\\AppInventaire\\Results\\iText.pdf"
        public static string PDF_DEST = HostingEnvironment.MapPath("\\Results\\temp_pdf.pdf");
        public static string LOGO_DEST = HostingEnvironment.MapPath("\\Content\\img\\logo_dark.png");
        public static string BASE_DIR = HostingEnvironment.MapPath("https://localhost:44389/");
    }
}