using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Domínio.Entidades.Aluno;
using SharpTestsEx;
using Solicita.TG.Persistência.Repositories.AcessoRepository;
using Solicita.TG.Domínio.Entidades.Acesso;

namespace Solicita.TG.Persistência.Testes
{
    [TestClass]
    public class AcessoRepositoryTestes
    {
        private AcessoRepository ObjetoDeTeste;
        private IAcesso UmAcesso;
        private IAcesso OutroAcesso;
        private List<IAcesso> ListaDeAcessos;


        [TestInitialize]
        public void SetUp()
        {
            ObjetoDeTeste = new AcessoRepository();
            ObjetoDeTeste.LimparBancoDeDados();
        }

        [TestCleanup]
        public void LimparBancoDeDados()
        {
            ObjetoDeTeste.LimparBancoDeDados();
        }

        [TestMethod]
        public void testeDeveCadastrarUmAcessoeConsultarUmAlunoPeloSeuID()
        {
            UmAcesso = Acesso.CriarAcesso(Guid.NewGuid(),"Elias", "sacola10!", TipoDeAcesso.FuncionárioAcadêmico);

            ObjetoDeTeste.Salvar(UmAcesso);

            OutroAcesso = ObjetoDeTeste.GetByID(UmAcesso.Id);

            OutroAcesso.Id.Should().Be.EqualTo(UmAcesso.Id);
            OutroAcesso.Usuário.Should().Be.EqualTo(UmAcesso.Usuário);
            OutroAcesso.Senha.Should().Be.EqualTo(UmAcesso.Senha);
            OutroAcesso.TipoDeUsuário.Should().Be.EqualTo(UmAcesso.TipoDeUsuário);
        }


   

        [TestMethod]
        public void testeDeveAtualizarSenhaDeUmAceso()
        {
            UmAcesso = Acesso.CriarAcesso(Guid.NewGuid(),"Elias", "sacola10!", TipoDeAcesso.FuncionárioAcadêmico);

            ObjetoDeTeste.Salvar(UmAcesso);

            UmAcesso.AlterarSenha("fruta20@");

            ObjetoDeTeste.Salvar(UmAcesso);

            OutroAcesso = ObjetoDeTeste.GetByID(UmAcesso.Id);

            OutroAcesso.Senha.Should().Be.EqualTo(OutroAcesso.Senha);


        }

        [TestMethod]
        public void testeDeveExcluirUmAcesso()
        {
            UmAcesso = Acesso.CriarAcesso(Guid.NewGuid(), "Elias", "sacola10!", TipoDeAcesso.FuncionárioAcadêmico);

            ObjetoDeTeste.Salvar(UmAcesso);

            OutroAcesso = ObjetoDeTeste.GetByID(UmAcesso.Id);
            OutroAcesso.Id.Should().Be.EqualTo(UmAcesso.Id);

            ObjetoDeTeste.Excluir(UmAcesso);

            OutroAcesso = ObjetoDeTeste.GetByID(UmAcesso.Id);
            OutroAcesso.Should().Be.Null();
        }

        [TestMethod]
        public void testeDeveListarTodosOsAlunosCadastrados()
        {
            UmAcesso = Acesso.CriarAcesso(Guid.NewGuid(),"Elias", "sacola10!", TipoDeAcesso.FuncionárioAcadêmico);
            OutroAcesso = Acesso.CriarAcesso(Guid.NewGuid(),"Patricia", "fruta20@", TipoDeAcesso.Diretor);

            ObjetoDeTeste.Salvar(UmAcesso);
            ObjetoDeTeste.Salvar(OutroAcesso);

            ListaDeAcessos = ObjetoDeTeste.ListAll();
            ListaDeAcessos.Count.Should().Be.EqualTo(2);

            ListaDeAcessos.Any(x => x.Usuário.Equals("Patricia")).Should().Be.True();
            ListaDeAcessos.Any(x => x.Senha.Equals(OutroAcesso.Senha)).Should().Be.True();
            ListaDeAcessos.Any(x => x.TipoDeUsuário.Equals(TipoDeAcesso.Diretor)).Should().Be.True();

            ListaDeAcessos.Any(x => x.Usuário.Equals("Elias")).Should().Be.True();
            ListaDeAcessos.Any(x => x.Senha.Equals(UmAcesso.Senha)).Should().Be.True();
            ListaDeAcessos.Any(x => x.TipoDeUsuário.Equals(TipoDeAcesso.FuncionárioAcadêmico)).Should().Be.True();
        }


    }
}
