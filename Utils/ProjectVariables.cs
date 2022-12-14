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
        // C:\Program FIles (x86)\IIS Express: "E:\\Santatra\\ASP.NET\\AppInventaire\\Results\\temp_pdf.pdf"
        public static string PDF_DEST = HostingEnvironment.MapPath("\\Results\\temp_pdf.pdf");
    }
}