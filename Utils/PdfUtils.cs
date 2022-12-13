using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using AppInventaire.Models;

// iText7 Namespace
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Html2pdf;


namespace AppInventaire.Utils
{
    public class PdfUtils
    {
        static string HeadHtml = $"<head><link rel=\"stylesheet\" type=\"text/css\" href=\"~/Content/css/style.css\"/>" +
               $"<link rel=\"stylesheet\" type=\"text/css\" href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css\"/>" +
               $"<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.13.2/themes/base/jquery-ui.min.css\"/>" +
               $"<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css\"/>" +
               $"</head>";

        public static void StringToPdf(string str, string dest)
        {
            PdfWriter writer = new PdfWriter(dest); // Creer un writer pour pouvoir ouvrir un document en mode écriture
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph page = new Paragraph(str);
            document.Add(page);
            document.Close();
        }

        public static string GenerateHtmlDetails(List<String> col_value_list, List<String> col_name_list)
        {
            string TitleHtml = "<h1> Detail </h1><div>";
            string BodyHtml = "<dl class=\"dl-horizontal\">";
            for(int i=0; i<col_value_list.Count; i++)
            {
                BodyHtml += $"<dt>{col_name_list[i]}</dt>";
                BodyHtml += $"<dd>{col_value_list[i]}</dd>";
            };
            BodyHtml += "</dl></div>";
            return HeadHtml + TitleHtml + BodyHtml;
        }

        public static string GenerateHtmlTable(List<Computer> ComputerList, List<String> col_name_list)
        {
            string outputHtml = "<table class=\"table table-striped\"><thead class=\"thead-dark\"><tr>";
            foreach(string col_name in col_name_list)
            {
                outputHtml += $"<th>{col_name}</th>";
            }
            outputHtml += "</tr></thead><tbody>";
            foreach (var computer in ComputerList)
            {
                outputHtml += $"<tr><th>{computer.ID}</th>" +
                    $"<td> {computer.Hostname}</td>" +
                    $"<td> {computer.Brand}</td>" +
                    $"<td> {computer.Model} </td>" +
                    $"<td> {computer.OS} </td>" +
                    $"<td> {computer.Processor}</td>" +
                    $"<td> {computer.RamGB}</td>" +
                    $"<td> {computer.GraphCard}</td>" +
                    $"<td> {computer.GraphCardGB}</td><td></td></tr>";
            }
            outputHtml += "</tbody></table>";
            return HeadHtml + outputHtml;
        }
        
        public static void CreatePdf(string html_string, string destination_path)
        {
            // Create File
            HtmlConverter.ConvertToPdf(html_string, new FileStream(destination_path, FileMode.Create));
            
        }

        

    }
}