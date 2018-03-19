using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento
{
    public class Status : IStatus
    {
        public Guid Id { get; private set; }

        public DateTime DataEntrada { get; private set; }

        public DateTime DataSaida { get; private set; }

        public TipoDeStatus StatusType { get; private set; }
        
        public Guid IdResponsável { get; private set; }

        public TipoDeResponsável TipoDeResponsável { get; private set; }

        public String Observação { get; private set; }

        public Status(String observação, Guid idResponsavel, TipoDeResponsável tipoResponsável, TipoDeStatus status)
        {
            Id = Guid.NewGuid();
            DataEntrada = DateTime.Now;
            StatusType = status;
            Observação = observação;
            IdResponsável = idResponsavel;
            TipoDeResponsável = tipoResponsável;
        }

        public Status(String observação, IResponsável responsável, TipoDeStatus status)
        {
            Id = Guid.NewGuid();
            DataEntrada = DateTime.Now;
            StatusType = status;
            Observação = observação;
            IdResponsável = responsável.Id;
            TipoDeResponsável = responsável.TipoDeResponsavel;
        }

        public void SairDoStatus()
        {
            this.DataSaida = DateTime.Now;
        }

        public Status(Guid IdStatus, String observação, Guid idResponsavel, TipoDeResponsável tipoResponsável, DateTime dataEntrada, DateTime dataSaida, TipoDeStatus tipoDeStatus)
        {
            Id = IdStatus;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
            StatusType = tipoDeStatus;
            IdResponsável = idResponsavel;
            TipoDeResponsável = tipoResponsável;
            Observação = observação;
        }
    }
}
