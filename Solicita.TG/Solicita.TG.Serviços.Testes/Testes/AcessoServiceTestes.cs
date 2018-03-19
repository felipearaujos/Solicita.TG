using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Persistência.Repositories.AcessoRepository;
using Solicita.TG.Serviços.Acesso;
using Solicita.TG.Serviços.Acesso.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpTestsEx;
using Solicita.TG.Domínio.Entidades.Acesso;

namespace Solicita.TG.Serviços.Testes.Testes
{
    [TestClass]
    public class AcessoServiceTestes
    {
        private IAcessoService service;
        private Guid idAcesso;

        [TestInitialize]
        public void Setup()
        {
            AcessoRepository acessoRepository = new AcessoRepository();
            acessoRepository.LimparBancoDeDados();
            service = new AcessoService();

            idAcesso = service.CriarAcesso(Guid.NewGuid(),"Elias", "sacola10!", 1);
        }

        [TestCleanup]
        public void CleanUp()
        {
            AcessoRepository acessoRepository = new AcessoRepository();
            acessoRepository.LimparBancoDeDados();
        }

        [TestMethod]
        public void testeDeveCriarUmAcesso()
        {
            AcessoModel modelo = service.Get(idAcesso);

            modelo.Usuario.Should().Be.EqualTo("Elias");
            modelo.Senha.Should().Be.EqualTo("oXUuZYas71eRBKSBO+709w==");
            modelo.TipoDeUsuario.Tipo.Should().Be.EqualTo(1);
        }

        [TestMethod]
        public void testeDeveListarTodosProfessores()
        {
            Guid idOutroAcesso = service.CriarAcesso(Guid.NewGuid(),"Patricia", "fruta20!", 2);

            List<AcessoModel> modelo = service.ListarTodosAcessos();

            modelo.Count.Should().Be.EqualTo(2);
            modelo.Any(x => x.Usuario.Equals("Patricia")).Should().Be.True();
            modelo.Any(x => x.Senha.Equals("5GVEJTJm3IHY+NnPDH0ufw==")).Should().Be.True();
            modelo.Any(x => x.TipoDeUsuario.Tipo.Equals(2)).Should().Be.True();

            modelo.Any(x => x.Usuario.Equals("Elias")).Should().Be.True();
            modelo.Any(x => x.Senha.Equals("oXUuZYas71eRBKSBO+709w==")).Should().Be.True();
            modelo.Any(x => x.TipoDeUsuario.Tipo.Equals(1)).Should().Be.True();
        }
    }
}
