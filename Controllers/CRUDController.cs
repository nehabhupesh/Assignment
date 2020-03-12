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
    public class CRUDController : ApiController
    {
        private DBEntities db = new DBEntities();

		[Route("api/CRUD/GetData")]
		[HttpGet]
		public Object GetData()
		{
			List<usp_GetItems_Result> retLst = new List<usp_GetItems_Result>();
			foreach (usp_GetItems_Result obj in db.usp_GetItems())
			{
				retLst.Add(obj);
			}
			return retLst;
		}

		// GET: api/CRUD/5
		[ResponseType(typeof(tbl_ContentLimitInsurance))]
        public IHttpActionResult Gettbl_ContentLimitInsurance(int id)
        {
            tbl_ContentLimitInsurance tbl_ContentLimitInsurance = db.tbl_ContentLimitInsurance.Find(id);
            if (tbl_ContentLimitInsurance == null)
            {
                return NotFound();
            }

            return Ok(tbl_ContentLimitInsurance);
        }

        // PUT: api/CRUD/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_ContentLimitInsurance(int id, tbl_ContentLimitInsurance tbl_ContentLimitInsurance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_ContentLimitInsurance.ItemID)
            {
                return BadRequest();
            }

            db.Entry(tbl_ContentLimitInsurance).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ContentLimitInsuranceExists(id))
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

		// POST: api/CRUD
		[Route("api/CRUD/AddItem")]
		[HttpPost]
        public IHttpActionResult Posttbl_ContentLimitInsurance(tbl_ContentLimitInsurance tbl_ContentLimitInsurance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			db.tbl_ContentLimitInsurance.Add(tbl_ContentLimitInsurance);
			db.SaveChanges();
			return Ok();
		}

        // DELETE: api/CRUD/5
        [ResponseType(typeof(tbl_ContentLimitInsurance))]
        public IHttpActionResult Deletetbl_ContentLimitInsurance(int id)
        {
            tbl_ContentLimitInsurance tbl_ContentLimitInsurance = db.tbl_ContentLimitInsurance.Find(id);
            if (tbl_ContentLimitInsurance == null)
            {
                return NotFound();
            }

            db.tbl_ContentLimitInsurance.Remove(tbl_ContentLimitInsurance);
            db.SaveChanges();

            return Ok(tbl_ContentLimitInsurance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ContentLimitInsuranceExists(int id)
        {
            return db.tbl_ContentLimitInsurance.Count(e => e.ItemID == id) > 0;
        }
    }
}