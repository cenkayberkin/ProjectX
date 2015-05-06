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
    [EnableCorsAttribute("*", "*", "*")]
    public class EmailsController : ApiController
    {
        private PXContext db;

        public EmailsController(PXContext context)
        {
            db = context;
        }

        [EnableQuery()]
        [ResponseType(typeof(Email))]
        public IHttpActionResult Get()
        {
            try
            {

                var emails = db.Emails
                    .Include(a => a.EmailGroups)
                    .Select(a => new
                         {
                             a.Id,
                             a.Address,
                             EmailGroups = a.EmailGroups.Select(x => new
                             {
                                 x.Id,
                                 x.Name
                             }),
                             NumGroups = a.EmailGroups.Count()
                         })
                 .AsQueryable();

                return Ok(emails);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Emails/5
        [ResponseType(typeof(Email))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                object email;

                if (id > 0)
                {
                    email = db.Emails.FirstOrDefault(a => a.Id == id);

                    if (email == null)
                    {
                        return NotFound();
                    }

                    email = db.Emails
                         .Include(a => a.EmailGroups)
                         .Where(a => a.Id == id)
                         .Select(a => new
                         {
                             a.Id,
                             a.Address,
                             EmailGroups = a.EmailGroups.Select(x => new
                             {
                                 x.Id,
                                 x.Name
                             }),
                             NumGroups = a.EmailGroups.Count()
                         });

                }
                else
                {
                    email = new Email();
                }

                return Ok(email);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        

        // POST: api/Emails
        [ResponseType(typeof(Email))]
        public IHttpActionResult PostEmail(int EmailGroup, Email email)
        {
            try
            {
                if (email == null)
                {
                    return BadRequest("Email cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var selectedGroup = db.EmailGroups.FirstOrDefault(a => a.Id == EmailGroup);
                if (selectedGroup == null)
                {
                    return Conflict();
                }

                selectedGroup.Emails.Add(email);
                db.SaveChanges();

                return Created<Email>(Request.RequestUri + email.Id.ToString(), email);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // DELETE: api/Emails/5
        [ResponseType(typeof(Email))]
        public IHttpActionResult DeleteEmail(int id)
        {
            Email email = db.Emails.Find(id);
            if (email == null)
            {
                return NotFound();
            }

            db.Emails.Remove(email);
            db.SaveChanges();

            return Ok(email);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmailExists(int id)
        {
            return db.Emails.Count(e => e.Id == id) > 0;
        }
    }
}