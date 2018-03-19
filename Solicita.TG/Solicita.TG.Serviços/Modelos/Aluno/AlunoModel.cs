using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Modelos.Aluno
{
    [DataContract]
    public class AlunoModel
    {
        [DataMember]
        public String Nome { get; set; }

        [DataMember]
        public String Sobrenome { get; set; }

        [DataMember]
        public String RA { get; set; }
    }
}
