using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.WebApi.Models;

namespace Inventory.WebApi.Tests.CrudTests
{
    public class TestHardwareAppContext : IHardwareAppContext
    {
        public TestHardwareAppContext()
        {
            this.Hardwares = new TestHardwareDbSet();
        }

        public DbSet<Hardware> Hardwares { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Hardware item) { }
        public void Dispose() { }
    }
}
