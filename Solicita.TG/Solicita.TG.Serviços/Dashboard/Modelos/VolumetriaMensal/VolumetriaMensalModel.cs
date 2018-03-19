using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Dashboard.Modelos
{
    [DataContract]
    public class VolumetriaMensalModel
    {

        [DataMember]
        public List<VolumetriaMensalPorCategoriaModel> VolumetriaPorMes { get; set; }

        public VolumetriaMensalModel()
        {
            VolumetriaPorMes = new List<VolumetriaMensalPorCategoriaModel>();
        }
    }
}
