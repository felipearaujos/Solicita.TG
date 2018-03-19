using Solicita.TG.Serviços.Professor.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Professor
{
    [ServiceContract]
    public interface IProfessorService
    {
        [OperationContract]
        [FaultContract(typeof(MyServiceFault))]
        Guid Criar(String Nome, String Sobrenome, Boolean Cordenador);

        [OperationContract]
        [FaultContract(typeof(MyServiceFault))]
        void Salvar(String Nome, String Sobrenome, Boolean Cordenador);

        [OperationContract]
        List<ProfessorModel> ListarTodosProfessores();

        [OperationContract]
        ProfessorModel Get(Guid Id);
    }
}
