using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Transactions
    {
        protected string reason;
        protected int idTransaction;
        protected string worker;
        protected int amount;
        protected CashBox cashbox;

        public string Reason { get { return reason; } set {  reason = value; } }
        public CashBox CashBox { get { return cashbox; } set { cashbox = value; } }
        public int IdTransaction { get {  return idTransaction; } set {  idTransaction = value; } }
        public string Worker { get { return worker; } set { worker = value; } }
        public int Amount { get { return amount; }
            set
            {
                amount = value;
            } }
        public Transactions()
        {

        }
    }
}