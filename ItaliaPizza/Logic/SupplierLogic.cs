using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class SupplierLogic
    {

        public static int RegisterSupplier(Supplier supplier)
        {
            int statusCode = 500;

                using (var database = new ItaliaPizzaEntities())
                {
                    var newSupplier = database.supplier.Add(new supplier
                    {
                        supplierName = supplier.SupplierName,
                        supplierAddress = supplier.SupplierAddress,
                        email = supplier.Email,
                        phoneNumber = supplier.PhoneNumber,
                        rfc = supplier.Rfc,
                        supplierType = supplier.SupplierType,
                        active = true
                    });

                    database.SaveChanges();

                    if (newSupplier != null)
                    {
                        statusCode = 200;
                    }
                }
            

            return statusCode;
        }


        public static int ModifySupplier(Supplier supplierToModify)
        {
            int statusCode = 500;

            using (var database = new ItaliaPizzaEntities())
            {
                var modifySupplier = database.supplier.First(i => i.idSupplier == supplierToModify.IdSupplier);
                if (modifySupplier != null)
                {
                    modifySupplier.supplierName = supplierToModify.SupplierName;
                    modifySupplier.email = supplierToModify.Email;
                    modifySupplier.phoneNumber = supplierToModify.PhoneNumber;
                    modifySupplier.rfc = supplierToModify.Rfc;
                    modifySupplier.supplierType = supplierToModify.SupplierType;
                    modifySupplier.supplierAddress = supplierToModify.SupplierAddress;
                }
                int resultObtained = database.SaveChanges();

                if (resultObtained != 0)
                {
                    statusCode = 200;
                }
            }
            return statusCode;
        }


        public static List<Supplier> RecoverActiveSuppliers()
        {
            List<Supplier> suppliersObtained = new List<Supplier>();

            using (var database = new ItaliaPizzaEntities())
            {
                var activeSuppliers = database.supplier.Where(x => x.active.Equals(true)).ToList();

                foreach (var supplier in activeSuppliers)
                {
                    Supplier recoverSupplier = new Supplier()
                    {
                        IdSupplier = supplier.idSupplier,
                        SupplierAddress = supplier.supplierAddress,
                        Email = supplier.email,
                        PhoneNumber = supplier.phoneNumber,
                        Rfc = supplier.rfc,
                        SupplierType = supplier.supplierType,
                        SupplierName = supplier.supplierName,
                        Active = supplier.active
                    };

                    suppliersObtained.Add(recoverSupplier);
                }
            }

            return suppliersObtained;
        }


        public static int DeleteSupplierById(int idSupplier)
        {
            int statusCode = 500;

            using (var database = new ItaliaPizzaEntities())
            {
                var supplierToDelete = database.supplier.First(u => u.idSupplier == idSupplier);

                if (supplierToDelete != null)
                {
                    supplierToDelete.active = false;
                    database.SaveChanges();
                    statusCode = 200;
                }
            }
            return statusCode;
        }
    }
}
