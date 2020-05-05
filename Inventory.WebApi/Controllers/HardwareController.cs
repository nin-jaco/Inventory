using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Inventory.WebApi;

namespace Inventory.WebApi.Controllers
{
    public class HardwareController : ApiController
    {
        private HardwareInventoryEntities db = new HardwareInventoryEntities();

        // GET: api/Hardware
        public IQueryable<Hardware> GetHardware()
        {
            return db.Hardwares;
        }

        // GET: api/Hardware/5
        [ResponseType(typeof(Hardware))]
        public IHttpActionResult GetHardware(int id)
        {
            Hardware hardware = db.Hardwares.Find(id);
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

            db.Entry(hardware).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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

            db.Hardwares.Add(hardware);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hardware.Id }, hardware);
        }

        // DELETE: api/Hardware/5
        [ResponseType(typeof(Hardware))]
        public IHttpActionResult DeleteHardware(int id)
        {
            Hardware hardware = db.Hardwares.Find(id);
            if (hardware == null)
            {
                return NotFound();
            }

            db.Hardwares.Remove(hardware);
            db.SaveChanges();

            return Ok(hardware);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HardwareExists(int id)
        {
            return db.Hardwares.Count(e => e.Id == id) > 0;
        }
    }
}