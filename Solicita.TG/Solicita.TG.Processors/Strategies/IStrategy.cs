using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Processors.Strategies
{
    public interface IStrategy
    {
        void Processar(String connection);
    }
}
