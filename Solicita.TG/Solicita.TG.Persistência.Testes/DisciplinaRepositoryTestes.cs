using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Persistência.Repositories.DisciplinaRepository;
using Solicita.TG.Domínio.Entidades.Disciplina;
using SharpTestsEx;

namespace Solicita.TG.Persistência.Testes
{
    [TestClass]
    public class DisciplinaRepositoryTestes
    {
        private DisciplinaRepository ObjetoDeTeste;
        private IDisciplina UmaDisciplina;
        private IDisciplina OutraDisciplina;


        [TestInitialize]
        public void SetUp()
        {
            ObjetoDeTeste = new DisciplinaRepository();
            ObjetoDeTeste.LimparBancoDeDados();
        }

        [TestCleanup]
        public void LimparBancoDeDados()
        {
            ObjetoDeTeste.LimparBancoDeDados();
        }

        [TestMethod]
        public void testeDeveCadastrarUmaDisicplinaEBuscarPorSeuID()
        {
            UmaDisciplina = Disciplina.CriarDisciplina("Administração", 80);

            ObjetoDeTeste.Salvar(UmaDisciplina);

            OutraDisciplina = ObjetoDeTeste.GetByID(UmaDisciplina.Id);

            OutraDisciplina.Id.Should().Be.EqualTo(UmaDisciplina.Id);
            OutraDisciplina.Nome.Should().Be.EqualTo(UmaDisciplina.Nome);
            OutraDisciplina.CargaHorária.Should().Be.EqualTo(UmaDisciplina.CargaHorária);

        }

        [TestMethod]
        public void testeDeveListarTodasAsDisciplinasCadastradas()
        {
            IDisciplina UmaDisciplina = Disciplina.CriarDisciplina("Administração", 80);
            IDisciplina SegundaDisciplina = Disciplina.CriarDisciplina("Administração II", 80);
            IDisciplina TerceiraDisciplina = Disciplina.CriarDisciplina("Administração III", 80);

            ObjetoDeTeste.Salvar(UmaDisciplina);
            ObjetoDeTeste.Salvar(SegundaDisciplina);
            ObjetoDeTeste.Salvar(TerceiraDisciplina);

            List<IDisciplina> disciplinas = ObjetoDeTeste.ListAll();

            disciplinas.Count.Should().Be.EqualTo(3);
            disciplinas.Any(x => x.Equals(UmaDisciplina.Nome) && x.Id.Equals(UmaDisciplina.Id) && x.CargaHorária.Equals(UmaDisciplina.CargaHorária));
            disciplinas.Any(x => x.Equals(SegundaDisciplina.Nome) && x.Id.Equals(SegundaDisciplina.Id) && x.CargaHorária.Equals(SegundaDisciplina.CargaHorária));
            disciplinas.Any(x => x.Equals(TerceiraDisciplina.Nome) && x.Id.Equals(TerceiraDisciplina.Id) && x.CargaHorária.Equals(TerceiraDisciplina.CargaHorária));

        }

        [TestMethod]
        public void testeDeveAtualizarUmaDisicplinaEBuscarPorSeuID()
        {
            UmaDisciplina = Disciplina.CriarDisciplina("Administração", 80);

            ObjetoDeTeste.Salvar(UmaDisciplina);

            OutraDisciplina = ObjetoDeTeste.GetByID(UmaDisciplina.Id);

            OutraDisciplina.Id.Should().Be.EqualTo(UmaDisciplina.Id);
            OutraDisciplina.Nome.Should().Be.EqualTo("Administração");
            OutraDisciplina.CargaHorária.Should().Be.EqualTo(80);


            UmaDisciplina.DefinirNome("Gestão de Equipes");
            UmaDisciplina.DefinirCargaHorária(40);

            ObjetoDeTeste.Salvar(UmaDisciplina);


            OutraDisciplina = ObjetoDeTeste.GetByID(UmaDisciplina.Id);
            OutraDisciplina.Id.Should().Be.EqualTo(UmaDisciplina.Id);
            OutraDisciplina.Nome.Should().Be.EqualTo("Gestão de Equipes");
            OutraDisciplina.CargaHorária.Should().Be.EqualTo(40);

        }

        [TestMethod]
        public void testeDeveExluirUmaDisciplina()
        {
            IDisciplina UmaDisciplina = Disciplina.CriarDisciplina("Administração", 80);
          
            ObjetoDeTeste.Salvar(UmaDisciplina);
          
            List<IDisciplina> disciplinas = ObjetoDeTeste.ListAll();

            disciplinas.Count.Should().Be.EqualTo(1);

            ObjetoDeTeste.Excluir(UmaDisciplina);

            disciplinas = ObjetoDeTeste.ListAll();

            disciplinas.Count.Should().Be.EqualTo(0);
          
        }
    }
}
