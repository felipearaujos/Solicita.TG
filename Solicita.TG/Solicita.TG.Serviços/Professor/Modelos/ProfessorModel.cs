using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Professor.Modelos
{
    [DataContract]
    public class ProfessorModel : EntityModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Nome { get; set; }

        [DataMember]
        public String Sobrenome { get; set; }

        [DataMember]
        public String Cordenador { get; set; }

        public ProfessorModel()
        {
            Nome = String.Empty;
            Sobrenome = String.Empty;
            Id = Guid.Empty;
            Cordenador = String.Empty;
        }
    }
}
