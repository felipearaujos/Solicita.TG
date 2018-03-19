using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento
{
    public class Indeferido: IStatus
    {
        public Guid Id { get; private set; }
        public DateTime DataEntrada { get; private set; }

        public DateTime DataSaida { get; private set; }

        public TipoDeStatus StatusType { get; private set; }

        public Responsável Responsável { get; private set; }

        public String Observação { get; private set; }

        public Indeferido(String observação)
        {
            Id = Guid.NewGuid();
            DataEntrada = DateTime.Now;
            StatusType = StatusRequerimento.TipoDeStatus.Indeferido;
            Responsável = StatusRequerimento.Responsável.Aluno;
            Observação = observação;
        }
    }
}
