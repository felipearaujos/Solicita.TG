using Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.EntidadeAplicacao
{
    public class QuantidadePorTipo
    {
        public String Tipo { get; set; }
        public Int32 CountPorTipo { get; set; }
       

        public QuantidadePorTipo(String Tipo, Int32 CountPorTipo)
        {
            this.Tipo = Tipo;
            this.CountPorTipo = CountPorTipo;        
        }
    }
}
