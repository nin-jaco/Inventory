using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.WebApi.Models;

namespace Inventory.WebApi.Tests.CrudTests
{
    class TestHardwareDbSet : TestDbSet<Hardware>
    {
        public override Hardware Find(params object[] keyValues)
        {
            return this.SingleOrDefault(hardware => hardware.Id == (int)keyValues.Single());
        }
    }
}
