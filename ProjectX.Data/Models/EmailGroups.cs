using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Data.Models
{
    [JsonObject(IsReference = true)]
    public class EmailGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Email> Emails { get; set; }
    }
}
