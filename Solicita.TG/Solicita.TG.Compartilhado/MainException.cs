using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Compartilhado
{
    public class MainException: Exception
    {
        public String Error { get; private set; }
        
        public MainException(String error)
        {
            this.Error = error; 
        }
    }
}
