using Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents = iTextSharp.text.Document;
using System.Windows;

namespace Logic
{
    public class ItextPdf
    {
        public static string GenerateInventoryReport(List<InventoryReport> report, string filePath)
        {
            string nombrepdf = string.Format("{0}_{1}.pdf", "ReporteDeInventario_" + DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("yyyy"), DateTime.Now.Ticks);
            //string pathbase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format(filePath);
            bool exists = Directory.Exists(path);
            if (!exists)
            {
                Directory.CreateDirectory(path);
            }
            string pathcompleto = string.Format("{0}\\{1}", path, nombrepdf);
            Document doc = new Document(PageSize.LETTER);
            PdfWriter writer = PdfWriter.GetInstance(doc,
            new FileStream(pathcompleto, FileMode.Create));

            // Definición de Título del documento (Metadatos NO VISIBLE)
            doc.AddTitle("Reporte de Inventario Italia Pizza ");

            // Abrir el documento para escritura
            doc.Open();

            // Definición de la fuente base
            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);

            Paragraph header = new Paragraph();
            header.Alignment = Element.ALIGN_CENTER;
            header.SpacingAfter = 20f;

            Chunk headerText = new Chunk("REPORTE DE INVENTARIO ITALIA PIZZA");
            headerText.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16); // Establecer fuente negrita y tamaño
            header.Add(headerText);
            doc.Add(header);

            // Escribimos el encabezamiento en el documento            
            doc.Add(new Paragraph("Fecha de creacion: " + DateTime.Today.ToString("dd/MM/yyyy")));
            doc.Add(new Paragraph("Fecha de actualizacion: "));
            doc.Add(new Paragraph("Articulos totales: " + report.Count));
            doc.Add(Chunk.NEWLINE);

            // Definición de tabla
            PdfPTable tbl_Report = new PdfPTable(6);
            tbl_Report.WidthPercentage = 90;

            // Definición de cabeceras de la tabla
            PdfPCell c01 = new PdfPCell(new Phrase("Tipo", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            PdfPCell c02 = new PdfPCell(new Phrase("ID", _standardFontBold));
            c02.BorderWidth = 0;
            c02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c02.BorderWidthBottom = 0.75f;

            PdfPCell c03 = new PdfPCell(new Phrase("Nombre", _standardFontBold));
            c03.BorderWidth = 0;
            c03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c03.BorderWidthBottom = 0.75f;

            PdfPCell c04 = new PdfPCell(new Phrase("Cantidad Minima", _standardFontBold));
            c04.BorderWidth = 0;
            c04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c04.BorderWidthBottom = 0.75f;

            PdfPCell c05 = new PdfPCell(new Phrase("Cantidad registrada", _standardFontBold));
            c05.BorderWidth = 0;
            c05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c05.BorderWidthBottom = 0.75f;

            PdfPCell c06 = new PdfPCell(new Phrase("Cantidad Real", _standardFontBold));
            c06.BorderWidth = 0;
            c06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c06.BorderWidthBottom = 0.75f;

            // Añadir las celdas a la tabla
            tbl_Report.AddCell(c01);
            tbl_Report.AddCell(c02);
            tbl_Report.AddCell(c03);
            tbl_Report.AddCell(c04);
            tbl_Report.AddCell(c05);
            tbl_Report.AddCell(c06);

            foreach (var aux in report)
            {
                PdfPCell d01 = new PdfPCell(new Phrase(aux.TypeOfProduct, _standardFont));
                d01.BorderWidth = 0;
                d01.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell d02 = new PdfPCell(new Phrase(aux.IdItem, _standardFont));
                d02.BorderWidth = 0;
                d02.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell d03 = new PdfPCell(new Phrase(aux.Name, _standardFont));
                d03.BorderWidth = 0;
                d03.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell d04 = new PdfPCell(new Phrase(aux.WarningTreshold.ToString(), _standardFont));
                d04.BorderWidth = 0;
                d04.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell d05 = new PdfPCell(new Phrase(aux.Quantity.ToString(), _standardFont));
                d05.BorderWidth = 0;
                d05.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell d06 = new PdfPCell(new Phrase("", _standardFont));
                d06.BorderWidth = 0;
                d06.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                d06.BorderWidthBottom = 0.75f;

                // Añadir las celdas a la tabla
                tbl_Report.AddCell(d01);
                tbl_Report.AddCell(d02);
                tbl_Report.AddCell(d03);
                tbl_Report.AddCell(d04);
                tbl_Report.AddCell(d05);
                tbl_Report.AddCell(d06);
            }

            // Añadir la tabla al documento
            doc.Add(tbl_Report);

            //Cerrar documento y writer
            doc.Close();
            writer.Close();

            return pathcompleto;
        }
        public static string ModifyReport(List<InventoryReport> report, string filePath)
        {
            FileInfo archivoInfo = new FileInfo(filePath);
            DateTime fechaCreacion = archivoInfo.CreationTime;
            string fecha = fechaCreacion.ToString("dd/MM/yyyy");
            Document doc = new Document();

            // Abre el documento para escritura
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                // Modifica el contenido del PDF con la información actualizada
                // ...

                // Definición de la fuente base
                Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);
                Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);

                Paragraph header = new Paragraph();
                header.Alignment = Element.ALIGN_CENTER;
                header.SpacingAfter = 20f;

                Chunk headerText = new Chunk("REPORTE DE INVENTARIO ITALIA PIZZA");
                headerText.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16); // Establecer fuente negrita y tamaño
                header.Add(headerText);
                doc.Add(header);

                // Escribimos el encabezamiento en el documento            
                doc.Add(new Paragraph("Fecha de creacion: " + fecha));
                doc.Add(new Paragraph("Fecha de actualizacion: " + DateTime.Today.ToString("dd/MM/yyyy")));
                doc.Add(new Paragraph("Articulos totales: " + report.Count));
                doc.Add(Chunk.NEWLINE);

                // Definición de tabla
                PdfPTable tbl_Report = new PdfPTable(6);
                tbl_Report.WidthPercentage = 90;

                // Definición de cabeceras de la tabla
                PdfPCell c01 = new PdfPCell(new Phrase("Tipo", _standardFontBold));
                c01.BorderWidth = 0;
                c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                c01.BorderWidthBottom = 0.75f;

                PdfPCell c02 = new PdfPCell(new Phrase("ID", _standardFontBold));
                c02.BorderWidth = 0;
                c02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                c02.BorderWidthBottom = 0.75f;

                PdfPCell c03 = new PdfPCell(new Phrase("Nombre", _standardFontBold));
                c03.BorderWidth = 0;
                c03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                c03.BorderWidthBottom = 0.75f;

                PdfPCell c04 = new PdfPCell(new Phrase("Cantidad Minima", _standardFontBold));
                c04.BorderWidth = 0;
                c04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                c04.BorderWidthBottom = 0.75f;

                PdfPCell c05 = new PdfPCell(new Phrase("Cantidad registrada", _standardFontBold));
                c05.BorderWidth = 0;
                c05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                c05.BorderWidthBottom = 0.75f;

                PdfPCell c06 = new PdfPCell(new Phrase("Cantidad Real", _standardFontBold));
                c06.BorderWidth = 0;
                c06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                c06.BorderWidthBottom = 0.75f;

                // Añadir las celdas a la tabla
                tbl_Report.AddCell(c01);
                tbl_Report.AddCell(c02);
                tbl_Report.AddCell(c03);
                tbl_Report.AddCell(c04);
                tbl_Report.AddCell(c05);
                tbl_Report.AddCell(c06);

                foreach (var aux in report)
                {
                    PdfPCell d01 = new PdfPCell(new Phrase(aux.TypeOfProduct, _standardFont));
                    d01.BorderWidth = 0;
                    d01.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell d02 = new PdfPCell(new Phrase(aux.IdItem, _standardFont));
                    d02.BorderWidth = 0;
                    d02.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell d03 = new PdfPCell(new Phrase(aux.Name, _standardFont));
                    d03.BorderWidth = 0;
                    d03.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell d04 = new PdfPCell(new Phrase(aux.WarningTreshold.ToString(), _standardFont));
                    d04.BorderWidth = 0;
                    d04.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell d05 = new PdfPCell(new Phrase(aux.Quantity.ToString(), _standardFont));
                    d05.BorderWidth = 0;
                    d05.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell d06 = new PdfPCell(new Phrase(aux.RealQuantity.ToString(), _standardFont));
                    d06.BorderWidth = 0;
                    d06.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                    // Añadir las celdas a la tabla
                    tbl_Report.AddCell(d01);
                    tbl_Report.AddCell(d02);
                    tbl_Report.AddCell(d03);
                    tbl_Report.AddCell(d04);
                    tbl_Report.AddCell(d05);
                    tbl_Report.AddCell(d06);
                }

                // Añadir la tabla al documento
                doc.Add(tbl_Report);

                //Cerrar documento y writer
                doc.Close();
                writer.Close();

                return filePath;
            }

        }
    }
}
