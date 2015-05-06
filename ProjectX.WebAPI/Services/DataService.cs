using ProjectX.Data;
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
using ProjectX.Data.Models;
using System.Web.Http.Cors;
using ProjectX.WebAPI.Services;

namespace ProjectX.WebAPI.Services
{
    public class DataService
    {
        private PXContext db;
        public DataService(PXContext context)
        {
            db = context;
        }

        public bool CopyGroupToAnother(int first , int second) 
        {
            var firstGroup = db.EmailGroups.Include(a => a.Emails).FirstOrDefault(a => a.Id == first);

            var secondGroup = db.EmailGroups.Include(a => a.Emails).FirstOrDefault(a => a.Id == second);

            if (firstGroup == null || secondGroup == null)
            {
                return false;
            }

            foreach (Email item in firstGroup.Emails)
            {
                if (!secondGroup.Emails.Any(a => a.Id == item.Id))
                {
                    secondGroup.Emails.Add(item);
                }
            }
           
            db.SaveChanges();
            return true;
        } 

    }
}