using Solicita.TG.Domínio.Entidades.FuncionarioAcademico;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using Solicita.TG.Persistência.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Persistência.Repositories.FuncionarioAcademicoRepository
{
    public class FuncionarioAcademicoRepository : BaseRepository
    {
        private Dictionary<String, Object> parametros;
        private String sql;
        private const String Tabela = "FUNCIONARIOACADEMICO";

        public FuncionarioAcademicoRepository()
        {
            parametros = new Dictionary<String, Object>();
            sql = string.Empty;
        }

        public void Salvar(IFuncionarioAcademico entidade)
        {
            IFuncionarioAcademico entidadeConsultada = GetByID(entidade.Id);
            if (entidadeConsultada == null)
            {
                Inserir(entidade);
            }
            else
            {
                Atualizar(entidade);
            }
        }

        public void Excluir(IFuncionarioAcademico entidade)
        {
            try
            {
                parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);

                sql = @"DELETE FROM FUNCIONARIOACADEMICO WHERE ID = @ID";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Inserir(IFuncionarioAcademico entidade)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);
                parametros.Add("@Nome", entidade.Nome);
                parametros.Add("@Sobrenome", entidade.Sobrenome);
                parametros.Add("@TipoDeResponsavel", (Int32)entidade.TipoDeResponsavel);

                sql = @"INSERT INTO FUNCIONARIOACADEMICO (ID,NOME,SOBRENOME,TIPODERESPONSAVEL) VALUES (@ID,@NOME,@SOBRENOME,@TIPODERESPONSAVEL)";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Atualizar(IFuncionarioAcademico entidade)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);
                parametros.Add("@NOME", entidade.Nome);
                parametros.Add("@SOBRENOME", entidade.Sobrenome);

                sql = @"UPDATE FUNCIONARIOACADEMICO SET NOME = @NOME, "
                                       + "SOBRENOME = @SOBRENOME "
                    + " WHERE ID = @Id";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IFuncionarioAcademico GetByID(Guid id)
        {
            IFuncionarioAcademico entidade;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", id);

                String sql = "SELECT  id, nome, sobrenome, tipoderesponsavel FROM FUNCIONARIOACADEMICO WHERE id = @Id";

                ExecuteQuery(sql, parametros);

                entidade = getEntidadeDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        public List<IFuncionarioAcademico> ListAll()
        {
            List<IFuncionarioAcademico> ListaDeEntidades = new List<IFuncionarioAcademico>();
            try
            {
                String sql = " SELECT  ID, NOME,SOBRENOME, tipoderesponsavel FROM FUNCIONARIOACADEMICO";

                ExecuteQuery(sql);

                ListaDeEntidades = GetEntidadesDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }
            return ListaDeEntidades;
        }

        #region Métodos Auxilires

        public void LimparBancoDeDados()
        {
            try
            {
                Execute("SET SQL_SAFE_UPDATES=0; DELETE FROM FUNCIONARIOACADEMICO;");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IFuncionarioAcademico getEntidadeDoDataReader()
        {
            IFuncionarioAcademico entidade;
            if (BaseReader.Read() && BaseReader.HasRows)
            {
                entidade = GetEntidade();
            }
            else
            {
                entidade = null;
            }

            FinalizarLeitor();

            return entidade;
        }

        private List<IFuncionarioAcademico> GetEntidadesDoDataReader()
        {
            List<IFuncionarioAcademico> entidades = new List<IFuncionarioAcademico>();
            while (BaseReader.Read())
            {
                IFuncionarioAcademico entidade = GetEntidade();

                entidades.Add(entidade);
            }

            FinalizarLeitor();

            return entidades;
        }

        private IFuncionarioAcademico GetEntidade()
        {
            return FuncionarioAcademico.CriarFuncionarioAcademico(Guid.Parse(BaseReader["id"].ToString()),
                                                 BaseReader["nome"].ToString(),
                                                 BaseReader["sobrenome"].ToString(),
                                                 (TipoDeResponsável)Convert.ToInt32(BaseReader["tipoderesponsavel"].ToString()));
        }

        #endregion
    }
}
