using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurich.SantanderConsumer.Utils
{
    /**
     * Classe responsável por criar o cabeçalho e o rodapé do pdf.
     */
    public class PDFHeaderAndFooter : PdfPageEventHelper
    {
        public Report report { get; set; }

        public PDFHeaderAndFooter(Report pReport)
        {
            report = pReport;
        }

        /**
        * Cria o cabeçalho
        */
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
            float[] columnWidths = { 5, 5, 1 };
            PdfPTable tabFot = new PdfPTable(columnWidths);

            tabFot.DefaultCell.Border = Rectangle.NO_BORDER;
            tabFot.TotalWidth = 530F;
            tabFot.SpacingAfter = 10F;
            PdfPCell cell;
            PdfPCell cellct;
            PdfPCell cellexec;
            PdfPCell cellstatus;

            //Nome do projeto
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA, 16);
            var titleText = new Phrase();
            titleText.Add(new Chunk(report.projectName, titleFont));

            cell = new PdfPCell(new Phrase(titleText));
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthRight = 0f;
            cell.Colspan = 2;
            tabFot.AddCell(cell);

            // logo
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Directory.GetCurrentDirectory() + "\\zurich2.jpg");
            PdfPCell cellLogo;
            cellLogo = new PdfPCell(logo);
            cellLogo.BorderWidthBottom = 0f;
            cellLogo.BorderWidthLeft = 0f;
            cellLogo.BorderWidthTop = 0f;
            cellLogo.BorderWidthRight = 0f;
            cellLogo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cellLogo.Rowspan = 3;
            tabFot.AddCell(cellLogo);

            //historia de usuario
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 11);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);

            var userHistory = new Phrase();
            userHistory.Add(new Chunk("História de Usuário: ", normalFont));
            userHistory.Add(new Chunk(report.userStories, boldFont));
            userHistory.Add(new Chunk(" - Sprint: ", normalFont));
            userHistory.Add(new Chunk(report.sprint, boldFont));

            PdfPCell celluh = new PdfPCell(userHistory);
            celluh.BorderWidthBottom = 0f;
            celluh.BorderWidthLeft = 0f;
            celluh.BorderWidthTop = 0f;
            celluh.BorderWidthRight = 0f;
            celluh.Colspan = 2;
            tabFot.AddCell(celluh);

            //Nome do caso de teste
            var ctText = new Phrase();
            ctText.Add(new Chunk("CT: ", normalFont));
            ctText.Add(new Chunk(report.testCase, normalFont));

            cellct = new PdfPCell(new Phrase(ctText));
            cellct.BorderWidthBottom = 0f;
            cellct.BorderWidthLeft = 0f;
            cellct.BorderWidthTop = 0f;
            cellct.BorderWidthRight = 0f;
            cellct.Colspan = 2;
            tabFot.AddCell(cellct);

            //data execucao
            var execText = new Phrase();
            execText.Add(new Chunk("Data Execução: ", normalFont));
            execText.Add(new Chunk(report.execDate, normalFont));

            cellexec = new PdfPCell(new Phrase(execText));
            cellct.BorderWidthBottom = 0f;
            cellct.BorderWidthLeft = 0f;
            cellct.BorderWidthTop = 0f;
            cellct.BorderWidthRight = 0f;
            cellexec.BorderColor = new BaseColor(System.Drawing.Color.Black);
            cellexec.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
            cellexec.BorderWidthBottom = 1f;
            tabFot.AddCell(cellexec);

            //status
            var statusText = new Phrase();
            statusText.Add(new Chunk("Status: ", normalFont));
            statusText.Add(new Chunk(report.status, boldFont));

            cellstatus = new PdfPCell(new Phrase(statusText));
            cellstatus.BorderColor = new BaseColor(System.Drawing.Color.Black);
            cellstatus.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
            cellstatus.BorderWidthBottom = 1f;
            cellstatus.Colspan = 2;
            tabFot.AddCell(cellstatus);

            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));

            tabFot.WriteSelectedRows(0, -1, 30, document.Top, writer.DirectContent);
        }

        /**
        * Cria a página
        */
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        /**
        * Cria o espaço do rodapé
        */
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            PdfPCell cell;
            tabFot.TotalWidth = 300F;
            cell = new PdfPCell(new Phrase(""));
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthRight = 0f;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);
        }

        //escreve e fecha o documento
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}
