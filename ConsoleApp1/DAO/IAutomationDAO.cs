using System;
using System.Data.Common;

namespace Automation.Base
{
    /*
     * Implemnetar aqui os contratos (interfaces)
     * 
     * o que for declarado aqui, as classes que a implementarem
     * serão obrigadas a escrever os métodos.
     */
    interface IAutomationDAO
    {
        void Close();

        string GetError();

        DbCommand ExecuteQuery(String sql);


    }
}
