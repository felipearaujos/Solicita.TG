using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Domínio.Entidades.Aluno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpTestsEx;
using Solicita.TG.Domínio.Entidades.Matricula;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;

namespace Solicita.TG.Domínio.Testes
{
    [TestClass]
    public class AlunoTestes
    {
        IAluno objetoDeTeste;

        [TestInitialize]
        public void ParamêtrosParaTeste()
        {
            objetoDeTeste = Aluno.CriarAluno("Jonathan", "Silva Santos", "1840481312021", "481230543", Período.Primeiro, Turno.Noite, Guid.Empty);
        }

        [TestMethod]
        public void testeDeveCriarAlunoComNomeEDadosDeMatrícula()
        {
            objetoDeTeste.Nome.Should().Be.EqualTo("Jonathan");
            objetoDeTeste.Sobrenome.Should().Be.EqualTo("Silva Santos");
            objetoDeTeste.RG.Should().Be.EqualTo("481230543");
            objetoDeTeste.Matrícula.RA.Should().Be.EqualTo("1840481312021");
            objetoDeTeste.Matrícula.Período.Should().Be.EqualTo(Período.Primeiro);
            objetoDeTeste.Matrícula.Turno.Should().Be.EqualTo(Turno.Noite);
        }

        [TestMethod]
        public void testeDevePermitirAlterarONomeDoAluno()
        {
            objetoDeTeste.Nome.Should().Be.EqualTo("Jonathan");

            objetoDeTeste.AlterarNome("Felipe");

            objetoDeTeste.Nome.Should().Be.EqualTo("Felipe");
        }

        [TestMethod]
        public void testeDevePermitirAlterarOSobrenomeDoAluno()
        {
            objetoDeTeste.Sobrenome.Should().Be.EqualTo("Silva Santos");

            objetoDeTeste.AlterarSobrenome("Araújo da Silva");

            objetoDeTeste.Sobrenome.Should().Be.EqualTo("Araújo da Silva");
        }

        [TestMethod]
        public void testeDeveVerificarQueOTipoDeResponsávelDoAlunoEstáCorreto()
        {
            objetoDeTeste.TipoDeResponsavel.Should().Be.EqualTo(TipoDeResponsável.Aluno);
        }

        [TestMethod]
        public void testeDevePermitirAlterarOPeríodoDaMatrículaDoAluno()
        {
            objetoDeTeste.Matrícula.Período.Should().Be.EqualTo(Período.Primeiro);

            objetoDeTeste.AlterarPeríodo(Período.Quarto);

            objetoDeTeste.Matrícula.Período.Should().Be.EqualTo(Período.Quarto);
        }

        [TestMethod]
        public void testeDevePermitirAlterarOTurnoDaMatrículaDoAluno()
        {
            objetoDeTeste.Matrícula.Turno.Should().Be.EqualTo(Turno.Noite);

            objetoDeTeste.AlterarTurno(Turno.Tarde);

            objetoDeTeste.Matrícula.Turno.Should().Be.EqualTo(Turno.Tarde);
        }

        [TestMethod]
        public void testeDevePermitirDefinirMatriculaDoAluno()
        {
            objetoDeTeste.Matrícula.Should().Not.Be.Null();
            objetoDeTeste.Matrícula.RA.Should().Be.EqualTo("1840481312021");
            objetoDeTeste.Matrícula.Período.Should().Be.EqualTo(Período.Primeiro);
            objetoDeTeste.Matrícula.Turno.Should().Be.EqualTo(Turno.Noite);
            objetoDeTeste.Matrícula.Curso.Should().Be.EqualTo(Guid.Empty);

            objetoDeTeste.DefinirMatrícula("1840481312022", Período.Novo, Turno.Manhã, Guid.Empty);

            objetoDeTeste.Matrícula.Should().Not.Be.Null();
            objetoDeTeste.Matrícula.RA.Should().Be.EqualTo("1840481312022");
            objetoDeTeste.Matrícula.Período.Should().Be.EqualTo(Período.Novo);
            objetoDeTeste.Matrícula.Turno.Should().Be.EqualTo(Turno.Manhã);
            objetoDeTeste.Matrícula.Curso.Should().Be.EqualTo(Guid.Empty);
        }

        [TestMethod]
        public void testeDevePermitirDefinirEmailDoAluno()
        {
            objetoDeTeste.Email.Should().Be.EqualTo(null);

            objetoDeTeste.DefinirEmail("solicitatg@gmail.com");

            objetoDeTeste.Email.Should().Be.EqualTo("solicitatg@gmail.com");
        }

        [TestMethod]
        public void testeDeveGerarErrosAoTentarCriarAlunoSemNomeESobrenome()
        {
            objetoDeTeste = Aluno.CriarAluno("", "", "1840481312021", "429434121", Período.Primeiro, Turno.Noite, Guid.Empty);

            objetoDeTeste.Erros.Count.Should().Be.EqualTo(2);
        }
    }
}
