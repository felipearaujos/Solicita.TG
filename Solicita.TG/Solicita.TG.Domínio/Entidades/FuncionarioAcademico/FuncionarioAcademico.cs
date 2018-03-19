using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.FuncionarioAcademico
{
    public class FuncionarioAcademico: IFuncionarioAcademico
    {
        #region Atributos

        public Guid Id { get; private set; }

        public String Nome { get; private set; }

        public String Sobrenome { get; private set; }

        public List<String> Erros { get; private set; }

        public TipoDeResponsável TipoDeResponsavel { get; private set; }

        #endregion Atributos

        #region Construtores

        public FuncionarioAcademico()
        {
            Id = Guid.NewGuid();
            Erros = new List<string>();
        }

        public static FuncionarioAcademico CriarFuncionarioAcademico(String Nome, String Sobrenome, TipoDeResponsável TipoDeResponsável)
        {
            FuncionarioAcademico funcionarioAcademico = new FuncionarioAcademico();
            funcionarioAcademico.Nome = Nome;
            funcionarioAcademico.Sobrenome = Sobrenome;
            funcionarioAcademico.TipoDeResponsavel = TipoDeResponsável;

            return funcionarioAcademico;
        }


        public static FuncionarioAcademico CriarFuncionarioAcademico(TipoDeResponsável TipoDeResponsável)
        {
            FuncionarioAcademico funcionarioAcademico = new FuncionarioAcademico();
            funcionarioAcademico.Id = Guid.Empty;
            funcionarioAcademico.TipoDeResponsavel = TipoDeResponsável;

            return funcionarioAcademico;
        }

        public static FuncionarioAcademico CriarFuncionarioAcademico(Guid Id, String Nome, String Sobrenome, TipoDeResponsável tipoDeResponsavel)
        {
            FuncionarioAcademico funcionarioAcademico = new FuncionarioAcademico();
            funcionarioAcademico.Id = Id;
            funcionarioAcademico.Nome = Nome;
            funcionarioAcademico.Sobrenome = Sobrenome;
            funcionarioAcademico.TipoDeResponsavel = tipoDeResponsavel;

            return funcionarioAcademico;
        }

        #endregion Construtores

        #region Métodos

        public void AlterarNome(String Nome)
        {
            if (Nome.Equals(String.Empty))
                Erros.Add("Não é possivel criar um Funcionário Acadêmico com Nome nulo.");
            else
                this.Nome = Nome;
        }

        public void AlterarSobrenome(String Sobrenome)
        {
            if (Sobrenome.Equals(String.Empty))
                Erros.Add("Não é possivel criar um Funcionário Acadêmico com Sobrenome nulo.");
            else
                this.Sobrenome = Sobrenome;
        }

        #endregion Métodos
    }
}
