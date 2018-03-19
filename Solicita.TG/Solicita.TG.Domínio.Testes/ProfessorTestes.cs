using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Domínio.Entidades.Professor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpTestsEx;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;

namespace Solicita.TG.Domínio.Testes
{
    [TestClass]
    public class ProfessorTestes
    {
        private IProfessor objetoDeTeste;

        [TestInitialize]
        public void Setup()
        {
            objetoDeTeste = Professor.CriarProfessor("José Carlos", "Bortot", true);
        }

        [TestMethod]
        public void TesteDeveCriarProfessorComNomeSobrenomeECondiçãoDeCordenador()
        {
            objetoDeTeste.Id.Should().Not.Be.Null();
            objetoDeTeste.Nome.Should().Be.EqualTo("José Carlos");
            objetoDeTeste.Sobrenome.Should().Be.EqualTo("Bortot");
            objetoDeTeste.ÉCordenador.Should().Be.True();
        }

        [TestMethod]
        public void testeDevePermitirAlterarONomeDoProfessor()
        {
            objetoDeTeste.Nome.Should().Be.EqualTo("José Carlos");

            objetoDeTeste.AlterarNome("Bruno");

            objetoDeTeste.Nome.Should().Be.EqualTo("Bruno");
        }

        [TestMethod]
        public void testeDevePermitirAlterarOSobrenomeDoProfessor()
        {
            objetoDeTeste.Sobrenome.Should().Be.EqualTo("Bortot");

            objetoDeTeste.AlterarSobrenome("panccioni");

            objetoDeTeste.Sobrenome.Should().Be.EqualTo("panccioni");
        }

        [TestMethod]
        public void testeDeveVerificarTipoReResponsavelCorretoAtribuidoAoProfessor()
        {
            objetoDeTeste.TipoDeResponsavel.Should().Be.EqualTo(TipoDeResponsável.Professor);
        }

        [TestMethod]
        public void testeNãoDevePermitirAlterarONomeDoProfessorParaNuloOuVazio()
        {
            objetoDeTeste.Nome.Should().Be.EqualTo("José Carlos");

            objetoDeTeste.AlterarNome(String.Empty);

            objetoDeTeste.Erros.Add("Não é possivel criar um Professor com Nome nulo.");
        }

        [TestMethod]
        public void testeNãoDevePermitirAlterarOSobrenomeDoProfessorParaNuloOuVazio()
        {
            objetoDeTeste.Sobrenome.Should().Be.EqualTo("Bortot");

            objetoDeTeste.AlterarSobrenome(String.Empty);

            objetoDeTeste.Erros.Add("Não é possivel criar um Professor com Sobrenome nulo.");
        }

        [TestMethod]
        public void testeDevePermitirAlterarCondiçãoDeCordenador()
        {
            objetoDeTeste.ÉCordenador.Should().Be.True();

            objetoDeTeste.DefinirComoCordenador(false);

            objetoDeTeste.ÉCordenador.Should().Be.False();
        }
    }
}
