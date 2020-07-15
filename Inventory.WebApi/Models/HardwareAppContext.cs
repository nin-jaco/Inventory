namespace Inventory.WebApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HardwareAppContext : DbContext, IHardwareAppContext
    {
        public HardwareAppContext()
            : base("name=HardwareAppContext")
        {
        }

        public virtual DbSet<Hardware> Hardwares { get; set; }

        public void MarkAsModified(Hardware item)
        {
            Entry(item).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hardware>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Hardware>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Hardware>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Hardware>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Hardware>()
                .Property(e => e.PurchasePrice)
                .HasPrecision(10, 2);
        }
    }
}
