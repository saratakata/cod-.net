using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurich.SantanderConsumer.Utils
{
    /**
     * Classe de Steps dos Casos de Teste.
     */
    public class Step
    {
        public string title { get; set; }
        [Obsolete("Este método não está mais em uso e foi substituído por 'evidence'")]
        public string content { get; set; }
        public byte[] evidence { get; set; }
    }
}
