using ProjectX.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using ProjectX.Data;

namespace ProjectX.WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        private PXContext _ctx;
        public ValuesController(PXContext context)
        {
            _ctx = context;
        }

        // GET api/values
        public IEnumerable<object> Get()
        {
            var query = _ctx.EmailGroups
                        .Include(a => a.Emails)
                        .Select(a => new
                        {
                            Name = a.Name,
                            Emails = a.Emails,
                            EmailAddresses = a.Emails.Select(x => new
                            {
                                x.Id,
                                x.Address
                            }),
                            NumberOfEmails = a.Emails.Count()
                        });

            return query;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
