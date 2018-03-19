using Solicita.TG.Domínio.Entidades.Disciplina;
using Solicita.TG.Domínio.Entidades.Requerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento;
using Solicita.TG.Persistência.Repositories.DisciplinaRepository;
using Solicita.TG.Serviços.Requerimentos.Modelos;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Mapper
{
    public class RequerimentoModelMapper: IRequerimentoModelMapper
    {
        private static Dictionary<String, Type> typesDictionary = new Dictionary<string, Type>()
        {
            { "SolicitacaoDeDocumentos", typeof(SolicitaçãoDeDocumentos) }
        };

        private static Dictionary<String, TipoRequerimento> typesEnumDictionary = new Dictionary<string, TipoRequerimento>()
        {
            { "SolicitacaoDeDocumentos", TipoRequerimento.SolicitaçãoDeDocumentos }
        };


        private static Dictionary<TipoRequerimento, Type> typesEnumToTypeDictionary = new Dictionary<TipoRequerimento, Type>()
        {
            {TipoRequerimento.SolicitaçãoDeDocumentos, typeof(SolicitaçãoDeDocumentos) }
        };

        public RequerimentoModel GetEspecificAttributes(IRequerimento requerimento)
        {
            if(requerimento.Tipo.Equals(TipoRequerimento.SolicitaçãoDeDocumentos))
            {
                SolicitacaoDeDocumentosModel solicitacaoDeDocumentosModel = new SolicitacaoDeDocumentosModel();

                SolicitaçãoDeDocumentos requerimentoParseado = requerimento as SolicitaçãoDeDocumentos;

                solicitacaoDeDocumentosModel.TipoDeMotivo = Enumerations.GetEnumDescription(requerimentoParseado.TipoDeMotivo);
                solicitacaoDeDocumentosModel.MotivoEspecificado = requerimentoParseado.MotivoEspecificado;
                solicitacaoDeDocumentosModel.InformarSemestreAtual = requerimentoParseado.InformarSemestreAtual == true ? "Sim" : "Não"; 
                solicitacaoDeDocumentosModel.InformarCargaHorária = requerimentoParseado.InformarCargaHorária == true ? "Sim" : "Não";
                solicitacaoDeDocumentosModel.InformarAulaAosSabados = requerimentoParseado.InformarAulaAosSabados == true ? "Sim" : "Não";
                solicitacaoDeDocumentosModel.InformarDisciplina = requerimentoParseado.InformarDisciplina == true ? "Sim" : "Não";
                solicitacaoDeDocumentosModel.InformarPeriodo = requerimentoParseado.InformarPeriodo == true ? "Sim" : "Não";
                solicitacaoDeDocumentosModel.InformarPrevisãoDeConclusão = requerimentoParseado.InformarPrevisãoDeConclusão == true ? "Sim" : "Não";

                if (requerimentoParseado.DisciplinaInformada == Guid.Empty)
                    solicitacaoDeDocumentosModel.DisciplinaInformada = "Não foi informado uma disciplina específica.";
                else
                {
                    DisciplinaRepository disciplinaRepository = new DisciplinaRepository();
                    IDisciplina disciplina = disciplinaRepository.GetByID(requerimentoParseado.DisciplinaInformada);
                    solicitacaoDeDocumentosModel.DisciplinaInformada = disciplina.Nome;                    
                }

                return solicitacaoDeDocumentosModel;
            }

            return null;
        }

        public Type ReturnEspecificType(String type)
        {
            return typesDictionary[type];
        }

        public TipoRequerimento ReturnEspecificTypeEnum(String type)
        {
            return typesEnumDictionary[type];
        }

        public Type ReturnEspecificEnumToType(TipoRequerimento type)
        {
            return typesEnumToTypeDictionary[type];
        }
    }
}
