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
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.IO.Image;


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

        /// <summary>
        /// Create a PDF file which contains a table.
        /// </summary>
        /// <param name="ComputerList"> List of object </param>
        /// <param name="float_parameter"> Define the width of the table relative to the available width of the page</param>
        /// <returns> A PDF File. </returns>
        public static void CreateTablePdf(List<Computer> ComputerList, float[] float_parameter)
        {

            // A writer takes a destination file (location of saved PDF) as parameter
            var writer = new PdfWriter(ProjectVariables.PDF_DEST);
            var pdf = new PdfDocument(writer);

            // Create a document
            var document = new Document(pdf, PageSize.A4.Rotate());
            document.SetMargins(20, 20, 20, 20);
            PageSize ps = PageSize.A4.Rotate();

            // Fonts
            var font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFontFamilies.HELVETICA);
            var bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

            Table table = new Table(float_parameter);

            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Logo
            Image logo = GenerateSmartlightLogo(ps);

            // HEADER: Logo & Title
            Paragraph par = new Paragraph();
            par.Add(logo).SetTextAlignment(TextAlignment.RIGHT);
            document.Add(par);
            par = new Paragraph().Add("LISTE ORDINATEUR").SetFont(bold).SetFontSize(30);
            document.Add(par);

            // Print Table header (name of model's property)
            foreach (String colname in Computer.GetPropertiesInFrench())
            {
                par = new Paragraph(colname);
                table.AddHeaderCell(new Cell().Add(par).SetFont(bold));
            }

            // Print each row
            foreach (Computer computer in ComputerList)
            {
                foreach (string value in ModelUtils.GetModelPropertiesValue(computer))
                {
                    par = new Paragraph(value);
                    table.AddCell(new Cell().Add(par).SetFont(font));
                }
            }

            document.Add(table);

            // Pagination
            AddNumberOfPages(document, pdf, ps);

            document.Close(); // Close and save document
        }

        public static void CreateTablePdf(List<Item> ItemList, float[] float_parameter)
        {

            // A writer takes a destination file (location of saved PDF) as parameter
            var writer = new PdfWriter(ProjectVariables.PDF_DEST);
            var pdf = new PdfDocument(writer);

            // Create a document
            var document = new Document(pdf, PageSize.A4.Rotate());
            document.SetMargins(20, 20, 20, 20);
            PageSize ps = PageSize.A4.Rotate();

            // Fonts
            var font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFontFamilies.HELVETICA);
            var bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

            Table table = new Table(float_parameter);

            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Logo
            Image logo = GenerateSmartlightLogo(ps);

            // HEADER: Logo & Title
            Paragraph par = new Paragraph();
            par.Add(logo).SetTextAlignment(TextAlignment.RIGHT);
            document.Add(par);
            par = new Paragraph().Add("LISTE ACCESSOIRES").SetFont(bold).SetFontSize(30);
            document.Add(par);

            // Print Table header (name of model's property)
            foreach (String colname in Item.GetPropertiesInFrench())
            {
                par = new Paragraph(colname);
                table.AddHeaderCell(new Cell().Add(par).SetFont(bold));
            }

            // Print each row
            foreach (Item item in ItemList)
            {
                foreach (string value in ModelUtils.GetModelPropertiesValue(item))
                {
                    par = new Paragraph(value);
                    table.AddCell(new Cell().Add(par).SetFont(font));
                }
            }

            document.Add(table);

            // Pagination
            AddNumberOfPages(document, pdf, ps);
            
            // Close and save document
            document.Close();
        }

        public static void CreateTablePdf(List<Raspberry> RaspberryList, float[] float_parameter)
        {

            // A writer takes a destination file (location of saved PDF) as parameter
            var writer = new PdfWriter(ProjectVariables.PDF_DEST);
            var pdf = new PdfDocument(writer);

            // Create a document
            var document = new Document(pdf, PageSize.A4.Rotate());
            document.SetMargins(20, 20, 20, 20);
            PageSize ps = PageSize.A4.Rotate();

            // Fonts
            var font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFontFamilies.HELVETICA);
            var bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

            Table table = new Table(float_parameter);

            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Logo
            Image logo = GenerateSmartlightLogo(ps);

            // HEADER: Logo & Title
            Paragraph par = new Paragraph();
            par.Add(logo).SetTextAlignment(TextAlignment.RIGHT);
            document.Add(par);
            par = new Paragraph().Add("LISTE RASPBERRY").SetFont(bold).SetFontSize(30);
            document.Add(par);

            // Print Table header (name of model's property)
            foreach (String colname in Raspberry.GetPropertiesInFrench())
            {
                par = new Paragraph(colname);
                table.AddHeaderCell(new Cell().Add(par).SetFont(bold));
            }

            // Print each row
            foreach (Raspberry raspberry in RaspberryList)
            {
                foreach (string value in ModelUtils.GetModelPropertiesValue(raspberry))
                {
                    par = new Paragraph(value);
                    table.AddCell(new Cell().Add(par).SetFont(font));
                }
            }

            document.Add(table);

            // Pagination
            AddNumberOfPages(document, pdf, ps);

            document.Close(); // Close and save document
        }

        public static void CreateTablePdf(List<User> UserList, float[] float_parameter)
        {

            // A writer takes a destination file (location of saved PDF) as parameter
            var writer = new PdfWriter(ProjectVariables.PDF_DEST);
            var pdf = new PdfDocument(writer);

            // Create a document
            PageSize ps = PageSize.A4.Rotate();
            var document = new Document(pdf, ps);
            document.SetMargins(20, 20, 20, 20);

            // Fonts
            var font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFontFamilies.HELVETICA);
            var bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

            Table table = new Table(float_parameter);

            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Logo
            Image logo = GenerateSmartlightLogo(ps);

            // HEADER: Logo & Title
            Paragraph par = new Paragraph();
            par.Add(logo).SetTextAlignment(TextAlignment.RIGHT);
            document.Add(par);
            par = new Paragraph().Add("LISTE UTILISATEUR").SetFont(bold).SetFontSize(30);
            document.Add(par);

            // Print Table header (name of model's property)
            foreach (String colname in User.GetPropertiesInFrench())
            {
                par = new Paragraph(colname);
                table.AddHeaderCell(new Cell().Add(par).SetFont(bold));
            }

            // Print each row
            foreach (User user in UserList)
            {
                foreach (string value in ModelUtils.GetModelPropertiesValue(user))
                {
                    par = new Paragraph(value);
                    table.AddCell(new Cell().Add(par).SetFont(font));
                }
            }

            document.Add(table);

            // Pagination
            AddNumberOfPages(document, pdf, ps);

            // Close and save document
            document.Close(); 
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

        public static Image GenerateSmartlightLogo(PageSize ps)
        {
            string logo_dest = ProjectVariables.LOGO_DEST;
            float logo_width = ps.GetWidth() / 4;
            return new Image(ImageDataFactory.Create(logo_dest)).SetWidth(logo_width);
        }

        /// <summary>
        /// Loop to every pages of a finished PDF and add a paragraph at the footer of each one at an absolute position.
        /// </summary>
        /// <param name="document"> iText.Layout.Document object </param>
        /// <param name="pdf_document"> iText.Kernel.Pdf.PdfDocument object </param>
        /// <param name="ps"> iText.Kernel.Geom.PageSize object </param>
        public static void AddNumberOfPages(Document doc, PdfDocument pdf_document, PageSize ps)
        {
            int nb_pages = pdf_document.GetNumberOfPages();
            float x_position = ps.GetWidth() / 2;
            float y_position = ps.GetHeight() / 64 ;
            Paragraph footer;
            for (int i = 1; i <= nb_pages; i++)
            {
                footer = new Paragraph($"Page {i} sur {nb_pages}");
                doc.ShowTextAligned(footer, x_position, y_position, i, TextAlignment.CENTER, VerticalAlignment.MIDDLE, 0);
            }
        }

    }
}