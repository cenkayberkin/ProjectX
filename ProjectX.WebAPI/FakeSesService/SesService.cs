using ProjectX.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ProjectX.WebAPI
{
    public class SesService
    {
        Random random;
        public SesService()
        {
            random = new Random();
        }

        public bool SendEmail(string email, EmailMessage message)
        {
            Thread.Sleep(1000);

            int randomNumber = random.Next(0, 100);
            bool result = false;
        
            if (randomNumber < 80)
            {
                result = true;
            }
            return result;
        }
    }
}