using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Dashboard.Modelos
{
    [DataContract]
    public class VolumetriaAnual
    {
        [DataMember]
        public List<String> MesesNoPeriodo { get; set; }

        [DataMember]
        public List<VolumetriaAnualChartData> Chart { get; set; }

        public VolumetriaAnual()
        {
            this.MesesNoPeriodo = new List<string>();
            this.Chart = new List<VolumetriaAnualChartData>();

        }
    }

    [DataContract]
    public class VolumetriaAnualChartData
    {
        [DataMember]
        public String Tipo { get; set; }

        [DataMember]
        public List<Int32> value { get; set; }

        [DataMember]
        public String LabelDate { get; set; }

        public VolumetriaAnualChartData()
        {
            this.Tipo = String.Empty;
            this.LabelDate = String.Empty;
            this.value = new List<int>();

        }
    }
}
