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
            var messages = new List<EmailMessage> 
            {
                new EmailMessage { Body = "Bu birinci mesaj", Tags="Okul|Ev" , Subject = string.Format("Hey {0}",DateTime.Now.ToShortTimeString()) },
                new EmailMessage { Body = "Bu ikinci mesaj", Tags="Pazar|Is" , Subject = string.Format("Hey {0}",DateTime.Now.ToShortTimeString())}
            };
            
            messages.ForEach(m => context.Messages.Add(m));

            EmailGroup a1 = new EmailGroup { Name = "Istanbul Musterileri"};
            EmailGroup a2 = new EmailGroup { Name = "Ankara Musterileri" };
            EmailGroup a3 = new EmailGroup { Name = "Yeni Musteriler" };

            List<Email> mails = new List<Email> {};
            Email tmp = null;

            Random e = new Random();
            
            for (int i = 0; i < 70; i++)
            {

                tmp = new Email() 
                {
                    Address = e.Next(10) < 7 ? "success@simulator.amazonses.com" : "bounce@simulator.amazonses.com",
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Title = Faker.Name.Prefix()
                };
                mails.Add(tmp);
            }

            for (int i = 0; i < 30; i++)
            {

                tmp = new Email()
                {
                    Address = e.Next(10) < 5 ? "success@simulator.amazonses.com" : "complaint@simulator.amazonses.com",
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Title = Faker.Name.Prefix()
                };
                mails.Add(tmp);
            }

            mails.ForEach(a => context.Emails.Add(a));
            context.SaveChanges();

            var firstGroup = mails.Take(50).ToList();
            firstGroup.ForEach(a => a1.Emails.Add(a));

            var secondGroup = mails.Skip(50).Take(50).ToList();
            secondGroup.ForEach(a => a2.Emails.Add(a));

            context.EmailGroups.AddRange(new List<EmailGroup>()
            {
                a1,a2,a3
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
