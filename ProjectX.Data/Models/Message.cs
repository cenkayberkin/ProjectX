using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectX.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        [AllowHtml]
        public string Body { get; set; }

        public string Tags { get; set; }
    }
}
