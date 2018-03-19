using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solicita.TG.UI.View.Models
{
    public class ChartParameters
    {
        public Guid Aluno { get; set; }

        public List<Int32> Status { get; set; }

        public Boolean AgruparPorTipo { get; set; }

        public Guid Disciplina { get; set; }

        public String DataInicial { get; set; }

        public String DataFinal { get; set; }

        public ChartParameters()
        {
            this.DataInicial = String.Empty;
            this.DataFinal = String.Empty;
            this.Aluno = Guid.Empty;
            this.Status = new List<int>();
            this.AgruparPorTipo = true;
        }

    }
}