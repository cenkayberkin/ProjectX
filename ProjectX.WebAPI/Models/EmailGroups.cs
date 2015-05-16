using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Data.Models
{
   
    public class EmailGroup
    {
        public EmailGroup()
        {
            Emails = new List<Email>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Email>   Emails { get; set; }
    }
}
