using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class TransactionsLogic
    {
        public static bool takeOutCash(Model.Transactions newTransaction)
        {
            bool result = false;
            using (var context = new ItaliaPizzaEntities())
            {
                try
                {

                    var transaction = new DataAccess.transactions
                    {
                        idCashbox=newTransaction.CashBox.IdCashbox,
                        amount=newTransaction.Amount,
                        worker=newTransaction.Worker,
                        reason=newTransaction.Reason,                        
                    };  
                    context.transactions.Add(transaction);
                    context.SaveChanges();
                    result = true;
                }catch (EntityException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }
    }
}
