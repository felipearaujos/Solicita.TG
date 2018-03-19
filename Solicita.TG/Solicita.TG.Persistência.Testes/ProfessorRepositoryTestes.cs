using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Domínio.Entidades.Professor;
using Solicita.TG.Persistência.Repositories.ProfessorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpTestsEx;

namespace Solicita.TG.Persistência.Testes
{
    [TestClass]
    public class ProfessorRepositoryTestes
    {
        private ProfessorRepository objetoDeTeste;
        private IProfessor UmProfessor;
        private IProfessor OutroProfessor;
        private List<IProfessor> ListaDeProfessores;

        [TestInitialize]
        public void SetUp()
        {
            objetoDeTeste = new ProfessorRepository();
            objetoDeTeste.LimparBancoDeDados();
        }

        [TestCleanup]
        public void LimparBancoDeDados()
        {
            objetoDeTeste.LimparBancoDeDados();
        }
        
        [TestMethod]
        public void testeDeveCadastrarUmProfessorEConsultarPeloSeuId()
        {

            UmProfessor = Professor.CriarProfessor("José Carlos", "Bortot", true);

            objetoDeTeste.Salvar(UmProfessor);

            IProfessor entidadeDoDB = objetoDeTeste.GetByID(UmProfessor.Id);
            entidadeDoDB.Nome.Should().Be.EqualTo("José Carlos");
            entidadeDoDB.Sobrenome.Should().Be.EqualTo("Bortot");
            entidadeDoDB.ÉCordenador.Should().Be.True();

        }

        [TestMethod]
        public void testeDeveLimparATabelaProfessoresDoBancoDeDados()
        {
            UmProfessor = Professor.CriarProfessor("José Carlos", "Bortot", true);
            OutroProfessor = Professor.CriarProfessor("Bruno", "Marques Panccioni", false);

            objetoDeTeste.Salvar(UmProfessor);
            objetoDeTeste.Salvar(OutroProfessor);

            List<IProfessor> Professores = objetoDeTeste.ListAll();
            Professores.Count.Should().Be.EqualTo(2);

            Professores.Any(x => x.Nome.Equals("José Carlos")).Should().Be.True();
            Professores.Any(x => x.Sobrenome.Equals("Marques Panccioni")).Should().Be.True();

            Professores.Any(x => x.Nome.Equals("José Carlos")).Should().Be.True();
            Professores.Any(x => x.Sobrenome.Equals("Bortot")).Should().Be.True();

            objetoDeTeste.LimparBancoDeDados();

            Professores = objetoDeTeste.ListAll();
            Professores.Count.Should().Be.EqualTo(0);
        }

        [TestMethod]
        public void testeDeveAtualizarONomeDeUmProfessor()
        {
            UmProfessor = Professor.CriarProfessor("José Carlos", "Bortot", true);

            objetoDeTeste.Salvar(UmProfessor);

            UmProfessor.AlterarNome("Bruno");
            UmProfessor.AlterarSobrenome("Marques Panccioni");
            UmProfessor.DefinirComoCordenador(false);

            objetoDeTeste.Salvar(UmProfessor);

            UmProfessor = objetoDeTeste.GetByID(UmProfessor.Id);

            UmProfessor.Nome.Should().Be.EqualTo("Bruno");
            UmProfessor.Sobrenome.Should().Be.EqualTo("Marques Panccioni");
            UmProfessor.ÉCordenador.Should().Be.False();

        }

        [TestMethod]
        public void testeDeveExcluirUmProfessor()
        {
            UmProfessor = Professor.CriarProfessor("José Carlos", "Bortot", true);

            objetoDeTeste.Salvar(UmProfessor);

            objetoDeTeste.Excluir(UmProfessor);

            UmProfessor = objetoDeTeste.GetByID(UmProfessor.Id);
            UmProfessor.Should().Be.Null();
        }

        [TestMethod]
        public void testeDeveListarTodosOsProfessoresCadastrados()
        {
            UmProfessor = Professor.CriarProfessor("José Carlos", "Bortot", true);
            OutroProfessor = Professor.CriarProfessor("Bruno", "Marques Panccioni", false);

            objetoDeTeste.Salvar(UmProfessor);
            objetoDeTeste.Salvar(OutroProfessor);

            List<IProfessor> Professores = objetoDeTeste.ListAll();
            Professores.Count.Should().Be.EqualTo(2);

            Professores.Any(x => x.Nome.Equals("José Carlos")).Should().Be.True();
            Professores.Any(x => x.Sobrenome.Equals("Marques Panccioni")).Should().Be.True();

            Professores.Any(x => x.Nome.Equals("José Carlos")).Should().Be.True();
            Professores.Any(x => x.Sobrenome.Equals("Bortot")).Should().Be.True();
        }

    }
}
