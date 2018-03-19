using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Dashboard.Modelos
{
    [DataContract]
    public class CustomDashBoardModel
    {
        [DataMember]
        public String Tipo { get; set; }

        [DataMember]
        public Int32 CountPorTipo { get; set; }


        public CustomDashBoardModel()
        { 
        
        }
    }
}
