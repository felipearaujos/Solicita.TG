using Solicita.TG.Domínio.Entidades.Disciplina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Curso
{
    public class Curso : ICurso
    {
        #region Atributos

        public Guid Id { get; private set; }

        public String Nome { get; private set; }

        public List<Disciplina.IDisciplina> GradeCurricular { get; private set; }

        public Int32 CargaHorária { get { return GradeCurricular.Sum(x => x.CargaHorária); } }

        public List<String> Erros { get; private set; }

        #endregion

        #region Construtores

        private Curso()
        {
            Erros = new List<string>();
            GradeCurricular = new List<IDisciplina>();
            Id = Guid.Empty;
            Nome = String.Empty;
        }

        public static Curso CriarCurso(String nome)
        {
            Curso curso = new Curso();
            curso.Id = Guid.NewGuid();
            curso.DefinirNome(nome);

            return curso;
        }

        public static Curso CriarCurso(Guid Id, String nome)
        {
            Curso curso = new Curso();
            curso.Id = Id;
            curso.DefinirNome(nome);

            return curso;
        }

        public static Curso CriarCurso(Guid Id, String nome, List<IDisciplina> gradeCurricular)
        {
            Curso curso = new Curso();
            curso.Id = Id;
            curso.DefinirNome(nome);
            curso.GradeCurricular = gradeCurricular;


            return curso;
        }

        #endregion

        #region Métodos

        public void AdicionarDisciplina(IDisciplina disciplina)
        {
            if (GradeCurricular.Any(x => x.Nome.Equals(disciplina.Nome) && x.CargaHorária.Equals(disciplina.CargaHorária)))
                throw new InvalidOperationException("Disciplina já Cadastrada");

            GradeCurricular.Add(disciplina);
        }

        public void RemoverDisciplina(IDisciplina disciplina)
        {
            GradeCurricular.Remove(disciplina);
        }

        public void DefinirNome(string nome)
        {
            if (nome.Equals(String.Empty))
                throw new ArgumentException("O nome do curso não pode ser vazio!");

            this.Nome = nome;
        }

        public void AdicionarGradeCurricularCompleta(List<IDisciplina> gradeCurricular)
        { 
            //ALERT: Não apague isso ... é feio mais resolve
            this.GradeCurricular = gradeCurricular;

        }

        #endregion

    }
}
