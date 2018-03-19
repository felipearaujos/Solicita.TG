using Solicita.TG.Domínio.Entidades.Pessoa;
using Solicita.TG.Domínio.Entidades.Requerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Professor
{
    public interface IProfessor: IPessoa, IResponsável
    {
        Boolean ÉCordenador { get; }

        void DefinirComoCordenador(Boolean éCordenador);
    }
}
    