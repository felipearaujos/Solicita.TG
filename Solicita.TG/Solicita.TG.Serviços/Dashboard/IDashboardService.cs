using Solicita.TG.Serviços.Dashboard.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Dashboard
{
    [ServiceContract]
    public interface IDashboardService
    {
        [OperationContract]
        VolumetriaAnual CriarGraficoDeVolumetriaAnual();

        [OperationContract]
        VolumetriaMensalModel CriarGraficoDeVolumetriaMensal();

        [OperationContract]
        List<CustomDashBoardModel> GraficoQuantidadePorTipo(List<Int32> status, Guid aluno, Guid responsavel, Boolean agruparPorTipo, Guid Disciplina, String dataInicial, String dataFinal);

        [OperationContract]
        ResumoAtual ResumoAtual();
    }
}
