using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CashBox
    {
        protected double incomes;
        protected double outcomes;
        protected double totalAmount;
        protected int idCashbox;


        public double Incomes { get { return incomes; } set { incomes = value; } }
        public double Outcomes {  get { return outcomes; } set { outcomes = value; } }
        public double TotalAmount { get { return totalAmount; } set { totalAmount = value; } }
        public int IdCashbox { get { return idCashbox; } set { idCashbox = value;  } }
        public CashBox() { }
        public override string ToString()
        {
            return IdCashbox.ToString();
        }
    }
}
