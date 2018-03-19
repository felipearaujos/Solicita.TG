using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Reset
{
    class Program
    {
        static void Main(string[] args)
        {
            Reset Load = new Reset();

            Console.WriteLine("Bem vindo ao Populate de testes");
            Load.Load();
        }
    }
}
