using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Persistência.Repositories.CursoRepository;
using Solicita.TG.Domínio.Entidades.Curso;
using Solicita.TG.Domínio.Entidades.Disciplina;
using System.Collections.Generic;
using Solicita.TG.Persistência.Repositories.DisciplinaRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpTestsEx;

namespace Solicita.TG.Persistência.Testes
{
    [TestClass]
    public class CursoRepositoryTestes
    {
        CursoRepository objetoDeTeste;
        ICurso entidade;
        IDisciplina ADM;
        IDisciplina ALP;

        [TestInitialize]
        public void SetUp()
        {
            objetoDeTeste = new CursoRepository();
            objetoDeTeste.LimparBancoDeDados();

            DisciplinaRepository disciplinaRepository = new DisciplinaRepository();

            ADM = Disciplina.CriarDisciplina("Administração", 40);
            disciplinaRepository.Salvar(ADM);

            ALP = Disciplina.CriarDisciplina("Algoritmo e Lógica de Programação", 80);
            disciplinaRepository.Salvar(ALP);
        }

        [TestCleanup]
        public void Clean()
        {
            objetoDeTeste.LimparBancoDeDados();
        }
        

        [TestMethod]
        public void testeDeveCadastrarUmCursoEConsultarPeloSeuId()
        {

            entidade = Curso.CriarCurso("Análise e Desenvolvimento de Sistemas");
            entidade.AdicionarDisciplina(ADM);

            objetoDeTeste.Salvar(entidade);

            ICurso entidadeDoDB = objetoDeTeste.GetByID(entidade.Id);
            entidadeDoDB.Nome.Should().Be.EqualTo("Análise e Desenvolvimento de Sistemas");
            entidadeDoDB.GradeCurricular.Count.Should().Be.EqualTo(1);
            entidadeDoDB.GradeCurricular.Any(x => x.Nome.Equals("Administração")).Should().Be.True();
        
            
        }


        [TestMethod]
        public void testeDeveListarTodosOsCursos()
        {

            ICurso ADS = Curso.CriarCurso("Análise e Desenvolvimento de Sistemas");
            ADS.AdicionarDisciplina(ALP);
            
            ICurso AGRO = Curso.CriarCurso("Agronegocio");
            AGRO.AdicionarDisciplina(ADM);


            objetoDeTeste.Salvar(ADS);
            objetoDeTeste.Salvar(AGRO);

            List<ICurso> ListaDeCursos = objetoDeTeste.ListAll();
            ListaDeCursos.Count.Should().Be.EqualTo(2);
            ListaDeCursos.Any(x => x.Nome.Equals("Análise e Desenvolvimento de Sistemas")
                           && x.CargaHorária.Equals(80)
                           && x.GradeCurricular.Count().Equals(1) 
                           && x.GradeCurricular.Any(y => y.Nome.Equals("Algoritmo e Lógica de Programação")));

            ListaDeCursos.Any(x => x.Nome.Equals("Agronegocio")
               && x.CargaHorária.Equals(40)
               && x.GradeCurricular.Count().Equals(1)
               && x.GradeCurricular.Any(y => y.Nome.Equals("Administração")));
        }


        [TestMethod]
        public void testeDeveAtualizarAsInformaçõesBasicasDoCurso()
        {
            ICurso ADS = Curso.CriarCurso("Análise e Desenvolvimento de Sistemas");
            ADS.AdicionarDisciplina(ALP);

            objetoDeTeste.Salvar(ADS);

            ADS.DefinirNome("Jogos Digitais");

            objetoDeTeste.Salvar(ADS);

            ICurso entidadeDoBD = objetoDeTeste.GetByID(ADS.Id);
            entidadeDoBD.Nome.Should().Be.EqualTo("Jogos Digitais");
            entidadeDoBD.GradeCurricular.Count.Should().Be.EqualTo(1);

        }


        [TestMethod]
        public void testeDeveAtualizarAsDisciplinasDoCursoDoCurso()
        {
            ICurso entidadeDoBD;

            ICurso ADS = Curso.CriarCurso("Análise e Desenvolvimento de Sistemas");
            ADS.AdicionarDisciplina(ALP);
            ADS.AdicionarDisciplina(ADM);

            objetoDeTeste.Salvar(ADS);

            entidadeDoBD = objetoDeTeste.GetByID(ADS.Id);
            entidadeDoBD.Nome.Should().Be.EqualTo("Análise e Desenvolvimento de Sistemas");
            entidadeDoBD.GradeCurricular.Count.Should().Be.EqualTo(2);

            ADS.DefinirNome("Jogos Digitais");
            ADS.RemoverDisciplina(ADM);

            objetoDeTeste.Salvar(ADS);

            entidadeDoBD = objetoDeTeste.GetByID(ADS.Id);
            entidadeDoBD.Nome.Should().Be.EqualTo("Jogos Digitais");
            entidadeDoBD.GradeCurricular.Count.Should().Be.EqualTo(1);

        }


        [TestMethod]
        public void testeDeveExcluirUMCurso()
        {
            entidade = Curso.CriarCurso("Análise e Desenvolvimento de Sistemas");
            entidade.AdicionarDisciplina(ADM);

            objetoDeTeste.Salvar(entidade);

            ICurso entidadeDoDB = objetoDeTeste.GetByID(entidade.Id);
            entidadeDoDB.Nome.Should().Be.EqualTo("Análise e Desenvolvimento de Sistemas");
            entidadeDoDB.GradeCurricular.Count.Should().Be.EqualTo(1);
            entidadeDoDB.GradeCurricular.Any(x => x.Nome.Equals("Administração")).Should().Be.True();


            objetoDeTeste.Excluir(entidade);

            entidadeDoDB = objetoDeTeste.GetByID(entidade.Id);
            entidadeDoDB.Should().Be.Null();

        }
    }
}
