using Solicita.TG.Domínio.Entidades.Pessoa;
using Solicita.TG.Domínio.Entidades.Requerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.FuncionarioAcademico
{
    public interface IFuncionarioAcademico: IPessoa, IResponsável
    {
        String Nome { get; }

        String Sobrenome { get; }
    }
}
