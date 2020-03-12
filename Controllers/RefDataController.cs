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
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class RefDataController : ApiController
    {
        private DBEntities db = new DBEntities();

		[Route("api/RefData/GetData")]
		[HttpGet]
		public Object GetData()
		{
			List<tbl_Category> retLst = new List<tbl_Category>();
			foreach (tbl_Category obj in db.tbl_Category)
			{
				retLst.Add(obj);
			}
			return retLst;
		}

		// GET: api/RefData/5
		[ResponseType(typeof(tbl_Category))]
        public IHttpActionResult Gettbl_Category(int id)
        {
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            if (tbl_Category == null)
            {
                return NotFound();
            }

            return Ok(tbl_Category);
        }

        // PUT: api/RefData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Category(int id, tbl_Category tbl_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Category.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_CategoryExists(id))
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

        // POST: api/RefData
        [ResponseType(typeof(tbl_Category))]
        public IHttpActionResult Posttbl_Category(tbl_Category tbl_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Category.Add(tbl_Category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Category.CategoryID }, tbl_Category);
        }

        // DELETE: api/RefData/5
        [ResponseType(typeof(tbl_Category))]
        public IHttpActionResult Deletetbl_Category(int id)
        {
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            if (tbl_Category == null)
            {
                return NotFound();
            }

            db.tbl_Category.Remove(tbl_Category);
            db.SaveChanges();

            return Ok(tbl_Category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_CategoryExists(int id)
        {
            return db.tbl_Category.Count(e => e.CategoryID == id) > 0;
        }
    }
}