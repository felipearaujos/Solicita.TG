using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Professor
{
    public class Professor: IProfessor
    {
         #region Atributos

        public Guid Id { get; private set; }

        public String Nome { get; private set; }

        public String Sobrenome { get; private set; }
        
        public List<String> Erros { get; private set; }

        public Boolean ÉCordenador { get; private set; }

        public TipoDeResponsável TipoDeResponsavel { get; private set; }

        #endregion Atributos

        #region Construtores

        public Professor()
        {
            Id = Guid.NewGuid();
            Erros = new List<string>();
        }

        public static Professor CriarProfessor(String Nome, String Sobrenome, Boolean ÉCordenador)
        {
            Professor professor = new Professor();
            professor.Nome = Nome;
            professor.Sobrenome = Sobrenome;
            professor.ÉCordenador = ÉCordenador;
            professor.TipoDeResponsavel = TipoDeResponsável.Professor;

            return professor;
        }

        public static Professor CriarProfessor(Guid Id, String Nome, String Sobrenome, Boolean ÉCordenador)
        {
            Professor professor = new Professor();
            professor.Id = Id;
            professor.Nome = Nome;
            professor.Sobrenome = Sobrenome;
            professor.ÉCordenador = ÉCordenador;

            return professor;
        }

        #endregion Construtores

        #region Métodos
        
        public void AlterarNome(String Nome)
        {
            if (Nome.Equals(String.Empty))
                Erros.Add("Não é possivel criar um Professor com Nome nulo.");
            else
                this.Nome = Nome;
        }

        public void AlterarSobrenome(String Sobrenome)
        {
            if (Sobrenome.Equals(String.Empty))
                Erros.Add("Não é possivel criar um Professor com Sobrenome nulo.");
            else
                this.Sobrenome = Sobrenome;
        }

        public void DefinirComoCordenador(Boolean éCordenador)
        {
            if (éCordenador)
                ÉCordenador = true;
            else
                ÉCordenador = false;
        }

        #endregion Métodos
    }
}
