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
using System.Web;
using System.IO;

namespace ProjectX.WebAPI.Services
{
    public class DataService
    {
        private PXContext db;
        public DataService(PXContext context)
        {
            db = context;
        }

        public bool CopyGroupToAnother(int first, int second)
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

        public bool ProcessUploadedFile(int groupId, string uploadedFile)
        {
            using(var reader = new StreamReader(File.OpenRead(uploadedFile)))
            {
                Email tmp = null;
                var group = db.EmailGroups.FirstOrDefault(a => a.Id == groupId);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    string address = values[0];
                    var email = db.Emails.Include(a => a.EmailGroups).FirstOrDefault(a => a.Address == address);
                    
                    if (email == null)
                    {
                        tmp = new Email { Address = values[0], FirstName = values[1], LastName = values[2], Title = values[3] };
                        group.Emails.Add(tmp);
                    }
                    else
                    {
                        if(!email.EmailGroups.Any(g => g.Id == groupId)){
                            group.Emails.Add(email);
                        }
                    }
                }
            };

            File.Delete(uploadedFile);
            
            db.SaveChanges();
            return true;
        }
    }
}