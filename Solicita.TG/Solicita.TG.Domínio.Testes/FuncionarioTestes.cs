using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Domínio.Entidades.FuncionarioAcademico;
using SharpTestsEx;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;

namespace Solicita.TG.Domínio.Testes
{
    [TestClass]
    public class FuncionarioTestes
    {

        private IFuncionarioAcademico objetoDeTeste;


        [TestInitialize]
        public void Setup()
        {
            objetoDeTeste = FuncionarioAcademico.CriarFuncionarioAcademico("Alexandre", "Costa", TipoDeResponsável.FuncionárioAcadêmico);
        }

        [TestMethod]
        public void TesteDeveCriarFuncionarioAcademicoComNomeSobrenome()
        {
            objetoDeTeste.Id.Should().Not.Be.Null();
            objetoDeTeste.Nome.Should().Be.EqualTo("Alexandre");
            objetoDeTeste.Sobrenome.Should().Be.EqualTo("Costa");
        }

        [TestMethod]
        public void testeDevePermitirCriarFuncionarioAcademicoAPartirDoTipoDeResponsavel()
        {
            objetoDeTeste = FuncionarioAcademico.CriarFuncionarioAcademico("Alexandre", "Costa", TipoDeResponsável.Diretor);

            objetoDeTeste.TipoDeResponsavel.Should().Be.EqualTo(TipoDeResponsável.Diretor);
        }

        [TestMethod]
        public void testeNãoDevePermitirCriarUmFuncionarioAcademicoComNomeVazioOuNulo()
        {
            objetoDeTeste.AlterarNome(String.Empty);

            objetoDeTeste.Erros.Contains("Não é possivel criar um Funcionário Acadêmico com Nome nulo.");
        }

        [TestMethod]
        public void testeNãoDevePermitirCriarUmFuncionarioAcademicoComSobrenomeVazioOuNulo()
        {
            objetoDeTeste.AlterarSobrenome(String.Empty);

            objetoDeTeste.Erros.Contains("Não é possivel criar um Funcionário Acadêmico com Sobrenome nulo.");
        }

        [TestMethod]
        public void testeDevePermitirAlterarONomeDoFuncionarioAcademico()
        {
            objetoDeTeste.Nome.Should().Be.EqualTo("Alexandre");

            objetoDeTeste.AlterarNome("Marcos");

            objetoDeTeste.Nome.Should().Be.EqualTo("Marcos");
        }

        [TestMethod]
        public void testeDevePermitirAlterarOSobrenomeDoProfessor()
        {
            objetoDeTeste.Sobrenome.Should().Be.EqualTo("Costa");

            objetoDeTeste.AlterarSobrenome("Oliveira");

            objetoDeTeste.Sobrenome.Should().Be.EqualTo("Oliveira");
        }
    }
}
