using ProjectX.Data;
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

            System.Console.WriteLine("Welcome to SMC");

            PXContext db = new PXContext();
            var msg = db.Messages.First();

            System.Console.WriteLine(msg.Body);

            System.Console.ReadLine();

        }
    }
}
