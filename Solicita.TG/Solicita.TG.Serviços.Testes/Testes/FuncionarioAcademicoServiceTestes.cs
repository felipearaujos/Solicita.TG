using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Serviços.FuncionarioAcademico;
using System.Threading;
using Solicita.TG.Persistência.Repositories.FuncionarioAcademicoRepository;
using Solicita.TG.Serviços.FuncionarioAcademico.Modelos;
using SharpTestsEx;
using System.Collections.Generic;
using System.Linq;

namespace Solicita.TG.Serviços.Testes.Testes
{
    [TestClass]
    public class FuncionarioAcademicoServiceTestes
    {
        private IFuncionarioAcademicoService service;
        private Guid idFuncionarioAcademico;

        [TestInitialize]
        public void Setup()
        {
            service = new FuncionarioAcademicoService();

            idFuncionarioAcademico = service.CriarFuncionario("Alexandre", "Costa",1);
            Thread.Sleep(50);
        }

        [TestCleanup]
        public void CleanUp()
        {
            FuncionarioAcademicoRepository funcionarioAcademicoRepository = new FuncionarioAcademicoRepository();
            funcionarioAcademicoRepository.LimparBancoDeDados();
        }

        [TestMethod]
        public void testeDeveCriarUmFuncionarioAcademico()
        {
            FuncionarioModel modelo = service.Get(idFuncionarioAcademico);

            modelo.Nome.Should().Be.EqualTo("Alexandre");
            modelo.Sobrenome.Should().Be.EqualTo("Costa");
        }

        [TestMethod]
        public void testeDeveListarTodosFuncionariosAcademicos()
        {
            Guid idOutroFuncionarioAcademico = service.CriarFuncionario("Carlos", "Oliveira",1);
            Thread.Sleep(50);

            List<FuncionarioModel> modelo = service.ListarTodosFuncionarios();

            modelo.Count.Should().Be.EqualTo(2);
            modelo.Any(x => x.Nome.Equals("Alexandre")).Should().Be.True();
            modelo.Any(x => x.Sobrenome.Equals("Costa")).Should().Be.True();

            modelo.Any(x => x.Nome.Equals("Carlos")).Should().Be.True();
            modelo.Any(x => x.Sobrenome.Equals("Oliveira")).Should().Be.True();
        }
    }
}
