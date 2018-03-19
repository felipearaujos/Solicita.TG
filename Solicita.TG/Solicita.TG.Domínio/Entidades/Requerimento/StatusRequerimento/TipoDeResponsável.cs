using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento
{
    public enum TipoDeResponsável
    {
        [Description("Aluno")]
        Aluno = 1,

        [Description("Professor")]
        Professor = 2,

        [Description("Diretor")]
        Diretor = 3,

        [Description("Funcionário Acadêmico")]
        FuncionárioAcadêmico = 4,

        [Description("Cordenador")]
        Cordenador = 5
    }
}
