using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Base
{
    /**
     * Fábrica de conexões de Banco de Dados da Sara 
     */
    class DACFactory : DbProviderFactory
    {
        //objetos da conexão
        private static string connectionString { get; set; } = null;
        private static DbConnection connection { get; set; } = null;
        private static Exception error { get; set; } = null;

        //Define o Driver
        private static System.Data.Common.DbProviderFactory factory { get; set; } = null;

        /**
          * Método que cria as conexões.
          * 
          * @connectionType tipo da conexão
          * @connectionString String com os dados de conexão
          * 
          * Exemplo:
          * 
          * Oracle:
          * User Id=user;Password=password;Data Source=database
          * 
          * MySQL:
          * Persist Security Info=False;server=localhost;database=Cadastro;uid=root;pwd=xxxx
          * 
          * MSSQL (SQL Server):
          * Server=localhost;Database=DevAberto;UID=usuario;PWD=senha;Connect Timeout=40
          *
          * DB2:
          * Server=localhost;Database=DEVA;UID=usuario;PWD=senha;Connect Timeout=40
          *
          * Nâo foram testadas as conexões DB2 e MSSQL (ajustar e utilizar quando necessário)
          *
          * 
          * *************************
          *        A V I S O
          * *************************
          * Quando for utilizar o DB2 e MSSQL será necessário configurar no arquivo App.config
          * 
          * os itens a seguir, adicionando os novos:
          * 
          * 
          *  <entityFramework>
          *     <providers>
          *       <provider invariantName="MySql.Data.MySqlClient" type="MySql.Data.MySqlClient.MySqlProviderServices, MySql.Data.Entity.EF6" />
          *       <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
          *       <provider invariantName="Oracle.ManagedDataAccess.Client" type="Oracle.ManagedDataAccess.EntityFramework.EFOracleProviderServices, Oracle.ManagedDataAccess.EntityFramework, Version=6.121.2.0, Culture=neutral, PublicKeyToken=89b483f429c47342" />
          *     </providers>
          *   </entityFramework>
          *   <system.data>
          *     <DbProviderFactories>
          *       <remove invariant="Oracle.ManagedDataAccess.Client" />
          *       <add name="ODP.NET, Managed Driver" invariant="Oracle.ManagedDataAccess.Client" description="Oracle Data Provider for .NET, Managed Driver" type="Oracle.ManagedDataAccess.Client.OracleClientFactory, Oracle.ManagedDataAccess, Version=4.122.18.3, Culture=neutral, PublicKeyToken=89b483f429c47342" />
          *       <remove invariant="MySql.Data.MySqlClient" />
          *       <add name="MySQL Data Provider" invariant="MySql.Data.MySqlClient" description=".Net Framework Data Provider for MySQL" type="MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data, Version=6.3.7.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d" />
          *       <remove invariant="System.Data.SqlServerCe.4.0" />
          *       <add name="Microsoft SQL Server Compact Data Provider 4.0" invariant="System.Data.SqlServerCe.4.0" description=".NET Framework Data Provider for Microsoft SQL Server Compact" type="System.Data.SqlServerCe.SqlCeProviderFactory, System.Data.SqlServerCe, Version=4.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" />          *     </DbProviderFactories>
          *   </system.data>
          *  
          */
        public static DbConnection GetConnection(ConnetionType connectionType, String connectionString)
        {
            factory = new DACFactory();
            Boolean isValid = false;
            try
            {
                switch (connectionType)
                {
                    case ConnetionType.Oracle:
                        Console.WriteLine("Selected DB Oracle.");

                        //testar talvez System.Data.Odbc
                        //ODAC 12c
                        //"Oracle.DataAccess.Client"
                        factory = DbProviderFactories.GetFactory("Oracle.ManagedDataAccess.Client");
                        isValid = true;
                        break;
                    case ConnetionType.MySQL:
                        Console.WriteLine("Selected DB MySQL.");

                        //MySQL
                        factory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
                        isValid = true;
                        break;
                    case ConnetionType.SQLServer:
                        Console.WriteLine("Selected DB SQL Server.");

                        //ADO NET Nativo - MSSQL Server
                        factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                        isValid = true;
                        break;
                    case ConnetionType.DB2:
                        Console.WriteLine("Selected DB DB2.");

                        //IBM DATA Server Provider
                        factory = DbProviderFactories.GetFactory("IBM.Data.DB2");
                        isValid = true;
                        break;
                    default:
                        Console.WriteLine("DataBase type not selected.");
                        Console.WriteLine("DB Type Not Available.");
                        isValid = false;
                        break;
                }

                if (isValid)
                {
                    connection = factory.CreateConnection();
                    connection.ConnectionString = connectionString;
                    connection.Open();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection Error:");
                Console.WriteLine(ex);
                error = ex;
            }
            return connection;
        }

        /*
         * Recupera o Driver
         */
        public static DbProviderFactory GetFactory()
        {
            return factory;
        }

        /*
         * Recupera a Mensagem de Erro
         */
        public static Exception GetError()
        {
            return error;
        }
    }       
}
