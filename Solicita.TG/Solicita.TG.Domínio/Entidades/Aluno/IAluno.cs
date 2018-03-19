using Solicita.TG.Domínio.Entidades.Matricula;
using Solicita.TG.Domínio.Entidades.Pessoa;
using Solicita.TG.Domínio.Entidades.Requerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Aluno
{
    public interface IAluno: IPessoa, IResponsável
    {
        IMatrícula Matrícula { get; }
        String RG { get; }
        String Email { get; }

        void DefinirMatrícula(String RA, Período Periodo, Turno Turno, Guid curso);
        void AlterarPeríodo(Período Periodo);
        void AlterarTurno(Turno Turno);
        void DefinirEmail(String email);
    }
}
