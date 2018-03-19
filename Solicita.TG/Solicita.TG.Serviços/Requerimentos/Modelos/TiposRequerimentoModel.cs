using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Solicita.TG.Serviços.Requerimentos.Modelos
{
    [DataContract]
    public class TiposRequerimentoModel
    {
        [DataMember]
        public String Tipo { get; set; }

        [DataMember]
        public String Identificador { get; set; }

    }
}
