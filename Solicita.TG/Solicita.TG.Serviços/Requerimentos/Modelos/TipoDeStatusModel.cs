using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Requerimentos.Modelos
{
    [DataContract]
    public class TipoDeStatusModel
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String Value { get; set; }


    }
}
