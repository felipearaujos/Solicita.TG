using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Requerimento
{
    public class SolicitaçãoDeDocumentos : AbstractRequerimento
    {
        public TipoDeMotivosSolicitacaoDeDocumentos TipoDeMotivo { get; private set; }

        public TiposDeDocumentos TipoDeDocumento { get; private set;}

        public String MotivoEspecificado { get; private set; }

        public Boolean InformarPeriodo { get; private set; }

        public Boolean InformarDisciplina { get; private set; }

        public Guid DisciplinaInformada { get; private set; }

        public Boolean InformarPrevisãoDeConclusão { get; private set; }

        public Boolean InformarSemestreAtual { get; private set; }

        public Boolean InformarCargaHorária { get; private set; }

        public Boolean InformarAulaAosSabados { get; private set; }

        public SolicitaçãoDeDocumentos(Guid idRequerimento, Guid aluno, DateTime dataAbertura, DateTime dataTermino)
        {
            this.Aluno = aluno;
            this.Id = idRequerimento;
            this.dataAbertura = dataAbertura;
            this.dataPrevistaTérmino = dataPrevistaTérmino;
            this.Erros = new List<string>();
            this.HistóricoDeStatus = new List<StatusRequerimento.IStatus>();
            this.Tipo = TipoRequerimento.SolicitaçãoDeDocumentos;
        }

        public SolicitaçãoDeDocumentos(Guid idRequerimento, Guid aluno, Guid idStatusAtual,
            TipoDeMotivosSolicitacaoDeDocumentos tipoDeMotivo, TiposDeDocumentos tipoDeDocumento, DateTime dataAbertura,
            DateTime dataPrevistaDeTérmino,  String especificacaoDoMotivo,
            Boolean informarSemestreAtual, Boolean informarCargaHoraria, Boolean informarAulaAosSabados,
            Boolean informarPrevisãoDeConclusão, Guid disciplinaInformada, Boolean informarDisciplina, 
            Boolean informarPeriodo)
            {
                this.Aluno = aluno;
                this.Id = idRequerimento;
                this.dataAbertura = dataAbertura;
                this.dataPrevistaTérmino = dataPrevistaTérmino;
                this.Erros = new List<string>();
                this.HistóricoDeStatus = new List<StatusRequerimento.IStatus>();
                this.InformarAulaAosSabados = informarAulaAosSabados;
                this.InformarCargaHorária = informarCargaHoraria;
                this.InformarSemestreAtual = informarSemestreAtual;
                this.MotivoEspecificado = especificacaoDoMotivo;
                this.Tipo = TipoRequerimento.SolicitaçãoDeDocumentos;
                this.TipoDeDocumento = TipoDeDocumento;
                this.InformarPrevisãoDeConclusão = informarPrevisãoDeConclusão;
                this.DisciplinaInformada = disciplinaInformada;
                this.InformarDisciplina = informarDisciplina;
                this.InformarPeriodo = informarPeriodo;
                this.TipoDeMotivo = tipoDeMotivo;
            }

        public SolicitaçãoDeDocumentos(IAluno aluno, TipoDeMotivosSolicitacaoDeDocumentos tipoDeMotivo, TiposDeDocumentos tipoDeDocumento,  String especificacaoDoMotivo,
            Boolean informarSemestreAtual, Boolean informarCargaHoraria, Boolean informarAulaAosSabados,
            Boolean informarPrevisãoDeConclusão, Guid disciplinaInformada, Boolean informarDisciplina,
            Boolean informarPeriodo) :base()
        {
            this.DefinirAluno(aluno);
            this.TipoDeMotivo = tipoDeMotivo;
            this.MotivoEspecificado = especificacaoDoMotivo;
            this.InformarSemestreAtual = informarSemestreAtual;
            this.InformarCargaHorária = informarCargaHoraria;
            this.InformarAulaAosSabados = informarAulaAosSabados;
            this.Tipo = TipoRequerimento.SolicitaçãoDeDocumentos;
            this.TipoDeDocumento = tipoDeDocumento;
            this.InformarPrevisãoDeConclusão = informarPrevisãoDeConclusão;
            this.DisciplinaInformada = disciplinaInformada;
            this.InformarDisciplina = informarDisciplina;
            this.InformarPeriodo = informarPeriodo;
            this.TipoDeMotivo = tipoDeMotivo;
            this.HistóricoDeStatus.Add(new Status(String.Empty, Guid.Empty, TipoDeResponsável.FuncionárioAcadêmico, TipoDeStatus.Novo));
        }
    }
}
