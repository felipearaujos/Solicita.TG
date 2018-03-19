using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Requerimentos.Modelos
{
    [DataContract]
    public class StatusModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String DataEntrada { get; set; }

        [DataMember]
        public String DataSaida { get; set; }

        [DataMember]
        public String StatusNome { get; set; }

        [DataMember]
        public String Responsável { get; set; }

        [DataMember]
        public String Observação { get; set; }
    }
}
