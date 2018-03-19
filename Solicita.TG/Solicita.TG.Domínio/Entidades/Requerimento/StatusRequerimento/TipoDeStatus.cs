using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento
{
    public enum TipoDeStatus
    {
        [Description("Em Análise")]
        EmAnálise = 1,

        [Description("Aguardando Informações")]
        AguardandoInformações = 2,

        [Description("Indeferido")]
        Indeferido = 3,

        [Description("Concluido")]
        Concluido = 4,

        [Description("Cancelado")]
        Cancelado = 5,

        [Description("Novo")]
        Novo = 0
    } 
}
