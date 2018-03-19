using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento
{
    public enum TipoRequerimento
    {
        [Description("Solicitação De Documentos")]
        SolicitaçãoDeDocumentos = 1,

        [Description("DeclaraçãoDeProva")]
        DeclaraçãoDeProva = 2,

        [Description("SolicitaçãoREaproveitamento")]
        SolicitaçãoREaproveitamento = 3,

        [Description("SolicitaçãoDePasseEscolar")]
        SolicitaçãoDePasseEscolar = 4,

        [Description("TrancamentoDeMateria")]
        TrancamentoDeMateria = 5,

        [Description("TrancamentoDeCurso")]
        TrancamentoDeCurso = 6,

        [Description("Indefinido")]
        Indefinido = 7
    }
}
