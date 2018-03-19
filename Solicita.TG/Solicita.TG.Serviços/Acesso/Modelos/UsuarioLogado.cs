using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Acesso.Modelos
{
    [DataContract]
    public class UsuarioLogado
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Usuario { get; set; }

        [DataMember]
        public String Tipo { get; set; }

        [DataMember]
        public Int32 TipoValue { get; set; }
    }
}
