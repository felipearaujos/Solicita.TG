using Solicita.TG.Serviços.Aluno.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Requerimentos.Modelos
{
    [DataContract]
    public class RequerimentoModel
    {
        [DataMember]
        public Guid Id { get; set; }
        
        [DataMember]
        public AlunoModel Aluno { get; set; }

        [DataMember]
        public String DataAbertura { get; set; }

        [DataMember]
        public List<StatusModel> HistoricoDeStatus { get; set; }

        [DataMember]
        public StatusModel StatusAtual { get; set; }

        [DataMember]
        public String DataPrevistaTermino { get; set; }

        public RequerimentoModel()
        {
            Aluno = new AlunoModel();
            HistoricoDeStatus = new List<StatusModel>();
            StatusAtual = new StatusModel();
        }
    }
}
