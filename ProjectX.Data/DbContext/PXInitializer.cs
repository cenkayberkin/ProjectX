using ProjectX.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Data
{
    public class PXInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PXContext>
    {
        protected override void Seed(PXContext context)
        {
            var messages = new List<Message> 
            {
                new Message { Body = "Bu ilk mesaj", Tags="Okul|Ev" , Subject = string.Format("Hey {0}",DateTime.Now.ToShortTimeString()) },
                new Message { Body = "Bu ikinci mesaj", Tags="Pazar|Is" , Subject = string.Format("Hey {0}",DateTime.Now.ToShortTimeString())}
            };
            messages.ForEach(m => context.Messages.Add(m));

            var groups = new List<EmailGroup> 
            {
                new EmailGroup{ Name="Rich Customers List"},
                new EmailGroup{ Name="Poor Customers List"},
            };
            groups.ForEach(a => context.EmailGroups.Add(a));
            context.SaveChanges();

            var emails = new List<Email>
            {
                new Email { EmailGroupId = 1, Address = "ayberkincenk@hotmail.com" },
                new Email { EmailGroupId = 1, Address = "ce@yahoo.ca" },
                new Email { EmailGroupId = 1, Address = "cenkayberkin@yahoo.ca" }
            };

            emails.ForEach(a => context.Emails.Add(a));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
