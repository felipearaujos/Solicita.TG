using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Domínio.Entidades.FuncionarioAcademico;
using System.Collections.Generic;
using Solicita.TG.Persistência.Repositories.FuncionarioAcademicoRepository;
using System.Linq;
using SharpTestsEx;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;

namespace Solicita.TG.Persistência.Testes
{
    [TestClass]
    public class FuncionarioAcademicoRepositoryTestes
    {
        private FuncionarioAcademicoRepository objetoDeTeste;
        private IFuncionarioAcademico umFuncionario;
        private IFuncionarioAcademico OutroFuncionario;
        private List<IFuncionarioAcademico> ListaDeFuncionarios;

        [TestInitialize]
        public void SetUp()
        {
            objetoDeTeste = new FuncionarioAcademicoRepository();
            objetoDeTeste.LimparBancoDeDados();
        }

        [TestCleanup]
        public void LimparBancoDeDados()
        {
            objetoDeTeste.LimparBancoDeDados();
        }

        [TestMethod]
        public void testeDeveCadastrarUmFuncionarioAcademicoEConsultarPeloSeuId()
        {

            umFuncionario = FuncionarioAcademico.CriarFuncionarioAcademico("Alexandre", "Costa",TipoDeResponsável.FuncionárioAcadêmico);

            objetoDeTeste.Salvar(umFuncionario);

            IFuncionarioAcademico entidadeDoDB = objetoDeTeste.GetByID(umFuncionario.Id);
            entidadeDoDB.Nome.Should().Be.EqualTo("Alexandre");
            entidadeDoDB.Sobrenome.Should().Be.EqualTo("Costa");
        }

        [TestMethod]
        public void testeDeveLimparATabelaFuncionarioAcademicoDoBancoDeDados()
        {
            umFuncionario = FuncionarioAcademico.CriarFuncionarioAcademico("Alexandre", "Costa",TipoDeResponsável.FuncionárioAcadêmico);
            OutroFuncionario = FuncionarioAcademico.CriarFuncionarioAcademico("Carlos", "Oliveira", TipoDeResponsável.FuncionárioAcadêmico);

            objetoDeTeste.Salvar(umFuncionario);
            objetoDeTeste.Salvar(OutroFuncionario);

            List<IFuncionarioAcademico> funcionarios = objetoDeTeste.ListAll();
            funcionarios.Count.Should().Be.EqualTo(2);

            funcionarios.Any(x => x.Nome.Equals("Alexandre")).Should().Be.True();
            funcionarios.Any(x => x.Sobrenome.Equals("Costa")).Should().Be.True();

            funcionarios.Any(x => x.Nome.Equals("Carlos")).Should().Be.True();
            funcionarios.Any(x => x.Sobrenome.Equals("Oliveira")).Should().Be.True();

            objetoDeTeste.LimparBancoDeDados();

            funcionarios = objetoDeTeste.ListAll();
            funcionarios.Count.Should().Be.EqualTo(0);
        }

        [TestMethod]
        public void testeDeveAtualizarONomeDeUmFuncionarioAcademico()
        {
            umFuncionario = FuncionarioAcademico.CriarFuncionarioAcademico("Alexandre", "Costa", TipoDeResponsável.FuncionárioAcadêmico);

            objetoDeTeste.Salvar(umFuncionario);

            umFuncionario.AlterarNome("Roberto");
            umFuncionario.AlterarSobrenome("Costa");

            objetoDeTeste.Salvar(umFuncionario);

            umFuncionario = objetoDeTeste.GetByID(umFuncionario.Id);

            umFuncionario.Nome.Should().Be.EqualTo("Roberto");
            umFuncionario.Sobrenome.Should().Be.EqualTo("Costa");

        }

        [TestMethod]
        public void testeDeveExcluirUmFuncionarioAcademico()
        {
            umFuncionario = FuncionarioAcademico.CriarFuncionarioAcademico("Alexandre", "Costa", TipoDeResponsável.FuncionárioAcadêmico);

            objetoDeTeste.Salvar(umFuncionario);

            objetoDeTeste.Excluir(umFuncionario);

            umFuncionario = objetoDeTeste.GetByID(umFuncionario.Id);
            umFuncionario.Should().Be.Null();
        }

        [TestMethod]
        public void testeDeveListarTodosOsFuncionariosAcademicosCadastrados()
        {
            umFuncionario = FuncionarioAcademico.CriarFuncionarioAcademico("Alexandre", "Costa", TipoDeResponsável.FuncionárioAcadêmico);
            OutroFuncionario = FuncionarioAcademico.CriarFuncionarioAcademico("Carlos", "Oliveira", TipoDeResponsável.FuncionárioAcadêmico);

            objetoDeTeste.Salvar(umFuncionario);
            objetoDeTeste.Salvar(OutroFuncionario);

            List<IFuncionarioAcademico> funcionarios = objetoDeTeste.ListAll();
            funcionarios.Count.Should().Be.EqualTo(2);

            funcionarios.Any(x => x.Nome.Equals("Alexandre")).Should().Be.True();
            funcionarios.Any(x => x.Sobrenome.Equals("Costa")).Should().Be.True();

            funcionarios.Any(x => x.Nome.Equals("Carlos")).Should().Be.True();
            funcionarios.Any(x => x.Sobrenome.Equals("Oliveira")).Should().Be.True();
        }
    }
}
