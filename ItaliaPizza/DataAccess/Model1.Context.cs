﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ItaliaPizzaEntities : DbContext
    {
        public ItaliaPizzaEntities()
            : base("name=ItaliaPizzaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<address> address { get; set; }
        public virtual DbSet<cashbox> cashbox { get; set; }
        public virtual DbSet<customer> customer { get; set; }
        public virtual DbSet<dailyBalance> dailyBalance { get; set; }
        public virtual DbSet<deliveryOrder> deliveryOrder { get; set; }
        public virtual DbSet<ingredient> ingredient { get; set; }
        public virtual DbSet<orderProduct> orderProduct { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<recipe> recipe { get; set; }
        public virtual DbSet<recipeIngredient> recipeIngredient { get; set; }
        public virtual DbSet<supplier> supplier { get; set; }
        public virtual DbSet<supplierOrder> supplierOrder { get; set; }
        public virtual DbSet<transactions> transactions { get; set; }
        public virtual DbSet<unitOfMeasurement> unitOfMeasurement { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<worker> worker { get; set; }
        public virtual DbSet<supplierIngredient> supplierIngredient { get; set; }
        public virtual DbSet<supplierProduct> supplierProduct { get; set; }
    }
}
