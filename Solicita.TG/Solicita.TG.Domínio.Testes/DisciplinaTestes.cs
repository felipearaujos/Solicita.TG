using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Domínio.Entidades.Disciplina;
using SharpTestsEx;

namespace Solicita.TG.Domínio.Testes
{
    [TestClass]
    public class DisciplinaTestes
    {
        IDisciplina objetoDeTeste;

        [TestInitialize]
        public void SetUp()
        {
            objetoDeTeste = Disciplina.CriarDisciplina("Engenharia De Software III", 40);
        }

        [TestMethod]
        public void testeDeveCriarDisciplinaComNomeECargaHorária()
        {
            objetoDeTeste.Nome.Should().Be.EqualTo("Engenharia De Software III");
            objetoDeTeste.CargaHorária.Should().Be.EqualTo(40);
        }

        [TestMethod]
        public void testeDeveAlterarNomeDaDisciplina()
        {
            objetoDeTeste.Nome.Should().Be.EqualTo("Engenharia De Software III");
            objetoDeTeste.DefinirNome("Liguagem de Vacilação");
            objetoDeTeste.Nome.Should().Be.EqualTo("Liguagem de Vacilação");
        }


        [TestMethod]
        public void testeDeveAlterarCargaHoráriaDaDisciplina()
        {
            objetoDeTeste.CargaHorária.Should().Be.EqualTo(40);
            objetoDeTeste.DefinirCargaHorária(12);
            objetoDeTeste.CargaHorária.Should().Be.EqualTo(12);
        }

        [TestMethod]
        public void testeNãoDevePermitirDefinirCargaHoráriaZeroOuNegativa()
        {
            objetoDeTeste.CargaHorária.Should().Be.EqualTo(40);

            objetoDeTeste.Executing(x => x.DefinirCargaHorária(0)).Throws<InvalidOperationException>();

            objetoDeTeste.Executing(x => x.DefinirCargaHorária(-1)).Throws<InvalidOperationException>();

            objetoDeTeste.CargaHorária.Should().Be.EqualTo(40);
        }

        [TestMethod]
        public void testeNãoDevePermitirCriarDisciplinaComNomeVazioOuNulo()
        {
            objetoDeTeste.Nome.Should().Be.EqualTo("Engenharia De Software III");

            objetoDeTeste.Erros.Add("O nome da Disciplina não pode ser vazio!");
        }
    }
}
