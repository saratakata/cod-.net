using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurich.SantanderConsumer.Utils
{
    /**
     * Classe responsável por criar o relatório pdf.
     */
    class PDFBuild
    {
        /**
        * Main apenas para teste, aqui você encontra um exemplo
        * de como instanciar e utilizar a classe.
        *
        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now; // data atual
            string now = dt.ToString("dd/MM/yyyy HH:mm");

            Report report = new Report();
            report.projectName = "Projeto Santander Consumer";
            report.sprint = "3";
            report.testCase = "Validar adição de pacote na coleção de parametros de critério de restrição";
            report.userStories = "UH031";
            report.status = "Aprovado";
            report.execDate = now;
            report.reportDirectory = "C:\\Users\\adema\\source\\repos\\ConsoleApp1\\ConsoleApp1\\bin\\Debug";

            List<Step> steps =  new List<Step>();

            try
            {
                Step stp1 = new Step();
                steps.Add(stp1);
                stp1.title = "Titulo do step blablabla";
                stp1.evidence = PDFUtils.readImage("C:\\Users\\adema\\Pictures\\evd.png");
                
                Step stp2 = new Step();
                steps.Add(stp2);
                stp2.title = "Titulo do step2222";
                
                stp2.evidence = PDFUtils.readImage("C:\\Users\\adema\\Pictures\\evd.png");                             
            }
            catch (Exception ex){ }
            finally
            {
                report.steps.AddRange(steps);
                PDFBuild.build(report);
            }
        }*/

        /**
        * Método que constrói o relatório PDF conforme os parâmetros informados
        */
        public static void build(Report report)
        {
            //valida se os parâmetros básicos estão OK para gerar o relatório
            if (parametersValidate(report))
            {
                try
                {
                    //nome do arquivo a ser gerado
                    String filename = report.reportDirectory + "\\CT - " + report.testCase + ".pdf";
                    Document doc = new Document(iTextSharp.text.PageSize.A4);
                    System.IO.FileStream file = new System.IO.FileStream(filename, System.IO.FileMode.OpenOrCreate);

                    //debug - remover
                    //System.Diagnostics.Debug.WriteLine(filename);

                    PdfWriter writer = PdfWriter.GetInstance(doc, file);
                    //chama a classe que escreve o cabecalho
                    writer.PageEvent = new PDFHeaderAndFooter(report);
                    doc.Open();

                    if (report.steps != null)
                    {
                        for (var i = 0; i < report.steps.Count; i++)
                        //foreach (var step in report.steps)
                        {
                            PdfPCell content;
                            PdfPTable tab = new PdfPTable(1);
                            tab.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            tab.TotalWidth = 530F;
                            tab.SpacingAfter = 10F;
                            tab.WidthPercentage = 100;

                            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 11);
                            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
                            var fontColour = new BaseColor(30, 65, 187);
                            var contentFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, fontColour);
                            var titleText = new Phrase();
                            titleText.Add(new Chunk((i + 1) + ". ", boldFont));
                            titleText.Add(new Chunk(report.steps[i].title, boldFont));
                            PdfPCell title = new PdfPCell(titleText);
                            title.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;

                            if (report.steps[i].evidence != null)
                            {
                                //faz a leitura da evidencia
                                iTextSharp.text.Image evd_img = iTextSharp.text.Image.GetInstance(report.steps[i].evidence);
                                //evd_img.ScaleAbsolute(653f, 354f);
                                evd_img.ScaleAbsolute(523f, 254f);
                                evd_img.Alignment = Element.ALIGN_CENTER;

                                //re-insere a borda superior porque a imagem está sobrepondo ela :( .. desculpa
                                //se colocar a volta toda fica muito grosso.. feião rs
                                evd_img.Border = iTextSharp.text.Rectangle.TOP_BORDER;
                                evd_img.BorderWidth = 1.0f;
                                evd_img.BorderColor = iTextSharp.text.BaseColor.BLACK;
                                content = new PdfPCell(evd_img);
                            }
                            else
                            {
                                //apenas cria a linha da evidencia sem conteúdo
                                var contentText = new Phrase();
                                contentText.Add(new Chunk(" ", contentFont));
                                content = new PdfPCell(contentText);
                            }
                            tab.AddCell(title);
                            tab.AddCell(content);
                            doc.Add(tab);
                        }
                    }
                    doc.Close();
                    file.Close();
                }catch(Exception ex)
                {
                    //tratando com exceção genérica, muito feio, maspor hora está valendo rs
                    //Don't You Worry 'Bout a Thing :)
                    System.Diagnostics.Debug.WriteLine("Ocorreu um erro generalizado na aplicação");
                    ex.GetBaseException();
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Operação abortada de geração do relatório PDF");
            }
        }

        /**
         * Método que valida os parâmetros
         */
        private static Boolean parametersValidate(Report report)
        {
            Boolean errors = false;
            if (report != null)
            {
                if (report.projectName.Equals(""))
                {
                    System.Diagnostics.Debug.WriteLine("Nome do Projeto está vazio");
                    errors = true;
                }
                if (report.sprint.Equals(""))
                {
                    System.Diagnostics.Debug.WriteLine("Sprint está vazia");
                    errors = true;
                }
                if (report.testCase.Equals(""))
                {
                    System.Diagnostics.Debug.WriteLine("Nome do Caso de teste está vazio");
                    errors = true;
                }
                if (report.userStories.Equals(""))
                {
                    System.Diagnostics.Debug.WriteLine("Nome da História está vazia");
                    errors = true;
                }
                if (report.status.Equals(""))
                {
                    System.Diagnostics.Debug.WriteLine("Status não foi informado");
                    errors = true;
                }
                if (report.execDate.Equals(""))
                {
                    System.Diagnostics.Debug.WriteLine("Data de execução não foi informada");
                    errors = true;
                }
                if (report.reportDirectory.Equals(""))
                {
                    System.Diagnostics.Debug.WriteLine("Diretório de destino do PDF não foi informado");
                    errors = true;
                }
                if (report.steps.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("A lista de passos não foi informada");
                    errors = true;
                }
            }
            else
            {
                errors = true;
            }

            if (errors == true)
            {
                return false;
            }
            return true;
        }
    }  
}