using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Dashboard.Modelos
{
    [DataContract]
    public class ResumoAtual
    {
        
        [DataMember]
        public Int32 QuantidadeEmAndamento { get; set; }

        [DataMember]
        public Double MediaConclusao { get; set; }

        public ResumoAtual()
        {
            QuantidadeEmAndamento = 0;
            MediaConclusao = 0;
        }

    }



}
