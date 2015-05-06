﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Data.Models
{
    [JsonObject(IsReference = true)]
    public class Job
    {
        public int Id { get; set; }

        [ForeignKey("Delivery")]
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        public string EmailAddress { get; set; }
        public JobStatus Status { get; set; }
    }
}
