using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurich.SantanderConsumer.Utils
{
    /**
    * Classe que representa o relatório.
    */
    public class Report
    {
        public string projectName { get; set; } = "";
        public string sprint { get; set; } = "";
        public string status { get; set; } = "";
        public string testCase { get; set; } = "";
        public string userStories { get; set; } = "";
        public string execDate { get; set; } = "";
        public string reportDirectory { get; set; } = "";
        public List<Step> steps { get; set; } = new List<Step>();

    }
}
