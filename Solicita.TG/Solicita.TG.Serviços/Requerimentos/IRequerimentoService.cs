using Solicita.TG.Serviços.Requerimentos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Solicita.TG.Serviços.Requerimentos
{
    
    [ServiceContract]
    public interface IRequerimentoService
    {
        [OperationContract]
        void Concluir(Guid id, Guid idResponsavel, Int32 tipoResponsavel);

        [OperationContract]
        List<MotivoModel> ListarMotivos();

        [OperationContract]
        void Indeferir(Guid id, String motivo, Guid idResponsavel, Int32 tipoResponsavel);

        [OperationContract]
        void Cancelar(Guid id, String motivo, Guid idResponsavel, Int32 tipoResponsavel);

        [OperationContract]
        void TransferirResponsabilidade(Guid id, Guid idResponsavel, Int32 tipoResponsavel, String observação);

        [OperationContract]
        Guid CriarSolicitacoesDeDocumentos(Guid idAluno, Int32 tipoDoMotivo, Int32 tipoDeDocumento, String especificacaoDoMotivo,
            Boolean informarSemestreAtual, Boolean informarCargaHoraria, Boolean informarAulaAosSabados, Boolean informarPrevisaoDeConclusao,
            Boolean informarDisciplina, Boolean informarPeriodo, Guid disciplinaInformada);

        [OperationContract]
        List<Requerimentos.Modelos.TiposRequerimentoModel> ListarTipos();

        [OperationContract]
        SolicitacaoDeDocumentosModel GetSolicitacaoDeDocumentos(Guid idRequerimento, String type);

        [OperationContract]
        List<RequerimentoSimplesModel> List(String RA, String nome, Guid curso, Int32 periodo, String type);

        [OperationContract]
        List<RequerimentoSimplesModel> ListJáAtendidas(String RA, String nome, Guid curso, Int32 periodo, String type);

        [OperationContract]
        List<SolicitacaoDeDocumentosModel> ListarSolicitacoesDeDocumentos();

        [OperationContract]
        List<RequerimentoSimplesModel> ListarDeclaracoesDeProva();

        [OperationContract]
        List<RequerimentoSimplesModel> ListarSolicitacoesDePasse();

        [OperationContract]
        List<RequerimentoSimplesModel> ListarReaproveitamentos();

        [OperationContract]
        List<RequerimentoSimplesModel> ListarTrancamentosDeCurso();

        [OperationContract]
        List<RequerimentoSimplesModel> ListarTrancamentosDeMatricula();

        [OperationContract]
        List<TipoDeStatusModel> ListarTipoDeStatus();
    }
}
