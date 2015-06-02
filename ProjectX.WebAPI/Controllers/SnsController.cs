using ProjectX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Amazon.SimpleNotificationService.Util;
using System.Web;
using System.IO;

namespace ProjectX.WebAPI.Controllers
{
    public class SnsController : ApiController
    {
        PXContext db;
        public SnsController(PXContext context)
        {
            db = context;
        }

        // GET api/values
        public string Get()
        {
            string result = "";

            var log = HttpContext.Current.Application["log"] as IList<string>;

            if (log == null)
            {
                result = "no content";
            }
            else
            {
                foreach (var message in log.Reverse())
                {
                    result += message + "\n";
                }

                //db.MyMessages.Add(new MyMessage() { Content = result });
                //db.SaveChanges();
            }

            return result;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post()
        {
            string contentBody;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream))
                contentBody = reader.ReadToEnd();

            Message message = Message.ParseMessage(contentBody);

            // Make sure message is authentic
            if (!message.IsMessageSignatureValid())
                throw new Exception("Amazon SNS Message signature is invalid");

            if (message.IsSubscriptionType)
            {
                ConfirmSubscription(message);
            }
            else if (message.IsNotificationType)
            {
                ProcessNotification( message);
            }

        }

        private void ProcessNotification(Message message)
        {
            
            //db.MyMessages.Add(new MyMessage() { Content = string.Format("{0}: Received notification from {1} with message {2}", DateTime.Now, message.TopicArn, message.MessageText) });
            //db.SaveChanges();

        }

        private void ConfirmSubscription(Message message)
        {

            try
            {
                message.SubscribeToTopic();

                //db.MyMessages.Add(new MyMessage() { Content = string.Format("Subscription to {0} confirmed.", message.TopicArn) });
                //var log = httpContext.Application["log"] as IList<string>;
                //if (log == null)
                //{
                //    log = new List<string>();
                //    httpContext.Application["log"] = log;
                //}

                //db.MyMessages.Add(new MyMessage() { Content = string.Format("{0}: Confirmed subscription to {1}", DateTime.Now, message.TopicArn) });
                //db.SaveChanges();
            }
            catch (Exception e)
            {
                //db.MyMessages.Add(new MyMessage() { Content = string.Format("Error confirming subscription to {0}: {1}", message.TopicArn, e.Message) });
                //db.SaveChanges();
            }
        }

    }
}
