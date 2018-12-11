using System;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Zurich.SantanderConsumer.Utils
{
    /// <summary>
    /// Classe para concatenação de arquivos PDF. 
    /// Utiliza a biblioteca iTextSharp.
    /// </summary>
    /// 
    /// PdfMerge merge = new PdfMerge();
    /// merge.Add("primeiro.pdf");
    /// merge.Add("pasta/segundo.pdf");
    /// merge.Add("terceiro.pdf");
    /// merge.Save("concatenado.pdf");
    /// 
    public class PDFMerge
    {
        /// <summary>
        /// Lista de arquivos a serem concatenados
        /// </summary>
        private List<string> pdfList;

        /// <summary>
        /// Objeto que representa um documento (pdf) do iTextSharp
        /// </summary>
        private Document document;

        /// <summary>
        /// Objeto responsável por salvar o pdf em disco.
        /// </summary>
        private PdfWriter writer;

        /// <summary>
        /// Construtor
        /// </summary>
        public PDFMerge()
        {
            pdfList = new List<string>();
        }

        /// <summary>
        /// Adiciona o arquivo que será concatenado ao PDF final.
        /// </summary>
        /// Caminho para o arquivo PDF
        public void Add(string filePath)
        {
            pdfList.Add(filePath);
        }

        /// <summary>
        /// Adiciona uma lista de arquivos pdf para serem concatenados.
        /// </summary>
        /// Lista contendo o caminho para os arquivos
        public void AddList(List<string> files)
        {
            pdfList.AddRange(files);
        }

        /// <summary>
        /// Concatena os arquivos de entrada, gerando um novo arquivo PDF.
        /// </summary>
        public void Save(string pathToDestFile)
        {
            PdfReader reader = null;
            PdfContentByte cb = null;
            int index = 0;
            try
            {
                // Percorre a lista de arquivos a serem concatenados.
                foreach (string file in pdfList)
                {
                    // Cria o PdfReader para ler o arquivo
                    reader = new PdfReader(pdfList[index]);
                    // Obtém o número de páginas deste pdf
                    int numPages = reader.NumberOfPages;

                    if (index == 0)
                    {
                        // Cria o objeto do novo documento
                        document = new Document(reader.GetPageSizeWithRotation(1));
                        // Cria um writer para gravar o novo arquivo
                        writer = PdfWriter.GetInstance(document, new FileStream(pathToDestFile, FileMode.Create));
                        // Abre o documento
                        document.Open();
                        cb = writer.DirectContent;
                    }

                    // Adiciona cada página do pdf origem ao pdf destino.
                    int i = 0;
                    while (i < numPages)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();
                        PdfImportedPage page = writer.GetImportedPage(reader, i);
                        int rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        else
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    }
                    index++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (document != null)
                    document.Dispose();
            }
        }
    }
}