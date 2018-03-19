using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Solicita.TG.Serviços.ProcessadorDeExcel
{
   
    [ServiceContract]
    public interface IProcessadorDeArquivoService
    {
        [OperationContract]
        void ProcessarExcel(String Archive, String Content, String Entidade);

        [OperationContract]
        String DownloadExcelModelo();

    }
}
