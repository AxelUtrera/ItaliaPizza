using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CashBoxLogic
    {
        public static List<CashBox> GetCashBoxes()
        {
            List<CashBox> cashBoxes = new List<CashBox>();
            using (var context = new ItaliaPizzaEntities())
            {
                try
                {
                    var foundCashBoxes = context.cashbox.ToList();
                    if (foundCashBoxes != null)
                    {
                        foreach (var aux in foundCashBoxes)
                        {
                            CashBox cashBox = new CashBox
                            {
                                TotalAmount = aux.totalAmount,
                                Incomes = aux.incomes,
                                Outcomes = aux.outcomes,
                                IdCashbox = aux.idCashbox,
                            };
                            cashBoxes.Add(cashBox);
                        }                        
                    }
                }
                catch (EntityException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return cashBoxes;
        }
    }
}
