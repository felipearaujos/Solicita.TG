using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Persistência.Repositories.ProfessorRepository;
using Solicita.TG.Serviços.Professor;
using Solicita.TG.Serviços.Professor.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpTestsEx;

namespace Solicita.TG.Serviços.Testes.Testes
{
    [TestClass]
    public class ProfessorServiceTestes
    {
        private IProfessorService service;
        private Guid idProfessor;

        [TestInitialize]
        public void Setup()
        {
            service = new ProfessorService();

            idProfessor = service.Criar("José Carlos", "Bortot", true);
            Thread.Sleep(50);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ProfessorRepository professorRepository = new ProfessorRepository();
            professorRepository.LimparBancoDeDados();
        }

        [TestMethod]
        public void testeDeveCriarUmProfessor()
        {
            ProfessorModel modelo = service.Get(idProfessor);

            modelo.Nome.Should().Be.EqualTo("José Carlos");
            modelo.Sobrenome.Should().Be.EqualTo("Bortot");
            modelo.Cordenador.Should().Be.EqualTo("Sim");
        }

        [TestMethod]
        public void testeDeveListarTodosProfessores()
        {
            Guid idOutroProfessor = service.Criar("Bruno", "Marques Panccioni", true);
            Thread.Sleep(50);

            List<ProfessorModel> modelo = service.ListarTodosProfessores();

            modelo.Count.Should().Be.EqualTo(2);
            modelo.Any(x => x.Nome.Equals("José Carlos")).Should().Be.True();
            modelo.Any(x => x.Sobrenome.Equals("Bortot")).Should().Be.True();
            modelo.Any(x => x.Cordenador.Equals("Sim")).Should().Be.True();

            modelo.Any(x => x.Nome.Equals("Bruno")).Should().Be.True();
            modelo.Any(x => x.Sobrenome.Equals("Marques Panccioni")).Should().Be.True();
            modelo.Any(x => x.Cordenador.Equals("Sim")).Should().Be.True();
        }
    }
}
