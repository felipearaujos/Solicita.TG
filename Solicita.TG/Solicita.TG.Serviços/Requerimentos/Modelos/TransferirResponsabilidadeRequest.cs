using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Requerimentos.Modelos
{
    [DataContract]
    public class TransferirResponsabilidadeRequest
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Int32 Responsavel { get; set; }
    }
}
