using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Base
{
    class TestaDAO
    {
        /**
         * Apenas para teste do DAO
         */
        static void Main(string[] args)
        {
            //isso deve ir na classe "context" como um parâmetro para cada banco
            //e no final virar um item no arquivo property
            String connectionString = "Data Source=10.121.248.180:1521/dbthmgm;User ID=starkatadb;Password=stzur18";
            AutomationDAO dao = new AutomationDAO(connectionString);

            dao.SearchItem("25753828");
            Console.WriteLine("bla");
            Console.ReadKey();
        }
    }
}
