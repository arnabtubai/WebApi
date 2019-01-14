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
using AssignWebApi.Models;

namespace AssignWebApi.Controllers
{
    public class PODETAILsController : ApiController
    {
        private ProdSuppModel db = new ProdSuppModel();

        // GET: api/PODETAILs
        public IQueryable<PODETAIL> GetPODETAILs()
        {
            return db.PODETAILs;
        }

        // GET: api/PODETAILs/5
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult GetPODETAIL(string id, string itemId)
        {
            PODETAIL pODETAIL = db.PODETAILs.Find(id, itemId);
            if (pODETAIL == null)
            {
                return NotFound();
            }

            return Ok(pODETAIL);
        }

        // PUT: api/PODETAILs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPODETAIL(string id, string itemId, PODETAIL pODETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pODETAIL.PONO.Trim() && itemId != pODETAIL.ITCODE.Trim())
            {
                return BadRequest();
            }

            db.Entry(pODETAIL).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PODETAILExists(id.Trim(), itemId.Trim()))
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

        // POST: api/PODETAILs
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult PostPODETAIL(PODETAIL pODETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PODETAILs.Add(pODETAIL);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PODETAILExists(pODETAIL.PONO.Trim(), pODETAIL.ITCODE.Trim()))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pODETAIL.PONO, itemId = pODETAIL.ITCODE }, pODETAIL);
        }

        // DELETE: api/PODETAILs/5
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult DeletePODETAIL(string id, string itemId)
        {
            PODETAIL pODETAIL = db.PODETAILs.Find(id.Trim(), itemId.Trim());
            if (pODETAIL == null)
            {
                return NotFound();
            }

            db.PODETAILs.Remove(pODETAIL);
            db.SaveChanges();

            return Ok(pODETAIL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PODETAILExists(string id, string itemId)
        {
            return db.PODETAILs.Count(e => e.PONO.Trim().ToUpper() == id.Trim().ToUpper() && e.ITCODE.Trim().ToUpper() == itemId.Trim().ToUpper()) > 0;
        }
    }
}
