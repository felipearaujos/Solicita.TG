using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Acesso.Modelos
{
    [DataContract]
    public class AcessoModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Usuario { get; set; }

        [DataMember]
        public String Senha { get; set; }

        [DataMember]
        public TipoDeUsuarioModel TipoDeUsuario { get; set; }

        public AcessoModel()
        {
            TipoDeUsuario = new TipoDeUsuarioModel();
        }


        [DataContract]
        public class TipoDeUsuarioModel
        {
            [DataMember]
            public Int32 Tipo { get; set; }

            [DataMember]
            public String NomeDoTipo { get; set; }
        }
    }
}
