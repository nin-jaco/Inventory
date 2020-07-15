using System;
using System.Data.Entity;

namespace Inventory.WebApi.Models
{
    public interface IHardwareAppContext : IDisposable
    {
        DbSet<Hardware> Hardwares { get; }
        int SaveChanges();
        void MarkAsModified(Hardware item);
    }
}