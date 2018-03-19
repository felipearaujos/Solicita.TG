using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;
using Solicita.TG.Domínio.EntidadeAplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Testes
{
    [TestClass]
    public class QuantidadePorTipoTestes
    {
        QuantidadePorTipo objetoDeTeste;


        [TestInitialize]
        public void ParamêtrosParaTeste()
        {
            objetoDeTeste = new QuantidadePorTipo("Declaração de Matrícula", 25);
        }

        [TestMethod]
        public void testeDevePermitirCriarQuantidadePorTipoComDadosCorretos()
        {
            objetoDeTeste.CountPorTipo.Should().Be.EqualTo(25);
            objetoDeTeste.Tipo.Should().Be.EqualTo("Declaração de Matrícula");
        }
    }
}
