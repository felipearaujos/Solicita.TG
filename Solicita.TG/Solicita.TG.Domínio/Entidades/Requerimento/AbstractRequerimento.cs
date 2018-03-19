using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Requerimento
{
    public abstract class AbstractRequerimento : IRequerimento
    {
        public Guid Aluno { get; protected set; }

        public IStatus StatusAtual { get { return HistóricoDeStatus.Last(); } }

        public List<IStatus> HistóricoDeStatus { get; protected set; }

        public Guid Id { get; protected set; }

        public DateTime dataAbertura { get; protected set; }

        public DateTime dataPrevistaTérmino { get; protected set; }

        public List<string> Erros { get; protected set; }

        public TiposRequerimento.TipoRequerimento Tipo { get; protected set; }

        public AbstractRequerimento()
        {
            HistóricoDeStatus = new List<IStatus>();
            dataAbertura = DateTime.Now;
            dataPrevistaTérmino = DateTime.Now.AddDays(3);
            Erros = new List<string>();
            Id = Guid.NewGuid();
        }

        public void DefinirAluno(IAluno aluno)
        {
            if(this.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Concluido))
                || this.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Cancelado))
                || this.HistóricoDeStatus.Any((x => x.StatusType.Equals(TipoDeStatus.Indeferido))))
                throw new InvalidOperationException("Não é possível definir um aluno " +
                    "de um requerimento já fechado");

            this.Aluno = aluno.Id;
        }

        public void Finalizar(IResponsável responsavel)
        {
            if(this.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Concluido))
                || this.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Cancelado))
                || this.HistóricoDeStatus.Any((x => x.StatusType.Equals(TipoDeStatus.Indeferido))))
                throw new InvalidOperationException("Não é possível definir um aluno " +
                    "de um requerimento já fechado");

            this.HistóricoDeStatus.Last().SairDoStatus();

            this.HistóricoDeStatus.Add(
                new Status(String.Empty, responsavel, TipoDeStatus.Concluido));
        }
        
        public void TransferirResponsabilidade(IResponsável responsavel, String motivação)
        {
            if(this.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Concluido))
                || this.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Cancelado))
                || this.HistóricoDeStatus.Any((x => x.StatusType.Equals(TipoDeStatus.Indeferido))))
                throw new InvalidOperationException("Não é possível definir um aluno " +
                    "de um requerimento já fechado");

            this.HistóricoDeStatus.Last().SairDoStatus();

            this.HistóricoDeStatus.Add(
                new Status(motivação, responsavel, TipoDeStatus.EmAnálise));
        }

        public void Cancelar(String motivo, IResponsável responsavel)
        {
            if (this.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Concluido))
                || this.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Cancelado))
                || this.HistóricoDeStatus.Any((x => x.StatusType.Equals(TipoDeStatus.Indeferido))))
                throw new InvalidOperationException("Não é possível definir um aluno " +
                    "de um requerimento já fechado");


            if (String.IsNullOrEmpty(motivo))
                throw new InvalidOperationException("Não é possível cancelar um requerimento sem justificativa.");

            this.HistóricoDeStatus.Last().SairDoStatus();

            this.HistóricoDeStatus.Add(
                new Status(motivo, responsavel, TipoDeStatus.Cancelado));
        }

        public void Indeferir(String motivo, IResponsável responsavel)
        {
            if (this.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Concluido))
                || this.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Cancelado))
                || this.HistóricoDeStatus.Any((x => x.StatusType.Equals(TipoDeStatus.Indeferido))))
                throw new InvalidOperationException("Não é possível definir um aluno " +
                    "de um requerimento já fechado");

            if (String.IsNullOrEmpty(motivo))
                throw new InvalidOperationException("Não é possível indeferir um requerimento sem justificativa.");

            this.HistóricoDeStatus.Last().SairDoStatus();

            this.HistóricoDeStatus.Add(
                new Status(motivo, responsavel, TipoDeStatus.Indeferido));
        }
        
    }
}
