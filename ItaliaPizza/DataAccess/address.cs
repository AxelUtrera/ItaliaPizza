//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class address
    {
        public int idAddress { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string neighborhood { get; set; }
        public string instructions { get; set; }
        public int idCustomer { get; set; }
    
        public virtual customer customer { get; set; }
        public virtual customer customer1 { get; set; }
    }
}
