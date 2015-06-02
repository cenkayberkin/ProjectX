using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProjectX.Data.Models
{
    public class EmailMessage
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        [Required]
        [AllowHtml]
        public string Body { get; set; }

        public string Tags { get; set; }
    }
}
