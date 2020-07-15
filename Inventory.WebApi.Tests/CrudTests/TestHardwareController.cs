using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Inventory.WebApi.Controllers.Api;
using Inventory.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inventory.WebApi.Tests.CrudTests
{
    [TestClass]
    public class TestHardwareController
    {
        [TestMethod]
        public void PostHardware_ShouldReturnSameHardware()
        {
            var controller = new HardwareController(new TestHardwareAppContext());

            var item = GetDemoHardware();

            var result =
                controller.PostHardware(item) as CreatedAtRouteNegotiatedContentResult<Hardware>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Description, item.Description);
            Assert.AreEqual(result.Content.PurchasePrice, item.PurchasePrice);
            Assert.AreEqual(result.Content.SerialNumber, item.SerialNumber);
            Assert.AreEqual(result.Content.Type, item.Type);
        }

        [TestMethod]
        public void PutHardware_ShouldReturnStatusCode()
        {
            var controller = new HardwareController(new TestHardwareAppContext());

            var item = GetDemoHardware();

            var result = controller.PutHardware(item.Id, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void PutHardware_ShouldFail_WhenDifferentID()
        {
            var controller = new HardwareController(new TestHardwareAppContext());

            var badresult = controller.PutHardware(999, GetDemoHardware());
            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetHardware_ShouldReturnHardwareWithSameID()
        {
            var context = new TestHardwareAppContext();
            context.Hardwares.Add(GetDemoHardware());

            var controller = new HardwareController(context);
            var result = controller.GetHardware(3) as OkNegotiatedContentResult<Hardware>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.Id);
        }

        [TestMethod]
        public void GetHardwares_ShouldReturnAllHardwares()
        {
            var context = new TestHardwareAppContext();
            context.Hardwares.Add(new Hardware { Id = 1, Description = "Demo1", PurchasePrice = 20, SerialNumber = "ABC123", Type = "Printer" });
            context.Hardwares.Add(new Hardware { Id = 2, Description = "Demo2", PurchasePrice = 30, SerialNumber = "ABC123", Type = "Screen" });
            context.Hardwares.Add(new Hardware { Id = 3, Description = "Demo3", PurchasePrice = 40, SerialNumber = "ABC123", Type = "Keyboard" });

            var controller = new HardwareController(context);
            var result = controller.GetHardware() as TestHardwareDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void DeleteHardware_ShouldReturnOK()
        {
            var context = new TestHardwareAppContext();
            var item = GetDemoHardware();
            context.Hardwares.Add(item);

            var controller = new HardwareController(context);
            var result = controller.DeleteHardware(3) as OkNegotiatedContentResult<Hardware>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Id, result.Content.Id);
        }

        Hardware GetDemoHardware()
        {
            return new Hardware() { Id = 3, Description = "Demo name", PurchasePrice = 5, SerialNumber = "ABC123", Type = "Printer"};
        }
    }
}
