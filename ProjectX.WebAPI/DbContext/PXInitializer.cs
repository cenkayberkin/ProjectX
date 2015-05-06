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

            EmailGroup a1 = new EmailGroup { Name = "A1"};
            EmailGroup a2 = new EmailGroup { Name = "A2" };
            
            Email m1 = new Email() { Address="cenkay@gg.com"};
            Email m2 = new Email() { Address = "sss@gg.com" };
            Email m3 = new Email() { Address = "aaaa@gg.com2" };

            a1.Emails.Add(m1);
            a1.Emails.Add(m2);
            a1.Emails.Add(m3);

            a2.Emails.Add(m1);

            context.EmailGroups.AddRange(new List<EmailGroup>()
            {
                a1,a2
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
