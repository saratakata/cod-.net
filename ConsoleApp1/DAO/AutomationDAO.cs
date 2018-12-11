using System;
using System.Data.Common;

namespace Automation.Base
{
    /**
     * Classe DAO da Automação. 
     */
    class AutomationDAO : IAutomationDAO
    {
        //Declarando os objetos
        private DbConnection connection {get; set; } = null;
        private Exception error { get; set; } = null;

        /**
         * Poderá ser alterado no futuro para receber por 
         * parâmetro o tipo da conexão e a string de conexão,
         * no momento não faz sentido e por isso está fixo o tipo
         * e apenas a string é recebida.
         */
        public AutomationDAO(String connectionString)
        {
            try {
                this.connection = DACFactory.GetConnection(
                    ConnetionType.Oracle, connectionString);
            }catch{
                error = DACFactory.GetError();
            }
        }

        /**
         * Modelo de Select.
         */
        public Modelo SearchItem(string NumCalculo)
        {
            //Criando o objeto
            Modelo modelo = new Modelo();

            //String sql = "SELECT * FROM modelo WHERE name = " + name;
            //String sql = "SELECT mod_id, ndt_id FROM model WHERE model = 'LALALALALA'";
            /*String sql = "SELECT  p.c03 Corretor, al.c06 TipoEndosso, p.policy_no NumeroCalculoTIA, "
                         + "P.timestamp Timestamp, al.transaction_cause TransactionCasue, "
                         + "case al.transaction_cause "
                         + "when '102' then "
                         + " '102 MTA with Additional premium' "
                         + "when '103' then "
                         + " '103 MTA with Premium refund' "
                         + "when '108' then "
                         + " '108 MTA with no change' "
                       + "end as Type_of_transaction "
                  + "from policy p, agreement_line al "
                  + "where al.policy_no = ' " + NumCalculo + "' "
                  + "and al.trans_id = p.trans_id "
                  + " and p.transaction_type = 'M' "
                  + " and p.c11 = 'Y' "
                  + " order by to_number(p.c03) ";
                  */
            String sql = "SELECT D06 as Data_Referencia,TIMESTAMP, policy_no_alt, RENEWAL_DATE, " +
                         "COVER_START_DATE,COVER_END_DATE, PAYMENT_METHOD, HANDLER, C03 " +
                         "FROM POLICY WHERE POLICY_NO = '" + NumCalculo + "' and newest = 'Y'";
            Console.WriteLine("sql => " + sql);  Console.ReadKey();
            try
            {
                //executor
                DbCommand cmd = ExecuteQuery(sql);

                //Cria o set de dados
                DbDataReader data = cmd.ExecuteReader();

                //populano o retorno
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        modelo.name = data.GetValue(0).ToString();
                        //modelo.value = data.GetString(1);

                        Console.WriteLine("RETORNO1="+ modelo.name);
                        //Console.WriteLine("product_line_id=" + (data.GetOrdinal("C06")));
                        //Console.WriteLine("version=" + data.GetOrdinal("PRICE_PAID"));
                        //Console.WriteLine("description=" + data.GetOrdinal("SHORT_DESC"));
                        //Console.WriteLine("RETORNO1=" + data.GetValue(data.GetOrdinal("RECORD_TIMESTAMP")));
                    }
                }
            }
            catch (Exception ex)
            {
                //Retorna o erro
                error = ex;
            }
            finally
            {
                Close();
            }
            return modelo;
        }

        /**
         * Modelo de Insert.
         */
        public bool InsertItem(Modelo modelo)
        {
            //SQL
            String sql = "INSERT INTO Modelo VALUES ("+modelo.name+",'"+modelo.value+"')";

            try{
                //executor
                DbCommand cmd = ExecuteQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                //Retorna o erro
                error = ex;
            }
            finally
            {
                Close();
            }
            return false;
        }

        /**
         * Modelo de Update.
         */
        public bool UpdateModelo(Modelo modelo)
        {
            //SQL
            String sql = "UPDATE Modelo SET " +
                "name = " + modelo.name + ", " +
                "value = '" + modelo.value + "')" +
                "WHERE name = " + modelo.name;

            try {
                //executor
                DbCommand cmd = ExecuteQuery(sql);
                return true;
            }catch (Exception ex){

                //Retorna erro
                error = ex;
            }
            return false;
        }

        /**
         * Modelo de Delete.
         */
        public bool deleteModelo(Modelo modelo)
        {
            //SQL
            String sql = "DELETE FROM Modelo WHERE name="+modelo.name;

            try{
                //executor
                DbCommand cmd = ExecuteQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                // Retorna erro
                error = ex;
            }
            finally
            {
                Close();
            }
            return false;
        }

        /*
         * Executor genérico das operações no banco de dados. 
         * 
         * @sql Comando sql a ser executado.
         * 
         * retorna NULL se os dados passados forem inválidos
         */
        public DbCommand ExecuteQuery(String sql)
        {
            if (connection != null && sql !=null && !sql.Equals(""))
            {
                //Cria a Conexão do Driver
                DbCommand cmd = DACFactory.GetFactory().CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = sql;
                //executa
                cmd.ExecuteNonQuery();
                return cmd;
            }
            else
            {
                return null;
            }
        }

        /*
         * Retorna o erro
         */
        public string GetError()
        {
            return error.ToString();
        }

        /*
         * Fecha a conexão com o DB;
         */
        public void Close()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
