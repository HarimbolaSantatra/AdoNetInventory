using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Reflection;
using AppInventaire.Models;
using AppInventaire.Utils;

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
        static string HeadHtml = $"<head>" +
               $"<link rel=\"stylesheet\" type=\"text/css\" href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css\"/>" +
               $"<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.13.2/themes/base/jquery-ui.min.css\"/>" +
               $"<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css\"/>" +
               $"</head>";

        public static void CreatePdf(string html_string, string destination_path)
        {
            ConverterProperties converterProperties = new ConverterProperties();
            // Create File
            HtmlConverter.ConvertToPdf(html_string, new FileStream(destination_path, FileMode.Create), converterProperties);
        }
        public static string GenerateHtmlDetails(List<String> col_value_list, List<String> property_list)
        {
            string BodyHtml = "<h1> <b> Detail </b></h1><div>";
            for (int i = 0; i < col_value_list.Count; i++)
            {
                // We should use a list here but there's an issue with the iText7 library when using some tag like <li>
                // See: https://stackoverflow.com/questions/65721048/itext-7-object-reference-not-set-to-an-instance-of-an-object
                BodyHtml += $"</br><span class=\"font-weight-bold\">• {property_list[i]} </span> : \t {col_value_list[i]}";
            };
            BodyHtml += "</div>";
            return HeadHtml + BodyHtml;
        }


        public static string GenerateHtmlTable(List<Computer> ComputerList)
        {
            PropertyInfo[] property_info_list = ModelUtils.GetModelProperties(ComputerList.First());

            string outputHtml = "<table class=\"table table-striped\"><thead class=\"thead-dark\"><tr>";

            // Table Header
            foreach (PropertyInfo propty in property_info_list)
            {
                outputHtml += $"<th>{propty.Name}</th>";
            }
            outputHtml += "</tr></thead><tbody>";

            // Table Body
            foreach (Computer computer in ComputerList)
            {
                outputHtml += $"<tr>";
                foreach(PropertyInfo propty in property_info_list)
                {
                    string current_value = propty.GetValue(computer, null) as string;
                    outputHtml += $"<td>{current_value}</td>";
                }
                
                outputHtml += "</tr>";

            }
            outputHtml += "</tbody></table>";
            return HeadHtml + outputHtml;
        }

        public static string GenerateHtmlTable(List<Item> ItemList, List<String> property_list)
        {
            PropertyInfo[] property_info_list = ModelUtils.GetModelProperties(ItemList.First());

            string outputHtml = "<table class=\"table table-striped\"><thead class=\"thead-dark\"><tr>";

            // Table Header
            foreach (PropertyInfo propty in property_info_list)
            {
                outputHtml += $"<th>{propty.Name}</th>";
            }
            outputHtml += "</tr></thead><tbody>";

            // Table Body
            foreach (Item item in ItemList)
            {
                outputHtml += $"<tr>";
                foreach (PropertyInfo propty in property_info_list)
                {
                    string current_value = propty.GetValue(item, null) as string;
                    outputHtml += $"<td>{current_value}</td>";
                }
                outputHtml += "</tr>";
            }
            outputHtml += "</tbody></table>";
            return HeadHtml + outputHtml;
        }

        public static string GenerateHtmlTable(List<Raspberry> RaspberryList)
        {
            PropertyInfo[] property_info_list = ModelUtils.GetModelProperties(RaspberryList.First());

            string outputHtml = "<table class=\"table table-striped\"><thead class=\"thead-dark\"><tr>";

            // Table Header
            foreach (PropertyInfo propty in property_info_list)
            {
                outputHtml += $"<th>{propty.Name}</th>";
            }
            outputHtml += "</tr></thead><tbody>";

            // Table Body
            foreach (Object obj in RaspberryList)
            {
                outputHtml += $"<tr>";
                foreach (PropertyInfo propty in property_info_list)
                {
                    string current_value = propty.GetValue(obj, null) as string;
                    outputHtml += $"<td>{current_value}</td>";
                }
                outputHtml += "</tr>";
            }
            outputHtml += "</tbody></table>";
            return HeadHtml + outputHtml;
        }

        public static string GenerateHtmlTable(List<User> UserList, List<String> property_list)
        {
            string outputHtml = "<table class=\"table table-striped\"><thead class=\"thead-dark\"><tr>";
            foreach (string col_name in property_list)
            {
                outputHtml += $"<th>{col_name}</th>";
            }
            outputHtml += "</tr></thead><tbody>";
            foreach (var user in UserList)
            {
                outputHtml += $"<tr><th> {user.ID} </th>" +
                    $"<td> {user.FirstName} </td>" +
                    $"<td> {user.LastName} </td>" +
                    $"<td> {user.Email} </td>" +
                    $"</tr>";
            }
            outputHtml += "</tbody></table>";
            return HeadHtml + outputHtml;
        }
       

    }
}