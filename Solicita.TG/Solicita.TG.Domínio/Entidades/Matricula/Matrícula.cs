using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Curso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Matricula
{
    public class Matrícula : IMatrícula
    {

        #region Construtores

        public Matrícula(IAluno aluno, String ra, Período período, Turno turno, Guid curso)
        {
            if (String.IsNullOrEmpty(ra) || String.IsNullOrWhiteSpace(ra))
                aluno.Erros.Add("Não é possível criar um aluno com o RA nulo.");
            else
                RA = ra;

            if (período.Equals(Período.Indefinido))
                aluno.Erros.Add("Não é possível criar um aluno com o Período indefinido.");
            else
                Período = período;

            this.Curso = curso;

            if (turno.Equals(Turno.Indefinido))
                aluno.Erros.Add("Não é possível criar um aluno com o Turno indefinido.");
            else
                Turno = turno;
        }

        #endregion Construtores

        #region Atributos

        public String RA { get; private set; }
        public Período Período { get; private set; }
        public Turno Turno { get; private set; }
        public Guid Curso { get; private set; }

        #endregion Atributos

        #region Métodos

        #endregion Métodos
    }
}
