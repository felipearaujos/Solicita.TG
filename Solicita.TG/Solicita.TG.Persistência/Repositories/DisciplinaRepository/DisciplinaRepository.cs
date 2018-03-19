using Solicita.TG.Domínio.Entidades.Disciplina;
using Solicita.TG.Persistência.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Persistência.Repositories.DisciplinaRepository
{
    public class DisciplinaRepository : BaseRepository 
    {
        public void Salvar(IDisciplina disciplina)
        {
            IDisciplina displinaConsultada = GetByID(disciplina.Id);

            if (displinaConsultada != null)
            {
                Atualizar(disciplina);
            }
            else
            {
                Inserir(disciplina);
            }
        }

        public void Excluir(IDisciplina disciplina)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", disciplina.Id);

                sql = @"DELETE FROM disciplina WHERE ID = @ID";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Inserir(IDisciplina disciplina)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", disciplina.Id);
                parametros.Add("@Nome", disciplina.Nome);
                parametros.Add("@CARGAHORARIA", disciplina.CargaHorária);

                sql = @"INSERT INTO DISCIPLINA (ID,NOME,CARGAHORARIA) VALUES (@ID,@NOME,@CARGAHORARIA)";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Atualizar(IDisciplina disciplina)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", disciplina.Id);
                parametros.Add("@NOME", disciplina.Nome);
                parametros.Add("@CARGAHORARIA", disciplina.CargaHorária);

                sql = @"UPDATE disciplina SET NOME = @NOME, "
                                       + "CARGAHORARIA = @CARGAHORARIA "
                    + " WHERE ID = @Id";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IDisciplina GetByID(Guid id)
        {
            IDisciplina entidade;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", id);

                String sql = "SELECT  id, nome, cargahoraria FROM disciplina WHERE id = @Id";

                ExecuteQuery(sql, parametros);

                entidade = getEntidadeDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        public List<IDisciplina> ListAll()
        {
            List<IDisciplina> ListaDeDisciplinas = new List<IDisciplina>();
            try
            {
                String sql = " SELECT  ID, NOME, cargahoraria FROM disciplina";

                ExecuteQuery(sql);

                ListaDeDisciplinas = GetEntidadesDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }
            return ListaDeDisciplinas;
        }

        #region Métodos Auxilires

        public void LimparBancoDeDados()
        {
            try
            {
                Execute("SET SQL_SAFE_UPDATES=0; DELETE FROM disciplina;");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IDisciplina getEntidadeDoDataReader()
        {
            IDisciplina entidade;
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

        private List<IDisciplina> GetEntidadesDoDataReader()
        {
            List<IDisciplina> ListaDeDisciplinas = new List<IDisciplina>();
            while (BaseReader.Read())
            {
                IDisciplina entidade = GetEntidade();

                ListaDeDisciplinas.Add(entidade);
            }

            FinalizarLeitor();

            return ListaDeDisciplinas;
        }

        private IDisciplina GetEntidade()
        {
            return Disciplina.CriarDisciplina(   Guid.Parse(BaseReader["id"].ToString()),
                                                 BaseReader["nome"].ToString(),
                                                 Convert.ToInt32(BaseReader["cargahoraria"].ToString())
                                       );
        }

        #endregion
    }
}
