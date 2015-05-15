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
using System.Web;
using ProjectX.WebAPI.Services;

namespace ProjectX.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:1863", "*", "*")]
    public class EmailsController : ApiController
    {
        private PXContext db;
        private DataService dservice;

        public EmailsController(PXContext context,DataService service)
        {
            db = context;
            dservice = service;
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
                             a.Title,
                             a.FirstName,
                             a.LastName,
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

        [Route("api/emails/uploadfile/{groupId}")]
        [HttpPost]
        public IHttpActionResult UploadMails(int groupId)
        {
            IHttpActionResult result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var postedFile = httpRequest.Files[0];
                
                var filePath = HttpContext.Current.Server.MapPath("~/tempFiles/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
                
                if (dservice.ProcessUploadedFile(groupId,filePath) == true) {
                    result = Ok();
                }
                else
                {
                    result = Conflict();
                }
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }

        // DELETE: api/Emails/5

        [Route("api/emails/{groupid}/{id}")]
        [ResponseType(typeof(Email))]
        public IHttpActionResult DeleteEmail(int groupid, int id)
        {
            var group = db.EmailGroups.Include(a => a.Emails).FirstOrDefault(a => a.Id == groupid);
            
            Email email = db.Emails.Find(id);
            
            if (email == null)
            {
                return NotFound();
            }

            if (!group.Emails.Contains(email))
            {
                return NotFound();
            }

            group.Emails.Remove(email);

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