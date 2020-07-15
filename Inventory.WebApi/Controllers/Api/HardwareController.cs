using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Inventory.WebApi.Models;

namespace Inventory.WebApi.Controllers.Api
{
    public class HardwareController : ApiController
    {
        private IHardwareAppContext _hardwareAppContext = new HardwareAppContext();

        public HardwareController()
        {
            
        }

        public HardwareController(IHardwareAppContext hardwareAppContext)
        {
            _hardwareAppContext = hardwareAppContext;
        }

        // GET: api/Hardware
        public IQueryable<Hardware> GetHardware()
        {
            return _hardwareAppContext.Hardwares.AsQueryable();
        }

        // GET: api/Hardware/5
        [ResponseType(typeof(Hardware))]
        public IHttpActionResult GetHardware(int id)
        {
            Hardware hardware = _hardwareAppContext.Hardwares.Find(id);
            if (hardware == null)
            {
                return NotFound();
            }

            return Ok(hardware);
        }

        // PUT: api/Hardware/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHardware(int id, Hardware hardware)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hardware.Id)
            {
                return BadRequest();
            }

            _hardwareAppContext.MarkAsModified(hardware);

            try
            {
                _hardwareAppContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HardwareExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Hardware
        [ResponseType(typeof(Hardware))]
        public IHttpActionResult PostHardware(Hardware hardware)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _hardwareAppContext.Hardwares.Add(hardware);
            _hardwareAppContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hardware.Id }, hardware);
        }

        // DELETE: api/Hardware/5
        [ResponseType(typeof(Hardware))]
        public IHttpActionResult DeleteHardware(int id)
        {
            Hardware hardware = _hardwareAppContext.Hardwares.Find(id);
            if (hardware == null)
            {
                return NotFound();
            }

            _hardwareAppContext.Hardwares.Remove(hardware);
            _hardwareAppContext.SaveChanges();

            return Ok(hardware);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hardwareAppContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HardwareExists(int id)
        {
            return _hardwareAppContext.Hardwares.Count(e => e.Id == id) > 0;
        }
    }
}