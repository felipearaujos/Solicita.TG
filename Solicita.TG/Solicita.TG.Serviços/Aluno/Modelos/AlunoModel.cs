using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Aluno.Modelos
{
    [DataContract]
    public class AlunoModel : EntityModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Nome { get; set; }

        [DataMember]
        public String Sobrenome { get; set; }

        [DataMember]
        public String RG { get; set; }

        [DataMember]
        public MatriculaModel Matricula { get; set; }

        [DataMember]
        public Boolean Editavel { get; set; }

        [DataMember]
        public String Email { get; set; }
        
        public AlunoModel()
        {
            this.Id = Guid.Empty;
            this.Nome = String.Empty;
            this.Sobrenome = String.Empty;
            this.RG = String.Empty;
            this.Matricula = new MatriculaModel();
            this.Editavel = false;
            this.Email = String.Empty;
        }

        [DataContract]
        public class MatriculaModel
        {
            [DataMember]
            public String RA { get; set; }

            [DataMember]
            public String Periodo { get; set; }

            [DataMember]
            public String Turno { get; set; }

            [DataMember]
            public String AnoIngresso { get; set; }

            [DataMember]
            public String Curso { get; set; }

            [DataMember]
            public String CargaHorária { get; set; }

            public MatriculaModel()
            {
                this.Turno = this.Periodo = this.RA = String.Empty;
            }
        }
    }
}
