using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Persistência.Repositories.RepositórioAluno;
using Solicita.TG.Domínio.Entidades.Aluno;
using SharpTestsEx;
using Solicita.TG.Domínio.Entidades.Matricula;
using Solicita.TG.Persistência.Repositories.CursoRepository;
using Solicita.TG.Domínio.Entidades.Curso;

namespace Solicita.TG.Persistência.Testes
{
    [TestClass]
    public class RepositórioAlunoTestes
    {
        private AlunoRepository ObjetoDeTeste;
        private IAluno UmAluno;
        private IAluno OutroAluno;
        private List<IAluno> ListaDeAlunos;        
        private Guid idCurso;


        [TestInitialize]
        public void SetUp()
        {
            ObjetoDeTeste = new AlunoRepository();
            ObjetoDeTeste.LimparBancoDeDados();
            CursoRepository cursoRepository = new CursoRepository();

            Curso curso = Curso.CriarCurso("ADS");
            idCurso = curso.Id;
            cursoRepository.Salvar(curso);
            

            ObjetoDeTeste.LimparBancoDeDados();
        }

        [TestCleanup]
        public void LimparBancoDeDados()
        {
            //ObjetoDeTeste.LimparBancoDeDados();
        }

        [TestMethod]
        public void testeDeveCadastrarUmAlunoeConsultarUmAlunoPeloSeuID()
        {
            UmAluno = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "429434121", Período.Primeiro, Turno.Noite, idCurso);

            ObjetoDeTeste.Salvar(UmAluno);

            OutroAluno = ObjetoDeTeste.GetByID(UmAluno.Id);

            OutroAluno.Id.Should().Be.EqualTo(UmAluno.Id);
            OutroAluno.Nome.Should().Be.EqualTo(UmAluno.Nome);
            OutroAluno.Sobrenome.Should().Be.EqualTo(UmAluno.Sobrenome);
            
        }

        [TestMethod]
        public void testeDeveLimparATabelaAlunoDoBancoDeDados()
        {
            
        }

        [TestMethod]
        public void testeDeveAtualizarONomeDeUmAluno()
        {
            UmAluno = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "429434121", Período.Primeiro, Turno.Noite, idCurso);

            UmAluno.Nome.Should().Be.EqualTo("Felipe");
            
        }

        [TestMethod]
        public void testeUmAlunoDeveTerSeuCurso()
        {
            UmAluno = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "429434121", Período.Primeiro, Turno.Noite, Guid.Empty);

            Boolean erro = false;
            try
            {
                ObjetoDeTeste.Salvar(UmAluno);
            }
            catch (Exception ex)
            {
                erro = true;
            }

            erro.Should().Be.True();
            
        }


        [TestMethod]
        public void testeDeveAtualizarOSobrenomeDeUmAluno()
        {
            UmAluno = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "429434121", Período.Primeiro, Turno.Noite, idCurso);

            ObjetoDeTeste.Salvar(UmAluno);

            UmAluno.AlterarSobrenome("Silva");

            ObjetoDeTeste.Salvar(UmAluno);

            OutroAluno = ObjetoDeTeste.GetByID(UmAluno.Id);

            OutroAluno.Nome.Should().Be.EqualTo("Felipe");
            OutroAluno.Sobrenome.Should().Be.EqualTo("Silva");


        }

        [TestMethod]
        public void testeDeveExcluirUmAluno()
        {
            UmAluno = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "429434121", Período.Primeiro, Turno.Noite, idCurso);

            ObjetoDeTeste.Salvar(UmAluno);

            OutroAluno = ObjetoDeTeste.GetByID(UmAluno.Id);
            OutroAluno.Id.Should().Be.EqualTo(UmAluno.Id);

            ObjetoDeTeste.Excluir(UmAluno);

            OutroAluno = ObjetoDeTeste.GetByID(UmAluno.Id);
            OutroAluno.Should().Be.Null();
        }

        [TestMethod]
        public void testeDeveListarTodosOsAlunosCadastrados()
        {
            UmAluno = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "429434121", Período.Primeiro, Turno.Noite, idCurso);
            OutroAluno = Aluno.CriarAluno("Jonathan", "Santos", "1840481312021", "429434122", Período.Primeiro, Turno.Noite, idCurso);
            ObjetoDeTeste.Salvar(UmAluno);
            ObjetoDeTeste.Salvar(OutroAluno);

            ListaDeAlunos = ObjetoDeTeste.ListAll();
            ListaDeAlunos.Count.Should().Be.EqualTo(2);

            ListaDeAlunos.Any(x => x.Nome.Equals("Felipe")).Should().Be.True();
            ListaDeAlunos.Any(x => x.Sobrenome.Equals("Araujo")).Should().Be.True();

            ListaDeAlunos.Any(x => x.Nome.Equals("Jonathan")).Should().Be.True();
            ListaDeAlunos.Any(x => x.Sobrenome.Equals("Santos")).Should().Be.True();
        }




        [TestMethod]
        public void testeDeveLimparATabelaAlunoDoBancoDeDadosDandoErro()
        {

        }

        [TestMethod]
        public void testeDeveAtualizarONomeDeUmAlunoDandoErro()
        {
            UmAluno = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "429434121", Período.Primeiro, Turno.Noite, idCurso);

            UmAluno.Nome.Should().Be.EqualTo("Felipe");

        }

        [TestMethod]
        public void testeUmAlunoDeveTerSeuCursoDandoErro()
        {
            UmAluno = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "429434121", Período.Primeiro, Turno.Noite, Guid.Empty);

            Boolean erro = false;
            try
            {
                ObjetoDeTeste.Salvar(UmAluno);
            }
            catch (Exception ex)
            {
                erro = true;
            }

            erro.Should().Be.True();

        }


        [TestMethod]
        public void testeDeveAtualizarOSobrenomeDeUmAlunoDandoErro()
        {
            UmAluno = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "429434121", Período.Primeiro, Turno.Noite, idCurso);

            ObjetoDeTeste.Salvar(UmAluno);

            UmAluno.AlterarSobrenome("Silva");

            ObjetoDeTeste.Salvar(UmAluno);

            OutroAluno = ObjetoDeTeste.GetByID(UmAluno.Id);

            OutroAluno.Nome.Should().Be.EqualTo("Felipe");
            OutroAluno.Sobrenome.Should().Be.EqualTo("Silva");


        }

        [TestMethod]
        public void testeDeveExcluirUmAlunoNovamente()
        {
            UmAluno = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "429434121", Período.Primeiro, Turno.Noite, idCurso);

            ObjetoDeTeste.Salvar(UmAluno);

            OutroAluno = ObjetoDeTeste.GetByID(UmAluno.Id);
            OutroAluno.Id.Should().Be.EqualTo(UmAluno.Id);

            ObjetoDeTeste.Excluir(UmAluno);

            OutroAluno = ObjetoDeTeste.GetByID(UmAluno.Id);
            OutroAluno.Should().Be.Null();
        }



    }
}
