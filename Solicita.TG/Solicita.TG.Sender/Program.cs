using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solicita.TG.Sender
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunnerTask runner = new RunnerTask();

            while (true)
            {
                Console.WriteLine("---- Procurando Emails...");
                runner.SendMail();
                System.Threading.Thread.Sleep(1000 * 20 * 1); // Sleep for 5 minutes
            }
        }
    }
}
