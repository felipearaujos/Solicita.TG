using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Acesso.Modelos
{
    [DataContract]
    public class TiposModel
    {
        [DataMember]
        public Int32 Value { get; set; }

        [DataMember]
        public String Text { get; set; }
    }
}
