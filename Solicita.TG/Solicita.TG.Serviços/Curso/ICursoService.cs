using Solicita.TG.Serviços.cursoService.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Solicita.TG.Serviços.cursoService
{
   
    [ServiceContract]
    public interface ICursoService
    {
        [OperationContract]
        Guid Criar(String nome, List<Guid> idDisciplinas);

        [OperationContract]
        CursoModel Get(Guid id);

        [OperationContract]
        List<CursoModel> Listar();

        [OperationContract]
        void Excluir(Guid id);

        [OperationContract]
        List<Modelos.CursoModel> ListarCursosDoAluno(Guid idAluno);

        [OperationContract]
        void Salvar(Guid id, String nome, List<Guid> idDisciplinas);
    }
}
