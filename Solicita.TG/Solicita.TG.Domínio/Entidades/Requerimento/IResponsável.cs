using Solicita.TG.Domínio.Entidades.Entidade;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Requerimento
{
    public interface IResponsável: IEntidade
    {
        TipoDeResponsável TipoDeResponsavel { get; }
    }
}
