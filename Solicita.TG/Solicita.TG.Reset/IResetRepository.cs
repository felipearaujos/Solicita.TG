using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Reset
{
    public interface IResetRepository
    {
        void ResetDatabase(String DatabaseName);
    }
}
