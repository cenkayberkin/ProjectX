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
using System.Web.Http.OData;
using System.Web.Http.Cors;

namespace ProjectX.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class MessagesController : ApiController
    {
        private PXContext db;
        
        public MessagesController(PXContext context)
        {
            db = context;
        }

        [EnableQuery()]
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessages()
        {
            try
            {
                var msgs = db.Messages.AsQueryable();
                return Ok(msgs);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessage(int id)
        {
            try
            {
                object msg;

                if (id > 0)
                {
                    msg = db.Messages.FirstOrDefault(a => a.Id == id);

                    if (msg == null)
                    {
                        return NotFound();
                    }

                    msg = db.Messages.Where(a => a.Id == id).FirstOrDefault();
                    
                }
                else
                {
                    msg = new Message();
                }

                return Ok(msg);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.Id)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            try
            {
                if (message == null)
                {
                    return BadRequest("Message cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                message = db.Messages.Add(message);
                db.SaveChanges();

                return Created<Message>(Request.RequestUri + message.Id.ToString(), message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult DeleteMessage(int id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            db.SaveChanges();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.Id == id) > 0;
        }
    }
}