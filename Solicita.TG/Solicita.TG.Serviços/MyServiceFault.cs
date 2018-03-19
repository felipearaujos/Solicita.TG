using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços
{
    [DataContract]
    public class MyServiceFault
    {
        public List<string> _message;

        public MyServiceFault()
        {
            _message = new List<string>();
        }
        
        [DataMember]
        public List<string> Message { get; set; }
    }
}
