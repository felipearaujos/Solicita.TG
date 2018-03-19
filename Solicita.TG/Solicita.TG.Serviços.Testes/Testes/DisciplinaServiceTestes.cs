using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;
using Solicita.TG.Serviços.DisciplinaService;
using Solicita.TG.Serviços.DisciplinaService.Modelos;
using Solicita.TG.Persistência.Repositories.DisciplinaRepository;

namespace Solicita.TG.Serviços.Testes.Testes
{
    [TestClass]
    public class DisciplinaServiceTestes
    {
        private IDisciplinaService service;

        [TestInitialize]
        public  void SetUp()
        {
            service = new DisciplinaService.DisciplinaService();

            DisciplinaRepository disciplinaRepository = new DisciplinaRepository();
            disciplinaRepository.LimparBancoDeDados();
        }

        [TestCleanup]
        public void CleanUp()
        { 
            DisciplinaRepository disciplinaRepository = new DisciplinaRepository();
            disciplinaRepository.LimparBancoDeDados();
        }


        [TestMethod]
        public void testeDeveCriarUmaDisciplinaInformandoSeuNomeECargaHoraria()
        {
            Guid id = service.Criar("Programação Para Microinformática", 80);

            id.Should().Not.Be.EqualTo(Guid.Empty);
        }

        [TestMethod]
        public void testeDeveConsultarUmDisciplinaPeloId()
        {
            Guid id = service.Criar("Programação Para Microinformática", 80);

            id.Should().Not.Be.EqualTo(Guid.Empty);

            DisciplinaModel modelo = service.Get(id);

            modelo.Id.Should().Be.EqualTo(id);
            modelo.Nome.Should().Be.EqualTo("Programação Para Microinformática");
            modelo.CargaHoraria.Should().Be.EqualTo(80);
        }

        [TestMethod]
        public void testeDeveListarTodasAsDisciplinasCadastradas()
        {
            service.Criar("Programação Para Microinformática", 80);
            service.Criar("Engenharia de Software I", 80);
            service.Criar("Engenharia de Software II", 80);
            service.Criar("Engenharia de Software III", 80);

            List<DisciplinaModel> modelos = service.Listar();

            modelos.Count.Should().Be.EqualTo(4);
        }

        [TestMethod]
        public void testeDeveExcluirUmaDisplina()
        {
            Guid id = service.Criar("Programação Para Microinformática", 80);

            DisciplinaModel modelo = service.Get(id);

            modelo.Id.Should().Be.EqualTo(id);
            modelo.Nome.Should().Be.EqualTo("Programação Para Microinformática");
            modelo.CargaHoraria.Should().Be.EqualTo(80);

            service.Excluir(id);

            modelo = service.Get(id);

            modelo.Should().Be.Null();
        }

        [TestMethod]
        public void testeDeveAtualizarUmaDisciplinaAposAterarSeuNomeECargaHoraria()
        {
            Guid id = service.Criar("Programação Para Microinformática", 80);

            DisciplinaModel modelo = service.Get(id); 
            modelo.Id.Should().Be.EqualTo(id);
            modelo.Nome.Should().Be.EqualTo("Programação Para Microinformática");
            modelo.CargaHoraria.Should().Be.EqualTo(80);

            service.Salvar(id,"ALP",40);

            modelo = service.Get(id);
            modelo.Id.Should().Be.EqualTo(id);
            modelo.Nome.Should().Be.EqualTo("ALP");
            modelo.CargaHoraria.Should().Be.EqualTo(40);
        }


    }
}
