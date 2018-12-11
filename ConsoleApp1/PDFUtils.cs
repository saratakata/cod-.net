using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurich.SantanderConsumer.Utils
{
    /**
     * Classe de utilidades.
     * 
     */
    class PDFUtils
    {
        /**
         * Método que faz a leitura de um arquivo de imagem e retorna um array de bytes
         * 
         */
        public static byte[] readImage(String fullPath)
        {
            // abre o ponteiro do arquivo
            FileInfo fileInfo = new FileInfo(fullPath);

            // cria o array de bytes
            byte[] data = new byte[fileInfo.Length];

            // lê a imagem do inputstream para o array de bytes
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }
            return data;
        }



    }
}
