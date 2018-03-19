using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Motivo
{
    public enum Motivo
    {
        [Description("Não estou dando conta de tantas Matérias")]
        Sobrecarga = 1,

        [Description("Trabalho no horário do curso/matéria")]
        Trabalho = 2,
        
        [Description("Outros/Especificar")]
        Outros = 99

    }
}
