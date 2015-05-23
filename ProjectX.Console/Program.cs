using ProjectX.Data;
using ProjectX.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SesService service = new SesService();
            
            for (int i = 0; i < 30; i++)
            {
                System.Console.WriteLine(service.SendEmail(new Data.Models.Email(), new Data.Models.Message()));    
            }
            
        }
    }
}
