using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpTestsEx;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solicita.TG.Domínio.Entidades.Requerimento;
using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento;
using Solicita.TG.Domínio.Entidades.Matricula;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using Solicita.TG.Domínio.Entidades.Disciplina;
using Solicita.TG.Domínio.Entidades.FuncionarioAcademico;

namespace Solicita.TG.Domínio.Testes
{
    [TestClass]
    public class RequerimentoTestes
    {
        private IRequerimento ObjetoDeTestes;
        private IAluno AlunoObj;
        private IFuncionarioAcademico FuncionarioObj;
        private IDisciplina DisciplinaObj;

        [TestInitialize]
        public void SetUp()
        {
            AlunoObj = Aluno.CriarAluno("Felipe", "Araujo", "1840481312010", "487130392", Período.Primeiro, Turno.Noite, Guid.Empty);

            DisciplinaObj = Disciplina.CriarDisciplina("Engenharia de Software", 80);

            FuncionarioObj = FuncionarioAcademico.CriarFuncionarioAcademico("Alexandre", "Costa", TipoDeResponsável.FuncionárioAcadêmico);

            ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.AtestadoDeDisciplina, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);
        }


        [TestMethod]
        public void testeDeveCriarUmaSolicitaçãoDeDocumentos()
        {
            ObjetoDeTestes.Should().Not.Be.Null();
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoDoSolicitaçãoDeDocumentos()
        {
            ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.AtestadoDeDisciplina, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.Tipo.Should().Be.EqualTo(TipoRequerimento.SolicitaçãoDeDocumentos);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoComTipoDeDocumentoComoAtestadoDeDisciplina()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.AtestadoDeDisciplina, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.TipoDeDocumento.Should().Be.EqualTo(TiposDeDocumentos.AtestadoDeDisciplina);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoComTipoDeDocumentoComoAtestadoDeFrequencia()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.AtestadoDeFrequencia, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.TipoDeDocumento.Should().Be.EqualTo(TiposDeDocumentos.AtestadoDeFrequencia);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoComTipoDeDocumentoComoAtestadoDeMatrícula()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.AtestadoDeMatrícula, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.TipoDeDocumento.Should().Be.EqualTo(TiposDeDocumentos.AtestadoDeMatrícula);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoComTipoDeDocumentoComoAtestadoDePeríodo()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.AtestadoDePeríodo, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.TipoDeDocumento.Should().Be.EqualTo(TiposDeDocumentos.AtestadoDePeríodo);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoComTipoDeDocumentoComoHistóricoEscolar()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.TipoDeDocumento.Should().Be.EqualTo(TiposDeDocumentos.HistóricoEscolar);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoComDisciplinaInformada()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.DisciplinaInformada.Should().Be.EqualTo(DisciplinaObj.Id);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoInformandoSemestreAtual()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.InformarSemestreAtual.Should().Be.True();
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoInformandoPrevisãoDeConclusão()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.InformarPrevisãoDeConclusão.Should().Be.True();
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoInformandoPeriodo()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.InformarPeriodo.Should().Be.True();
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoInformandoCargaHorária()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.InformarCargaHorária.Should().Be.True();
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoInformandoAulaAosSabados()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.InformarAulaAosSabados.Should().Be.True();
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoComDataDeAberturaReferenteAoMomentoDeCriação()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.dataAbertura.Date.Should().Be.EqualTo(DateTime.Now.Date);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoComDataPrevistaDeterminoATresDiasPosteriorAAbertura()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.dataPrevistaTérmino.Date.Should().Be.EqualTo(DateTime.Now.AddDays(3).Date);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoComAlunoCorreto()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.Aluno.Should().Be.EqualTo(AlunoObj.Id);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoComStatusInicialDeNovoComFuncionarioComoResponsavel()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.HistóricoDeStatus.First().StatusType.Should().Be.EqualTo(TipoDeStatus.Novo);
            ObjetoDeTestes.HistóricoDeStatus.First().TipoDeResponsável.Should().Be.EqualTo(TipoDeResponsável.FuncionárioAcadêmico);
        }

        [TestMethod]
        public void testeDevePermitirFinalizarUmRequerimento()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.Finalizar(FuncionarioObj);

            ObjetoDeTestes.HistóricoDeStatus.Last().StatusType.Should().Be.EqualTo(TipoDeStatus.Concluido);
        }

        [TestMethod]
        public void testeDevePermitirCancelarUmRequerimento()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.Cancelar("Cancelamento", FuncionarioObj);

            ObjetoDeTestes.HistóricoDeStatus.Last().StatusType.Should().Be.EqualTo(TipoDeStatus.Cancelado);
            ObjetoDeTestes.HistóricoDeStatus.Last().Observação.Should().Be.EqualTo("Cancelamento");
        }

        [TestMethod]
        public void testeNãoDevePermitirCancelarUmRequerimentoSemMotivo()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.Executing(x => x.Cancelar(String.Empty, FuncionarioObj)).Throws<InvalidOperationException>();
        }

        [TestMethod]
        public void testeDeveLembrarOResponsávelQueRealizouOCancelamentoDoRequerimento()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                  TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.Cancelar("Cancelamento", FuncionarioObj);

            ObjetoDeTestes.HistóricoDeStatus.Last().IdResponsável.Should().Be.EqualTo(FuncionarioObj.Id);
        }

        [TestMethod]
        public void testeDevePermitirIndeferirUmRequerimento()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.Indeferir("Indeferimento", FuncionarioObj);

            ObjetoDeTestes.HistóricoDeStatus.Last().StatusType.Should().Be.EqualTo(TipoDeStatus.Indeferido);
            ObjetoDeTestes.HistóricoDeStatus.Last().Observação.Should().Be.EqualTo("Indeferimento");
        }

        [TestMethod]
        public void testeNãoDevePermitirIndeferirUmRequerimentoSemMotivo()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.Executing(x => x.Indeferir(String.Empty, FuncionarioObj)).Throws<InvalidOperationException>();
        }

        [TestMethod]
        public void testeDeveLembrarOResponsávelQueRealizouOIndeferimentoDoRequerimento()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                  TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);

            ObjetoDeTestes.Indeferir("Indeferimento", FuncionarioObj);

            ObjetoDeTestes.HistóricoDeStatus.Last().IdResponsável.Should().Be.EqualTo(FuncionarioObj.Id);
        }

        [TestMethod]
        public void testeDeveCriarUmRequerimentoAtribuindoOMesmoParaAEquipeAcademicaSemUmResponsavelEspecifico()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                  TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);
            
            ObjetoDeTestes.HistóricoDeStatus.Last().IdResponsável.Should().Be.EqualTo(Guid.Empty);
        }

        [TestMethod]
        public void testeDevePermitirAtribuirAResponsabilidadeDoRequerimentoAUmFuncionarioAcademico()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                  TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);


            var FuncionarioObj2 = FuncionarioAcademico.CriarFuncionarioAcademico("Alexandre", "Costa", TipoDeResponsável.FuncionárioAcadêmico);

            ObjetoDeTestes.HistóricoDeStatus.Last().StatusType.Should().Be.EqualTo(TipoDeStatus.Novo);
            ObjetoDeTestes.HistóricoDeStatus.Last().IdResponsável.Should().Be.EqualTo(Guid.Empty);

            ObjetoDeTestes.TransferirResponsabilidade(FuncionarioObj2, "Analisando solicitação");

            ObjetoDeTestes.HistóricoDeStatus.Last().StatusType.Should().Be.EqualTo(TipoDeStatus.EmAnálise);
            ObjetoDeTestes.HistóricoDeStatus.Last().IdResponsável.Should().Be.EqualTo(FuncionarioObj2.Id);
        }

        [TestMethod]
        public void testeDevePermitirNãoAtribuirAResponsabilidadeAUmFuncionarioEspecifico()
        {
            SolicitaçãoDeDocumentos ObjetoDeTestes = new SolicitaçãoDeDocumentos(AlunoObj, TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                  TiposDeDocumentos.HistóricoEscolar, "Teste 01", true, true, true, true, DisciplinaObj.Id, true, true);


            var FuncionarioObj2 = FuncionarioAcademico.CriarFuncionarioAcademico("Alexandre", "Costa", TipoDeResponsável.FuncionárioAcadêmico);

            ObjetoDeTestes.HistóricoDeStatus.Last().StatusType.Should().Be.EqualTo(TipoDeStatus.Novo);
            ObjetoDeTestes.HistóricoDeStatus.Last().IdResponsável.Should().Be.EqualTo(Guid.Empty);

            ObjetoDeTestes.TransferirResponsabilidade(FuncionarioAcademico.CriarFuncionarioAcademico(TipoDeResponsável.FuncionárioAcadêmico), "Analisando solicitação");

            ObjetoDeTestes.HistóricoDeStatus.Last().StatusType.Should().Be.EqualTo(TipoDeStatus.EmAnálise);
            ObjetoDeTestes.HistóricoDeStatus.Last().IdResponsável.Should().Be.EqualTo(Guid.Empty);
        }
    }
}
