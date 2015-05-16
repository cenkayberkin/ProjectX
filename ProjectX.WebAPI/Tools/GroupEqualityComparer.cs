using ProjectX.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectX.WebAPI
{
    public class GroupEqualityComparer: IEqualityComparer<EmailGroup>
    {
        public bool Equals(EmailGroup x, EmailGroup y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(EmailGroup obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}