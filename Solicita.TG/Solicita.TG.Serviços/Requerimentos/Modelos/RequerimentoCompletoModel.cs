using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Requerimentos.Modelos
{
    [DataContract]
    public class RequerimentoCompletoModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String RA { get; set; }

        [DataMember]
        public String RG { get; set; }

        [DataMember]
        public String Nome { get; set; }

        [DataMember]
        public String Sobrenome { get; set; }

        [DataMember]
        public String CursoSelecionado { get; set; }

        [DataMember]
        public String MateriaSelecionada { get; set; }

        [DataMember]
        public String Período { get; set; }

        [DataMember]
        public String Turno { get; set; }

        [DataMember]
        public String TipoDeRequerimento { get; set; }

        [DataMember]
        public String Observacoes { get; set; }

        [DataMember]
        public String DataTermino { get; set; }

        [DataMember]
        public String DataAbertura { get; set; }

        [DataMember]
        public List<Status> HistoricoDeStatus { get; set; }

        [DataMember]
        public Status StatusAtual { get; set; }


        public RequerimentoCompletoModel()
        {
            HistoricoDeStatus = new List<Status>();
            StatusAtual = new Status();
        }

        public class Status
        {
            [DataMember]
            public String StatusName { get; set; }

            [DataMember]
            public String Responsável { get; set; }

            [DataMember]
            public String DataDeEntradaNesseStatus { get; set; }

            [DataMember]
            public String DataDeSaidaNesseStatus { get; set; }

            [DataMember]
            public String Obs { get; set; }
        }
    }
}
