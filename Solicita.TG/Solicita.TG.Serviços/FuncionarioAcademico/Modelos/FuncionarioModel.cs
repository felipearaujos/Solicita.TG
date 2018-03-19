using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.FuncionarioAcademico.Modelos
{
    [DataContract]
    public class FuncionarioModel : EntityModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Nome { get; set; }

        [DataMember]
        public String Sobrenome { get; set; }

        public FuncionarioModel()
        {
            Nome = String.Empty;
            Sobrenome = String.Empty;
            Id = Guid.Empty;
        }
    }
}
