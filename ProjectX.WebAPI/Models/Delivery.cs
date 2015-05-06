using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Data.Models
{
    
    public class Delivery
    {
        public int Id { get; set; }

        [ForeignKey("Message")]
        public int MessageId { get; set; }
        
        public Message Message { get; set; }

        public DateTime Time { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }

        public EmailGroup Group { get; set; }

        public DeliveryStatus Status { get; set; }
    }
}
