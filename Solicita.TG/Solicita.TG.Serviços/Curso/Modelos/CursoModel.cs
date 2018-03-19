using Solicita.TG.Serviços.DisciplinaService.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.cursoService.Modelos
{
    [DataContract]
    public class CursoModel : EntityModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Nome { get; set; }

        [DataMember]
        public List<DisciplinaModel> GradeCurricular { get; set; }

        [DataMember]
        public Int32 CargaHoraria { get; set; }

        public CursoModel()
        {
            this.Id = Guid.Empty;
            this.Nome = String.Empty;
            this.GradeCurricular = new List<DisciplinaModel>();
            this.CargaHoraria = 0;
        }

    }
}
