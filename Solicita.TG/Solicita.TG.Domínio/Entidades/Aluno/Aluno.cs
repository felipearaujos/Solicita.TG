using Solicita.TG.Domínio.Entidades.Curso;
using Solicita.TG.Domínio.Entidades.Entidade;
using Solicita.TG.Domínio.Entidades.Matricula;
using Solicita.TG.Domínio.Entidades.Pessoa;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Aluno
{
    public class Aluno : IAluno
    {
        #region Atributos

        public Guid Id { get; private set; }

        public String Nome { get; private set; }

        public String Sobrenome { get; private set; }

        public IMatrícula Matrícula { get; private set; }

        public String RG { get; private set; }

        public List<String> Erros { get; private set; }

        public TipoDeResponsável TipoDeResponsavel { get; private set; }

        public String Email { get; private set; }

        #endregion Atributos

        #region Construtores

        private Aluno()
        {
            Erros = new List<string>();
            TipoDeResponsavel = TipoDeResponsável.Aluno;
        }

        public static Aluno CriarAluno(String nome, String sobrenome, String RA, String RG, Período Periodo, Turno Turno, Guid curso)
        {
            Aluno aluno = new Aluno();
            aluno.Id = Guid.NewGuid();
            aluno.RG = RG;
            aluno.DefinirMatrícula(RA, Periodo, Turno, curso);
            aluno.AlterarNome(nome);
            aluno.AlterarSobrenome(sobrenome);
            
            return aluno;
        }

        public static Aluno CriarAluno(Guid id, String nome, String sobrenome, String RA, String RG, Período Periodo, Turno Turno, Guid curso)
        {
            Aluno aluno = new Aluno();
            aluno.Id = id;
            aluno.RG = RG;  
            aluno.DefinirMatrícula(RA, Periodo, Turno,curso);
            aluno.AlterarNome(nome);
            aluno.AlterarSobrenome(sobrenome);

            return aluno;
        }

        #endregion Construtores
            
        #region Métodos
        
        public void AlterarNome(String Nome)
        {
            if (Nome.Equals(String.Empty))
                Erros.Add("Não é possivel criar um Aluno com Nome nulo.");
            else
                this.Nome = Nome;
        }

        public void AlterarSobrenome(String Sobrenome)
        {
            if (Sobrenome.Equals(String.Empty))
                Erros.Add("Não é possivel criar um Aluno com Sobrenome nulo.");
            else
                this.Sobrenome = Sobrenome;
        }

        public void AlterarPeríodo(Período Periodo)
        {
            if (!Periodo.Equals(this.Matrícula.Período))
            {
                this.DefinirMatrícula(this.Matrícula.RA, Periodo, this.Matrícula.Turno, this.Matrícula.Curso);
            }
        }

        public void AlterarTurno(Turno Turno)
        {
            if (!Turno.Equals(this.Matrícula.Turno))
            {
                this.DefinirMatrícula(this.Matrícula.RA, this.Matrícula.Período, Turno, this.Matrícula.Curso);
            }
        }

        public void DefinirMatrícula(String RA, Período Periodo, Turno Turno, Guid Curso)
        {
            IMatrícula umaMatrícula = new Matrícula(this, RA, Periodo, Turno, Curso);
            this.Matrícula = umaMatrícula; 
        }

        public void DefinirEmail(String email)
        {
            if (String.IsNullOrEmpty(email))
                Erros.Add("Não é possivel criar um Aluno com Nome nulo.");

            this.Email = email;
        }

        #endregion Métodos

    }
}
