using Solicita.TG.Domínio.Entidades.FuncionarioAcademico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento
{
    public interface IStatus
    {
        Guid Id { get; }
        DateTime DataEntrada { get; }
        DateTime DataSaida { get; }
        TipoDeStatus StatusType { get; }
        Guid IdResponsável { get; }
        TipoDeResponsável TipoDeResponsável { get; }
        String Observação { get; }

        void SairDoStatus();
    }
}
