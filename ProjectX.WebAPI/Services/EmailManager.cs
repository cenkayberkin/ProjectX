using ProjectX.Data;
using ProjectX.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace ProjectX.WebAPI.Services
{
    public class EmailManager
    {
        private PXContext db;
        private SesService service;
        public EmailManager(PXContext context, SesService mailService)
        {
            db = context;
            service = mailService;
        }

        public void StartSendingEmails(int emailGroup, int messageId)
        {
            var message = db.Messages.FirstOrDefault(a => a.Id == messageId);

            var delivery = new Delivery()
            {
                GroupId = emailGroup,
                MessageId = messageId,
                Status = DeliveryStatus.Sending,
                Time = DateTime.Now
            };
            db.Deliveries.Add(delivery);
            db.SaveChanges();

            var group = db.EmailGroups.Include(a => a.Emails).FirstOrDefault(a => a.Id == emailGroup);
            foreach (var item in group.Emails)
            {
                db.Jobs.Add(new Job()
                {
                    DeliveryId = delivery.Id,
                    Status = JobStatus.Pending,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Title = item.Title,
                    EmailAddress = item.Address
                });
            }

            db.SaveChanges();

            var jobs = db.Jobs.Where(a => a.DeliveryId == delivery.Id);

            foreach (var item in jobs)
            {
                //Replace first name and last name with specified fields in message.
                var result = service.SendEmail(item.EmailAddress, message);
                if (result)
                {
                    item.Status = JobStatus.Sent;
                }
                else
                {
                    item.Status = JobStatus.Error;
                }
                db.SaveChanges();
            }
            
            delivery.Status = DeliveryStatus.Finished;
            db.SaveChanges();
        }
    }
}