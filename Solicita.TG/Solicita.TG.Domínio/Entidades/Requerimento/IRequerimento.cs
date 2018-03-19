using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Entidade;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Requerimento
{
    public interface IRequerimento : IEntidade
    {
        Guid Aluno { get; }

        IStatus StatusAtual { get; }

        DateTime dataPrevistaTérmino { get; }

        DateTime dataAbertura { get; }

        List<IStatus> HistóricoDeStatus { get; }

        TipoRequerimento Tipo { get; }

        void DefinirAluno(IAluno aluno);

        void Finalizar(IResponsável responsavel);

        void TransferirResponsabilidade(IResponsável responsavel, String observação);

        void Cancelar(String motivo, IResponsável responsavel);

        void Indeferir(String motivo, IResponsável responsavel);
    }
}
