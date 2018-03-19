using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Requerimentos.Modelos
{
    [DataContract]
    public class RequerimentoSimplesModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Aluno { get; set; }

        [DataMember]
        public String DataTermino { get; set; }

        [DataMember]
        public String DataAbertura { get; set; }

        [DataMember]
        public String Tipo { get; set; }
        
        [DataMember]
        public String Status { get; set; }

        [DataMember]
        public String Responsavel { get; set; }

        [DataMember]
        public int NumeroPaginas { get; set; }

        [DataMember]
        public int PaginaAtual { get; set; }


        public RequerimentoSimplesModel()
        {
        }
    }
}
