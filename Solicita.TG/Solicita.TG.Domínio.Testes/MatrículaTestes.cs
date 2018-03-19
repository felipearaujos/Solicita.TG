using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Domínio.Entidades.Matricula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpTestsEx;
using Solicita.TG.Domínio.Entidades.Aluno;

namespace Solicita.TG.Domínio.Testes
{
    [TestClass]
    public class MatrículaTestes
    {

        IMatrícula objetoDeTeste;

        [TestInitialize]
        public void ParamêtrosParaTeste()
        {
            IAluno umAluno = Aluno.CriarAluno("Jonathan", "Silva Santos", "1840481312021", "429434121", Período.Primeiro, Turno.Noite, Guid.Empty);
            objetoDeTeste = new Matrícula(umAluno, "1840481312021", Período.Primeiro, Turno.Noite, Guid.Empty);
        }


        [TestMethod]
        public void testeDeveCriarMatriculaComRAPeríodoETurno()
        {
            objetoDeTeste.RA.Should().Be.EqualTo("1840481312021");
            objetoDeTeste.Período.Should().Be.EqualTo(Período.Primeiro);
            objetoDeTeste.Turno.Should().Be.EqualTo(Turno.Noite);
        }
    }
}
