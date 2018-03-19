using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Solicita.TG.Serviços.Acesso.Modelos;

namespace Solicita.TG.Serviços.Acesso.Modelos
{
    [DataContract]
    public class InfoDeLogon
    {
        [DataMember]
        public Boolean AcessoLiberado { get; set; }

        [DataMember]
        public AcessoModel.TipoDeUsuarioModel TipoDeUsuario { get; set; }

        [DataMember]
        public String NomeDoUsuario { get; set; }

        [DataMember]
        public String Login { get; set; }

        [DataMember]
        public String Response { get; set; }

        public InfoDeLogon()
        {
            TipoDeUsuario = new AcessoModel.TipoDeUsuarioModel();
        }
    }
}
