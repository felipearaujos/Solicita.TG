using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Requerimentos.Modelos
{
    [DataContract]
    [KnownType(typeof(RequerimentoModel))]
    public class SolicitacaoDeDocumentosModel: RequerimentoModel
    {
        [DataMember]
        public String TipoDeMotivo { get; set; }

        [DataMember]
        public String MotivoEspecificado { get; set; }

        [DataMember]
        public String InformarSemestreAtual { get; set; }

        [DataMember]
        public String InformarCargaHorária { get; set; }

        [DataMember]
        public String InformarAulaAosSabados { get; set; }

        [DataMember]
        public String InformarDisciplina { get; set; }

        [DataMember]
        public String InformarPeriodo { get; set; }

        [DataMember]
        public String InformarPrevisãoDeConclusão { get; set; }

        [DataMember]
        public String DisciplinaInformada { get; set; }
    }
}
