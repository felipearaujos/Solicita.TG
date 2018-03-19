using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.DisciplinaService.Modelos
{
    [DataContract]
    public class DisciplinaModel : EntityModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Nome { get; set; }

        [DataMember]
        public Int32 CargaHoraria { get; set; }

        public DisciplinaModel()
        {
            this.Id = Guid.Empty;
            this.Nome = String.Empty;
            this.CargaHoraria = 0;
        }
    }
}
