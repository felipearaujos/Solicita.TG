using Solicita.TG.Serviços.Aluno.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Aluno
{
    [ServiceContract]
    public interface IAlunoService
    {
        [OperationContract]
        [FaultContract(typeof(MyServiceFault))]
        void Criar(String Nome, String Sobrenome, String RA, String RG, Int32 Periodo, Int32 Turno, Guid Curso);

        [OperationContract]
        List<AlunoModel> Listar(String RA);

        [OperationContract]
        void Salvar(Guid Id, String Nome, String Sobrenome, String RA, String RG, Int32 Periodo, Int32 Turno, Guid Curso);

        [OperationContract]
        Modelos.AlunoModel Get(Guid Id);

        [OperationContract]
        Modelos.AlunoModel GetByRA(String RA);
    }
}
