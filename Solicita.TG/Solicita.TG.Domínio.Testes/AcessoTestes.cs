using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Domínio.Entidades.Acesso;
using SharpTestsEx;

namespace Solicita.TG.Domínio.Testes
{
    [TestClass]
    public class AcessoTestes
    {
        IAcesso objetoDeTeste;

        [TestInitialize]
        public void ParamêtrosParaTeste()
        {
            objetoDeTeste = Acesso.CriarAcesso(Guid.NewGuid(),"Professor", "Prof1010!", TipoDeAcesso.Cordenador);
        }

        [TestMethod]
        public void testeDeveCriarUmAcessoComUsuárioSenhaETIpoDeAcesso()
        {
            objetoDeTeste.Id.Should().Not.Be.Null();
            objetoDeTeste.Usuário.Should().Be.EqualTo("Professor");
            objetoDeTeste.Senha.Should().Be.EqualTo("C6hqklCx6pq62Tk94YGrCg==");
            objetoDeTeste.TipoDeUsuário.Should().Be.EqualTo(TipoDeAcesso.Cordenador);
        }

        [TestMethod]
        public void testeDevePermitirAlterarSenhaDoUsuario()
        {
            objetoDeTeste.Senha.Should().Be.EqualTo("C6hqklCx6pq62Tk94YGrCg==");

            objetoDeTeste.AlterarSenha("Professor!");

            objetoDeTeste.Senha.Should().Be.EqualTo("1Qbl39zAOLIzD0XLmxcYGA==");
        }

        [TestMethod]
        public void testeNãoDevePermitirAlterarUmaSenhaEmBrancoOuNula()
        {
            objetoDeTeste.AlterarSenha(String.Empty);

            objetoDeTeste.Erros.Contains("Não é possível adicionar uma senha vazia!").Should().Be.True();
        }

        [TestMethod]
        public void testeNãoDevePermitirAlterarUmaSenhMenorQueSeisDigitos()
        {
            objetoDeTeste.AlterarSenha("123");

            objetoDeTeste.Erros.Contains("A senha deve conter no mínimo 6 caracteres").Should().Be.True();
        }
    }
}
