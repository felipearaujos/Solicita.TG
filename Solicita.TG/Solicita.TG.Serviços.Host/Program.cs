using Solicita.TG.Serviços.Serviços.AlunoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost sh = new ServiceHost(typeof(AlunoService));
            var se = sh.AddServiceEndpoint(typeof(IAlunoService), new WebHttpBinding(), "http://localhost:8082/Servicos/AlunoService/");
            se.Behaviors.Add(new WebHttpBehavior());
            se.EndpointBehaviors.Add(new EnableCrossOriginResourceSharingBehavior());
            EnableCrossOriginResourceSharingBehavior enableCross = new EnableCrossOriginResourceSharingBehavior();


            while (true)
            {
                if (!sh.State.Equals(CommunicationState.Opened))
                {
                    sh.Open();
                    Console.WriteLine(se.Address);
                }
                Console.ReadLine();
            }
        }
    }
}
