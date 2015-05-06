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
using System.Web.Http.Cors;
using ProjectX.WebAPI.Services;

namespace ProjectX.WebAPI.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class EmailGroupsController : ApiController
    {
        private PXContext db;
        private DataService dService;

        public EmailGroupsController(PXContext context,DataService services)
        {
            db = context;
            dService = services;
        }

        // GET: api/EmailGroups
        public IHttpActionResult Get()
        {
            try
            {
                var groups = db.EmailGroups
                        .Include(a => a.Emails)
                        .Select(a => new
                        {
                            a.Id,
                            a.Name,
                            emails = a.Emails.Select(e => new
                            {
                                e.Id,
                                e.Address
                            }),
                            NumEmails = a.Emails.Count()
                        })
                     .ToList();

                return Ok(groups);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/EmailGroups/5
        [ResponseType(typeof(EmailGroup))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var emailGroup = db.EmailGroups.FirstOrDefault(a => a.Id == id);
                if (emailGroup == null)
                {
                    return NotFound();
                }

                var group = db.EmailGroups.Where(a => a.Id == id)
                    .Include(a => a.Emails)
                    .Select(a => new
                    {
                        a.Id,
                        a.Name,
                        emails = a.Emails.Select(e => new
                        {
                            e.Id,
                            e.Address
                        }),
                        NumGroups = a.Emails.Count()
                    });

                return Ok(group);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/emailgroups/{firstGroup}/copygroups/{secondGroup}")]
        [HttpGet]
        public IHttpActionResult CopyGroups(int firstGroup, int secondGroup)
        {
            //combine two email groups.
            try
            {
                if (dService.CopyGroupToAnother(firstGroup,secondGroup)) 
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/EmailGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmailGroup(int id, EmailGroup emailGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emailGroup.Id)
            {
                return BadRequest();
            }

            db.Entry(emailGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailGroupExists(id))
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

        // POST: api/EmailGroups
        [ResponseType(typeof(EmailGroup))]
        public IHttpActionResult PostEmailGroup(EmailGroup emailGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmailGroups.Add(emailGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = emailGroup.Id }, emailGroup);
        }

        // DELETE: api/EmailGroups/5
        [ResponseType(typeof(EmailGroup))]
        public IHttpActionResult DeleteEmailGroup(int id)
        {
            EmailGroup emailGroup = db.EmailGroups.Find(id);
            if (emailGroup == null)
            {
                return NotFound();
            }

            db.EmailGroups.Remove(emailGroup);
            db.SaveChanges();

            return Ok(emailGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmailGroupExists(int id)
        {
            return db.EmailGroups.Count(e => e.Id == id) > 0;
        }
    }
}