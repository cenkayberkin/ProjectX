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
using ProjectX.Data;
using ProjectX.Data.Models;
using ProjectX.WebAPI.Services;

namespace ProjectX.WebAPI.Controllers
{
    public class DeliveriesController : ApiController
    {
        private PXContext db;

        private EmailManager emailManager;

        public DeliveriesController(PXContext context, EmailManager manager)
        {
            db = context;
            emailManager = manager;
        }

        // GET: api/Deliveries
        public IQueryable<Delivery> GetDeliveries()
        {
            return db.Deliveries;
        }

        // GET: api/Deliveries/5
        [ResponseType(typeof(Delivery))]
        public IHttpActionResult GetDelivery(int id)
        {
            Delivery delivery = db.Deliveries.Find(id);
            if (delivery == null)
            {
                return NotFound();
            }

            return Ok(delivery);
        }

        // PUT: api/Deliveries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDelivery(int id, Delivery delivery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != delivery.Id)
            {
                return BadRequest();
            }

            db.Entry(delivery).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExists(id))
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

        [Route("api/delivery/{emailGroup}/{message}")]
        [HttpPost]
        public IHttpActionResult SendDelivery(int emailGroup, int message)
        {
            try
            {
                emailManager.StartSendingEmails(emailGroup, message);
                return Ok();
            }
            catch (Exception ex)
            {

                return NotFound();
            }

        }

        // DELETE: api/Deliveries/5
        [ResponseType(typeof(Delivery))]
        public IHttpActionResult DeleteDelivery(int id)
        {
            Delivery delivery = db.Deliveries.Find(id);
            if (delivery == null)
            {
                return NotFound();
            }

            db.Deliveries.Remove(delivery);
            db.SaveChanges();

            return Ok(delivery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeliveryExists(int id)
        {
            return db.Deliveries.Count(e => e.Id == id) > 0;
        }
    }
}