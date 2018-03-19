using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.FuncionarioAcademico.Modelos
{
    [DataContract]
    public class TipoDeResponsavelModel
    {
        [DataMember]
        public String Tipo { get; set; }

        [DataMember]
        public String Identificador { get; set; }
      
    }
}
