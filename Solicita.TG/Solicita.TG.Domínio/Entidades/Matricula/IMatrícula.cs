using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Matricula
{
    public interface IMatrícula
    {
        String RA { get; }
        Período Período { get; }
        Turno Turno { get; }
        Guid Curso { get; }
    }
}
