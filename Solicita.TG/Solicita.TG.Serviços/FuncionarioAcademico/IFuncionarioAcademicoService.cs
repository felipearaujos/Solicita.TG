using Solicita.TG.Serviços.FuncionarioAcademico.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.FuncionarioAcademico
{
    [ServiceContract]
    public interface IFuncionarioAcademicoService
    {
        [OperationContract]
        [FaultContract(typeof(MyServiceFault))]
        Guid CriarFuncionario(String Nome, String Sobrenome, Int32 TipoResponsavel);

        [OperationContract]
        List<FuncionarioModel> ListarTodosFuncionarios();

        [OperationContract]
        FuncionarioModel Get(Guid Id);

        [OperationContract]
        List<TipoDeResponsavelModel> ListarTipoDeResponsável();

        [OperationContract]
        List<FuncionarioModel> ListarPorNome(String nome);
    }
}
