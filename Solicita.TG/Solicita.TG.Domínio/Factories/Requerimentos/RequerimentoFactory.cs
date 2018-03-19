using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Requerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Factories.Requerimentos
{
    public static class RequerimentoFactory
    {
        public static IRequerimento CreateSolicitacaoDeDocumentos(IAluno aluno, 
            TipoDeMotivosSolicitacaoDeDocumentos tipoDoMotivo, TiposDeDocumentos tipoDeDocumento, String especificacaoDoMotivo,
            Boolean informarSemestreAtual, Boolean informarCargaHoraria, Boolean informarAulaAosSabados,
            Boolean informarPrevisãoDeConclusão, Guid disciplinaInformada, Boolean informarDisciplina,
            Boolean informarPeriodo)
        {
            return new SolicitaçãoDeDocumentos(aluno, tipoDoMotivo, tipoDeDocumento, especificacaoDoMotivo, informarSemestreAtual, informarCargaHoraria,
                informarAulaAosSabados, informarPrevisãoDeConclusão, disciplinaInformada, informarDisciplina, informarPeriodo);
        }
    }
}
