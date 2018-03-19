using Solicita.TG.Serviços.DisciplinaService.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace Solicita.TG.Serviços.DisciplinaService
{
    [ServiceContract]
    public interface IDisciplinaService
    {
        [OperationContract]
        Guid Criar(String nome, Int32 cargaHoraria);

        [OperationContract]
        DisciplinaModel Get(Guid id);

        [OperationContract]
        List<DisciplinaModel> Listar();

        [OperationContract]
        void Excluir(Guid id);

        [OperationContract]
        void Salvar(Guid id,String novoNome,Int32 novaCargaHoraria);

        [OperationContract]
        List<DisciplinaModel> ListarDisciplinasPorNome(String nome);
    }
}
