using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Data.Models
{
    [JsonObject(IsReference = true)]
    public class Email
    {
        public int Id { get; set; }

        public string Address { get; set; }

        [ForeignKey("EmailGroup")]
        public int EmailGroupId { get; set; }

        public EmailGroup EmailGroup { get; set; }

    }
}
