using Solicita.TG.Domínio.EntidadeAplicacao;
using Solicita.TG.Domínio.Entidades.Requerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento;
using Solicita.TG.Persistência.Repositories.Base;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Persistência.Repositories.RequerimentoRepository
{
    public class RequerimentoRepository : BaseRepository
    {
        private static string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public List<QuantidadePorTipo> RelatorioCustomizadoRequerimento(Guid aluno, Guid responsavel, List<Int32> status, Boolean agruparPorTipo, Guid IdDisciplina, String dataInicial, String dataFinal)
        {

            Dictionary<String, Object> parametros = new Dictionary<String, Object>();

            if (aluno != Guid.Empty)
            {
                parametros.Add("@id_aluno", aluno);
            }

            if (responsavel != Guid.Empty)
            {
                parametros.Add("@idResponsavel", responsavel);
            }

            String statusFilter = String.Empty;
            foreach (var item in status)
            {
                statusFilter += ((Int32)item).ToString() + ",";
            }
            
            
            if( status.Count > 0)
            {
                parametros.Add("@tipoDeStatus", status);
            }

            if (IdDisciplina != Guid.Empty)
            {
                parametros.Add("@id_disciplina", IdDisciplina);
            }


            if (!String.IsNullOrEmpty(dataInicial))
            {
                parametros.Add("@DATAABERTURA", IdDisciplina);
            }

            if (!String.IsNullOrEmpty(dataFinal))
            {
                parametros.Add("@DATAFINAL", IdDisciplina);
            }


            String sql = String.Empty;

            String select = String.Empty;
            String GroupBy = String.Empty;
            String From = @" 
                            FROM   requerimento R 
                                    INNER JOIN solicitacaodedocumentos S 
                                            ON ( S.id_requerimento = R.id ) 
                                    INNER JOIN statusrequerimento sr 
                                            ON ( sr.id = R.id_statusatual ) ";

            if (agruparPorTipo)
            {
                select += @"SELECT S.tipodedocumento,                                    
                                   Count(S.tipodedocumento) countTipo ";

                GroupBy += @"GROUP  BY S.tipodedocumento";
            }
            else
            {
                select += @"SELECT sr.tipodestatus,                                    
                                   Count(sr.tipodestatus) countTipo ";

                GroupBy += @"GROUP  BY sr.tipodeStatus";
            }


                String Where = String.Empty;

                if (parametros.Count > 0)
                {
                    Where = "WHERE ";

                    if (aluno != Guid.Empty)
                        Where += "id_aluno =  '" + aluno.ToString()+ "' AND ";

                    if (responsavel != Guid.Empty)
                        Where += "idResponsavel = @idResponsavel AND ";

                    if (status.Count > 0)
                    {
                        statusFilter = statusFilter.Remove(statusFilter.Length - 1);
                        Where += "tipoDeStatus IN( " + statusFilter + " )  AND ";
                    }

                    if (IdDisciplina != Guid.Empty)
                        Where += "id_disciplina =  '" + IdDisciplina.ToString() + "' AND ";

                    if (!String.IsNullOrEmpty(dataInicial))
                    { 
                        DateTime data = Convert.ToDateTime(dataInicial);                        
                        Where += "R.DATAABERTURA >= '"+ new DateTime(data.Year, data.Month, 1, 00, 00, 01).ToString(DateFormat)  + "' AND ";
                    }

                    if (!String.IsNullOrEmpty(dataFinal))
                    {
                        DateTime data = Convert.ToDateTime(dataFinal);
                        Where += "R.DATAABERTURA <= '" + new DateTime(data.Year, data.Month, 1, 00, 00, 01).ToString(DateFormat) + "' AND ";
                    }

                    Where = Where.Remove(Where.Length - 4);
                }

                
            
            try
            {
                sql = select + From + Where + GroupBy;

                ExecuteQuery(sql);

                return GetCountPorTipo(agruparPorTipo);
            }
            catch (Exception e)
            {

            }

            return new List<QuantidadePorTipo>();
        }

        public Int32 TotalDeRequerimentosPorStatus(List<TipoDeStatus> status, DateTime data)
        {

            Dictionary<String, Object> parametros = new Dictionary<String, Object>();
            DateTime ultimoDia = data.AddMonths(1).AddDays(-1);

            parametros = new Dictionary<String, Object>();
            parametros.Add("@primeiroDiaMes", new DateTime(data.Year, data.Month, 1, 00, 00, 01).ToString(DateFormat));
            parametros.Add("@ultimoDiaMes", new DateTime(ultimoDia.Year, ultimoDia.Month, 1, 00, 00, 01).ToString(DateFormat));


            String statusFilter = String.Empty;
            foreach (var item in status)
            {
                statusFilter += ((Int32)item).ToString() + ",";
            }

            statusFilter = statusFilter.Remove(statusFilter.Length - 1);
            
            String sql = String.Empty;

            Int32 total = 0;

            try
            {
                sql = @"
                        SELECT Sum(Retorno.totalemaberto) countStatus 
                        FROM   (SELECT Count(S.tipodedocumento) TotalEmAberto 
                                FROM   requerimento R 
                                       INNER JOIN solicitacaodedocumentos S 
                                               ON ( S.id_requerimento = R.id ) 
                                       INNER JOIN statusrequerimento sr 
                                               ON ( sr.id = R.id_statusatual ) 
                                WHERE R.DATAABERTURA >= @primeiroDiaMes AND 
                                      R.DATAABERTURA <= @ultimoDiaMes 
                                       AND sr.tipodestatus IN( " + statusFilter + @" ) 
                                GROUP  BY S.tipodedocumento) Retorno; 
                    ";

                ExecuteQuery(sql, parametros);

                
                while (BaseReader.Read())
                {
                    total = Convert.ToInt32(BaseReader["countStatus"].ToString());                                        
                }

                

                FinalizarLeitor();
            }
            catch (Exception e)
            {

            }

            return total;
        }

        public void Salvar(IRequerimento entidade)
        {

            Inserir(entidade);
        }

        public void Excluir(IRequerimento entidade)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);

                sql = @"DELETE FROM Requerimento WHERE ID = @ID";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IRequerimento GetByID(Guid id)
        {
            return null;
        }

        public IRequerimento GetByIDAndType(Guid id, Type type)
        {
            IRequerimento entidade;
            try
            {
                entidade = GetEspecificMethod(id, type);
            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        private IRequerimento GetEspecificMethod(Guid id, Type type)
        {
            if (type.Equals(typeof(SolicitaçãoDeDocumentos)))
                return GetSolicitacaoDeDocumentos(id);

            return null;
        }

        public List<IRequerimento> ListAllByFilters(Type type, TipoRequerimento tipo, Guid alunoId)
        {
            List<IRequerimento> entidades;
            try
            {
                entidades = ListEspecificMethod(type, tipo, alunoId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return entidades;
        }

        public List<IRequerimento> ListAllByFiltersJáAtendidas(Type type, TipoRequerimento tipo, Guid alunoId)
        {
            List<IRequerimento> entidades;
            try
            {
                entidades = ListJáAtendidasEspecificMethod(type, tipo, alunoId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return entidades;
        }

        public Dictionary<TiposDeDocumentos, Int32> VerificarVolumetriaMensal(DateTime data)
        {
            Dictionary<TiposDeDocumentos, Int32> retorno = new Dictionary<TiposDeDocumentos, int>();
            Dictionary<String, Object> parametros = new Dictionary<String, Object>();
            String sql = String.Empty;

            try
            {
                DateTime ultimoDia = data.AddMonths(1).AddDays(-1);

                parametros = new Dictionary<String, Object>();
                parametros.Add("@primeiroDiaMes", new DateTime(data.Year, data.Month, 1, 00, 00, 01).ToString(DateFormat));
                parametros.Add("@ultimoDiaMes", new DateTime(ultimoDia.Year, ultimoDia.Month, 1, 00, 00, 01).ToString(DateFormat));

                sql = @"

                                SELECT 
	                                S.TIPODEDOCUMENTO TIPODEDOCUMENTO,
	                                COUNT(S.TIPODEDOCUMENTO) QUANTIDADE
                                FROM
                                REQUERIMENTO R
                                INNER JOIN SOLICITACAODEDOCUMENTOS S ON (S.ID_REQUERIMENTO = R.ID) 
                                WHERE R.DATAABERTURA >= @primeiroDiaMes AND 
                                      R.DATAABERTURA <= @ultimoDiaMes 
                                GROUP BY S.TIPODEDOCUMENTO
                    ";

                ExecuteQuery(sql, parametros);

                retorno = GetVolumetriaMensalDoDataReader();
            }
            catch (Exception e)
            {

            }

            return retorno;
        }

        public TipoRequerimento GetTipoDoRequerimento(Guid idRequerimento)
        {
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", idRequerimento);

                String sql = "SELECT  id, tipo FROM requerimento WHERE id = @ID";

                ExecuteQuery(sql, parametros);
                if (BaseReader.Read() && BaseReader.HasRows)
                {
                    var tipo = (TipoRequerimento)Convert.ToInt32(BaseReader["tipo"].ToString());

                    FinalizarLeitor();

                    return tipo;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return TipoRequerimento.Indefinido;
        }

        public void AtualizarStatus(IRequerimento entidade)
        {
            String sql = String.Empty;
            Dictionary<String, Object> parametros = new Dictionary<String, Object>();

            var transaction = this.BeginTransaction();

            try
            {

                var statusAtual = entidade.HistóricoDeStatus.Last();

                parametros.Add("@id", statusAtual.Id);
                parametros.Add("@id_requerimento", entidade.Id);
                parametros.Add("@dataDeEntrada", statusAtual.DataEntrada.ToString());
                parametros.Add("@dataDeSaida", statusAtual.DataSaida.ToString());
                parametros.Add("@tipoDeStatus", (Int32)statusAtual.StatusType);
                parametros.Add("@observacao", statusAtual.Observação.ToString());
                parametros.Add("@idResponsavel", statusAtual.IdResponsável);
                parametros.Add("@tipoResponsavel", (Int32)statusAtual.TipoDeResponsável);

                sql = @"INSERT INTO statusRequerimento (id,id_requerimento,dataDeEntrada,dataDeSaida,tipoDeStatus,observacao,idResponsavel,tipoResponsavel) 
                    VALUES (@id,@id_requerimento,@dataDeEntrada,@dataDeSaida,@tipoDeStatus,@observacao,@idResponsavel,@tipoResponsavel)";

                Execute(sql, parametros, transaction);

                parametros = new Dictionary<string, object>();
                parametros.Add("@id", entidade.Id);
                parametros.Add("@id_statusAtual", statusAtual.Id);

                sql = @"UPDATE requerimento SET id_statusAtual = @id_statusAtual WHERE id = @id";

                Execute(sql, parametros, transaction);

                var statusASerAtualizado = entidade.HistóricoDeStatus[entidade.HistóricoDeStatus.Count - 2];

                parametros = new Dictionary<string, object>();
                parametros.Add("@id_statusAtual", statusASerAtualizado.Id);
                parametros.Add("@dataSaida", statusASerAtualizado.DataSaida.ToString());

                sql = @"UPDATE statusrequerimento SET dataDeSaida = @dataSaida WHERE id = @id_statusAtual";

                Execute(sql, parametros, transaction);

                this.Commit(transaction);
            }
            catch (Exception ex)
            {
                this.Rollback(transaction);
                throw new ArgumentNullException(ex.Message);
            }

        }

        private List<IRequerimento> ListEspecificMethod(Type type, TipoRequerimento tipo, Guid alunoId)
        {
            return List(type, tipo, alunoId);
        }

        private List<IRequerimento> ListJáAtendidasEspecificMethod(Type type, TipoRequerimento tipo, Guid alunoId)
        {
            return ListByJáAtendidas(type, tipo, alunoId);
        }

        public List<IRequerimento> ListAll()
        {            
            return null;
        }

        public List<IRequerimento> ListAll(Type type, TipoRequerimento tipo)
        {
            List<IRequerimento> entidades;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@tipo", tipo);

                String sql = @"

                                

                                SELECT 
	                                R.id RequerimentoId,
                                    R.id_aluno AlunoId,
	                                R.tipo TipoRequerimento,
	                                R.id_statusatual StatusAtualId,
	                                R.dataAbertura DataAbertura,
	                                R.dataPrevistaDeTermino DataPrevistaTermino,
	                                S.tipoDeStatus
                                FROM REQUERIMENTO R
                                JOIN statusrequerimento S ON (R.id = S.id_requerimento)
                                WHERE tipo = @tipo
                ";

                
                ExecuteQuery(sql, parametros);

                entidades = ListEntidadeDoDataReader(type);

                foreach (var aEntidade in entidades)
                {

                    parametros = new Dictionary<String, Object>();
                    parametros.Add("@id_requerimento", aEntidade.Id);

                    sql = @"

                                SELECT *
                                FROM STATUSREQUERIMENTO
                                WHERE id_requerimento =  @id_requerimento ORDER BY dataDeEntrada
                    ";

                    ExecuteQuery(sql, parametros);

                    List<IStatus> statusList = GetStatusEntidadeDoDataReader();

                    aEntidade.GetType().GetProperty("HistóricoDeStatus").SetValue(aEntidade, statusList);
                }



            }
            catch (Exception ex)
            {
                throw;
            }

            return entidades;
        }

        private void Inserir(IRequerimento entidade)
        {
            try
            {
                if (entidade.Tipo.Equals(TipoRequerimento.SolicitaçãoDeDocumentos))
                    InserirSolicitacaoDeDocumentos((SolicitaçãoDeDocumentos)entidade);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Queries Solicitação de Documentos

        private void InserirSolicitacaoDeDocumentos(SolicitaçãoDeDocumentos entidade)
        {
            String sql = String.Empty;
            Dictionary<String, Object> parametros = new Dictionary<String, Object>();

            var transaction = this.BeginTransaction();

            try
            {
                foreach (var umStatus in entidade.HistóricoDeStatus)
                {
                    //Insert na status Requerimento
                    parametros.Add("@id", umStatus.Id);
                    parametros.Add("@id_requerimento", entidade.Id);
                    parametros.Add("@dataDeEntrada", umStatus.DataEntrada.ToString());
                    parametros.Add("@dataDeSaida", umStatus.DataSaida.ToString());
                    parametros.Add("@tipoDeStatus", (Int32)umStatus.StatusType);
                    parametros.Add("@observacao", umStatus.Observação.ToString());
                    parametros.Add("@idResponsavel", umStatus.IdResponsável);
                    parametros.Add("@tipoResponsavel", (Int32)umStatus.TipoDeResponsável);

                    sql = @"INSERT INTO statusRequerimento (id,id_requerimento,dataDeEntrada,dataDeSaida,tipoDeStatus,observacao,idResponsavel,tipoResponsavel) 
                    VALUES (@id,@id_requerimento,@dataDeEntrada,@dataDeSaida,@tipoDeStatus,@observacao,@idResponsavel,@tipoResponsavel)";

                    Execute(sql, parametros, transaction);
                }

                parametros = new Dictionary<String, Object>();

                //Insert na status Requerimento
                parametros.Add("@id", Guid.NewGuid());
                parametros.Add("@id_requerimento", entidade.Id);
                parametros.Add("@tipoDeMotivo", (Int32)entidade.Tipo);
                parametros.Add("@motivoEspecificado", entidade.MotivoEspecificado);
                parametros.Add("@informarSemestreAtual", Convert.ToInt32(entidade.InformarSemestreAtual));
                parametros.Add("@informarCargaHoraria", Convert.ToInt32(entidade.InformarCargaHorária));
                parametros.Add("@informarAulaAosSabados", Convert.ToInt32(entidade.InformarAulaAosSabados));
                parametros.Add("@tipoDeDocumento", Convert.ToInt32(entidade.TipoDeDocumento));
                parametros.Add("@informarDisciplina", Convert.ToInt32(entidade.InformarDisciplina));
                parametros.Add("@informarPeriodo", Convert.ToInt32(entidade.InformarPeriodo));
                parametros.Add("@informarPrevisaoDeConclusao", Convert.ToInt32(entidade.InformarPrevisãoDeConclusão));
                parametros.Add("@idDisciplina", entidade.DisciplinaInformada.ToString());

                sql = @"INSERT INTO solicitacaoDeDocumentos (id,id_requerimento,tipoDeMotivo, tipoDeDocumento,
                        motivoEspecificado,informarSemestreAtual,informarCargaHoraria,informarAulaAosSabados, informarDisciplina, informarPeriodo, informarPrevisaoDeConclusao, id_disciplina) 
                    VALUES (@id,@id_requerimento,@tipoDeMotivo, @tipoDeDocumento, @motivoEspecificado,
                        @informarSemestreAtual,@informarCargaHoraria,@informarAulaAosSabados, @informarDisciplina, @informarPeriodo, @informarPrevisaoDeConclusao, @idDisciplina)";

                Execute(sql, parametros, transaction);

                parametros = new Dictionary<String, Object>();

                

                //Insert na Requerimento
                parametros.Add("@id", entidade.Id);
                parametros.Add("@id_statusAtual", entidade.StatusAtual.Id);
                parametros.Add("@id_aluno", entidade.Aluno);
                parametros.Add("@tipo", (Int32)entidade.Tipo);
                parametros.Add("@dataAbertura", entidade.dataAbertura.ToString(DateFormat));
                parametros.Add("@dataPrevistaDeTermino", entidade.dataPrevistaTérmino.ToString(DateFormat));

                sql = @"INSERT INTO requerimento (id,id_statusAtual,id_aluno,tipo,dataAbertura,dataPrevistaDeTermino) 
                    VALUES (@id,@id_statusAtual,@id_aluno,@tipo,@dataAbertura,@dataPrevistaDeTermino)";

                Execute(sql, parametros, transaction);

                this.Commit(transaction);
            }
            catch (Exception ex)
            {
                this.Rollback(transaction);
                throw new ArgumentNullException(ex.Message);
            }
        }

        private IRequerimento GetSolicitacaoDeDocumentos(Guid id)
        {
            IRequerimento entidade;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", id);

                String sql = @"

                                SELECT 
	                                R.id RequerimentoId,
                                    R.id_aluno AlunoId,
	                                R.tipo TipoRequerimento,
	                                R.id_statusatual StatusAtualId,
	                                R.dataAbertura DataAbertura,
	                                R.dataPrevistaDeTermino DataPrevistaTermino,
	                                D.tipoDeDocumento TipoDeDocumento,
	                                D.id SolicitacaoDeDocumentosId,
	                                D.tipoDeMotivo TipoDeMotivo,
	                                D.motivoEspecificado MotivoEspecificado,
	                                D.informarSemestreAtual InformarSemestreAtual,
	                                D.informarCargaHoraria InformarCargaHoraria,
	                                D.informarPrevisaoDeConclusao InformarPrevisaoDeConclusao,
	                                D.informarDisciplina InformarDisciplina,
	                                D.id_disciplina DisciplinaInformada,
	                                D.informarPeriodo InformarPeriodo,
	                                D.informarAulaAosSabados InformarAulaAosSabados
                                FROM REQUERIMENTO R
                                JOIN SOLICITACAODEDOCUMENTOS D ON (R.id = D.id_requerimento)
                                WHERE R.Id =  @ID
                ";

                ExecuteQuery(sql, parametros);

                entidade = getEntidadeDoDataReader(typeof(SolicitaçãoDeDocumentos));

                parametros = new Dictionary<String, Object>();
                parametros.Add("@RequerimentoId", id);

                sql = @"

                                SELECT *
                                FROM STATUSREQUERIMENTO
                                WHERE id_requerimento =  @RequerimentoId ORDER BY dataDeEntrada
                ";

                ExecuteQuery(sql, parametros);

                List<IStatus> statusList = GetStatusEntidadeDoDataReader();

                entidade.GetType().GetProperty("HistóricoDeStatus").SetValue(entidade, statusList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        private List<IRequerimento> List(Type type, TipoRequerimento tipo, Guid alunoId)
        {
            List<IRequerimento> entidades;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@tipo", (int)tipo);

                String sql = @"

                                

                                SELECT 
	                                R.id RequerimentoId,
                                    R.id_aluno AlunoId,
	                                R.tipo TipoRequerimento,
	                                R.id_statusatual StatusAtualId,
	                                R.dataAbertura DataAbertura,
	                                R.dataPrevistaDeTermino DataPrevistaTermino,
	                                S.tipoDeStatus
                                FROM REQUERIMENTO R
                                JOIN statusrequerimento S ON (R.id = S.id_requerimento)
                                WHERE tipo = @tipo  AND (S.tipoDeStatus = 1 OR S.tipoDeStatus = 0 OR S.tipoDeStatus = 0  AND S.tipoDeStatus != 5)  
                ";

                if (alunoId != Guid.Empty)
                    sql += " AND R.id_aluno = " + "'" + alunoId + "'";

                ExecuteQuery(sql, parametros);

                entidades = ListEntidadeDoDataReader(type);

                foreach (var aEntidade in entidades)
                {

                    parametros = new Dictionary<String, Object>();
                    parametros.Add("@id_requerimento", aEntidade.Id);

                    sql = @"

                                SELECT *
                                FROM STATUSREQUERIMENTO
                                WHERE id_requerimento =  @id_requerimento ORDER BY dataDeEntrada
                    ";

                    ExecuteQuery(sql, parametros);

                    List<IStatus> statusList = GetStatusEntidadeDoDataReader();

                    aEntidade.GetType().GetProperty("HistóricoDeStatus").SetValue(aEntidade, statusList);
                }

                var entidadesHelper = entidades.ToList();

                foreach (var aEntidade in entidadesHelper)
                {
                    if (aEntidade.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Cancelado)))
                        entidades.Remove(aEntidade);

                    if (aEntidade.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Concluido)))
                        entidades.Remove(aEntidade);

                    if (aEntidade.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Indeferido)))
                        entidades.Remove(aEntidade);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return entidades;
        }

        private List<IRequerimento> ListByJáAtendidas(Type type, TipoRequerimento tipo, Guid alunoId)
        {
            List<IRequerimento> entidades;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@tipo", (int)tipo);

                String sql = @"

                                SELECT 
	                                R.id RequerimentoId,
                                    R.id_aluno AlunoId,
	                                R.tipo TipoRequerimento,
	                                R.id_statusatual StatusAtualId,
	                                R.dataAbertura DataAbertura,
	                                R.dataPrevistaDeTermino DataPrevistaTermino
                                FROM REQUERIMENTO R
                                JOIN statusrequerimento S ON (R.id = S.id_requerimento)
                                WHERE tipo = @tipo AND S.tipoDeStatus = 4 
                ";

                if (alunoId != Guid.Empty)
                    sql += " AND R.id_aluno = " + "'" + alunoId + "'";

                ExecuteQuery(sql, parametros);

                entidades = ListEntidadeDoDataReader(type);

                foreach (var aEntidade in entidades)
                {

                    parametros = new Dictionary<String, Object>();
                    parametros.Add("@id_requerimento", aEntidade.Id);

                    sql = @"

                                SELECT *
                                FROM STATUSREQUERIMENTO
                                WHERE id_requerimento =  @id_requerimento ORDER BY dataDeEntrada
                    ";

                    ExecuteQuery(sql, parametros);

                    List<IStatus> statusList = GetStatusEntidadeDoDataReader();

                    aEntidade.GetType().GetProperty("HistóricoDeStatus").SetValue(aEntidade, statusList);
                }
            }
            catch (Exception ex)
            {
                throw;
            }


            var entidadesHelper = entidades.ToList();

            foreach (var aEntidade in entidadesHelper)
            {
                if (!aEntidade.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Concluido)))
                    entidades.Remove(aEntidade);
            }

            return entidades;
        }

        #endregion

        private void Atualizar(IRequerimento entidade)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);
                //parametros.Add("@NOME", aluno.Nome);
                //parametros.Add("@SOBRENOME", aluno.Sobrenome);

                sql = @"UPDATE Requerimento SET NOME = @NOME, "
                                       + "SOBRENOME = @SOBRENOME "
                    + " WHERE ID = @Id";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Métodos Auxilires

        public void LimparBancoDeDados()
        {
            try
            {
                Execute("SET SQL_SAFE_UPDATES=0; DELETE FROM requerimento;");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IRequerimento getEntidadeDoDataReader(Type type)
        {
            IRequerimento entidade;
            if (BaseReader.Read() && BaseReader.HasRows)
            {
                entidade = GetEntidade(type);
            }
            else
            {
                entidade = null;
            }

            FinalizarLeitor();

            return entidade;
        }

        private List<IRequerimento> ListEntidadeDoDataReader(Type type)
        {
            List<IRequerimento> requerimentosList = new List<IRequerimento>();
            while (BaseReader.Read())
            {
                IRequerimento entidade = GetEntidadeSimples(type);

                requerimentosList.Add(entidade);
            }

            FinalizarLeitor();

            return requerimentosList;
        }

        private List<IStatus> GetStatusEntidadeDoDataReader()
        {
            List<IStatus> statusList = new List<IStatus>();
            while (BaseReader.Read())
            {
                IStatus entidade = GetStatusEntidade();

                statusList.Add(entidade);
            }

            FinalizarLeitor();

            return statusList;
        }

        private Dictionary<TiposDeDocumentos, Int32> GetVolumetriaMensalDoDataReader()
        {
            Dictionary<TiposDeDocumentos, Int32> retorno = new Dictionary<TiposDeDocumentos, Int32>();
            while (BaseReader.Read())
            {
                retorno.Add((TiposDeDocumentos)Convert.ToInt32(BaseReader["TIPODEDOCUMENTO"].ToString()), Convert.ToInt32(BaseReader["QUANTIDADE"].ToString()));
            }

            FinalizarLeitor();

            return retorno;
        }

        private IRequerimento GetEntidade(Type typeRequerimento)
        {
            if (typeRequerimento.Equals(typeof(SolicitaçãoDeDocumentos)))
            {
                return new SolicitaçãoDeDocumentos(
                                                    Guid.Parse(BaseReader["RequerimentoId"].ToString()),
                                                    Guid.Parse(BaseReader["AlunoId"].ToString()),
                                                    Guid.Parse(BaseReader["StatusAtualId"].ToString()),
                                                    (TipoDeMotivosSolicitacaoDeDocumentos)Convert.ToInt32(BaseReader["TipoDeMotivo"].ToString()),
                                                    (TiposDeDocumentos)Convert.ToInt32(BaseReader["TipoDeDocumento"].ToString()),
                                                    DateTime.Parse(BaseReader["DataAbertura"].ToString()),
                                                    DateTime.Parse(BaseReader["DataPrevistaTermino"].ToString()),
                                                    BaseReader["MotivoEspecificado"].ToString(),
                                                    Convert.ToBoolean(Convert.ToInt32((BaseReader["InformarSemestreAtual"].ToString()))),
                                                    Convert.ToBoolean(Convert.ToInt32((BaseReader["InformarCargaHoraria"].ToString()))),
                                                    Convert.ToBoolean(Convert.ToInt32((BaseReader["InformarAulaAosSabados"].ToString()))),
                                                    Convert.ToBoolean(Convert.ToInt32((BaseReader["InformarPrevisaoDeConclusao"].ToString()))),
                                                    Guid.Parse(BaseReader["DisciplinaInformada"].ToString()),
                                                    Convert.ToBoolean(Convert.ToInt32((BaseReader["InformarDisciplina"].ToString()))),
                                                    Convert.ToBoolean(Convert.ToInt32((BaseReader["InformarPeriodo"].ToString())))
                                                );
            }

            return null;
        }

        private IRequerimento GetEntidadeSimples(Type typeRequerimento)
        {
            if (typeRequerimento.Equals(typeof(SolicitaçãoDeDocumentos)))
            {
                return new SolicitaçãoDeDocumentos(
                                                    Guid.Parse(BaseReader["RequerimentoId"].ToString()),
                                                    Guid.Parse(BaseReader["AlunoId"].ToString()),
                                                    DateTime.Parse(BaseReader["DataAbertura"].ToString()),
                                                    DateTime.Parse(BaseReader["DataPrevistaTermino"].ToString())
                                                );
            }

            return null;
        }

        private IStatus GetStatusEntidade()
        {

            return new Status(
                                Guid.Parse(BaseReader["id"].ToString()),
                                BaseReader["observacao"].ToString(),
                                Guid.Parse(BaseReader["idResponsavel"].ToString()),
                                (TipoDeResponsável)Convert.ToInt32(BaseReader["tipoResponsavel"].ToString()),
                                DateTime.Parse(BaseReader["dataDeEntrada"].ToString()),
                                DateTime.Parse(BaseReader["dataDeSaida"].ToString()),
                                (TipoDeStatus)Convert.ToInt32(BaseReader["tipoDeStatus"].ToString())
                );
        }


        private List<QuantidadePorTipo> GetCountPorTipo(Boolean agruparPorTipo)
        {
            List<QuantidadePorTipo> retorno = new List<QuantidadePorTipo>();
            while (BaseReader.Read())
            {
                String Label = String.Empty;
                if (agruparPorTipo)
                {
                    TiposDeDocumentos tipoDoc = (TiposDeDocumentos)Convert.ToInt32(BaseReader["TIPODEDOCUMENTO"].ToString());
                    Label = Enumerations.GetEnumDescription(tipoDoc);
                }
                else
                {
                    TipoDeStatus tipoDoc = (TipoDeStatus)Convert.ToInt32(BaseReader["tipodestatus"].ToString());
                    Label = Enumerations.GetEnumDescription(tipoDoc);                    
                }


                Int32  quantidadePorTipo = Convert.ToInt32(BaseReader["countTipo"].ToString());
                QuantidadePorTipo rel = new QuantidadePorTipo(Label, quantidadePorTipo);

                retorno.Add(rel);
            }

            FinalizarLeitor();

            return retorno;
        }

        #endregion
    }
}
