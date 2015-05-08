using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Data.Models
{

    public class Email
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a valid email address")]
        [Index]
        [StringLength(200)]
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }

        public ICollection<EmailGroup> EmailGroups { get; set; }

    }
}
