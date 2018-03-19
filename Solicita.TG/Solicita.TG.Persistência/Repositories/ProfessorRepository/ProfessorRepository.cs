using Solicita.TG.Domínio.Entidades.Professor;
using Solicita.TG.Persistência.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Persistência.Repositories.ProfessorRepository
{
    public class ProfessorRepository: BaseRepository 
    {
        private Dictionary<String, Object> parametros;
        private String sql;
        private const String Tabela = "PROFESSOR";

        public ProfessorRepository()
        {
            parametros = new Dictionary<String, Object>();
            sql = string.Empty;
        }

        public void Salvar(IProfessor entidade)
        {
            IProfessor entidadeConsultada = GetByID(entidade.Id);
            if (entidadeConsultada == null)
            {
                Inserir(entidade);
            }
            else
            {
                Atualizar(entidade);
            }
        }

        public void Excluir(IProfessor entidade)
        {
            try
            {
                parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);

                sql = @"DELETE FROM PROFESSOR WHERE ID = @ID";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Inserir(IProfessor entidade)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);
                parametros.Add("@Nome", entidade.Nome);
                parametros.Add("@Sobrenome", entidade.Sobrenome);
                parametros.Add("@cordenador", entidade.ÉCordenador);

                sql = @"INSERT INTO PROFESSOR (ID,NOME,SOBRENOME,CORDENADOR) VALUES (@ID,@NOME,@SOBRENOME,@CORDENADOR)";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Atualizar(IProfessor entidade)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);
                parametros.Add("@NOME", entidade.Nome);
                parametros.Add("@SOBRENOME", entidade.Sobrenome);
                parametros.Add("@CORDENADOR", entidade.ÉCordenador);

                sql = @"UPDATE PROFESSOR SET NOME = @NOME, "
                                       + "SOBRENOME = @SOBRENOME,"
                                       + "CORDENADOR = @CORDENADOR "
                    + " WHERE ID = @Id";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IProfessor GetByID(Guid idProfessor)
        {
            IProfessor entidade;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", idProfessor);

                String sql = "SELECT  id, nome, sobrenome, cordenador FROM professor WHERE id = @Id";

                ExecuteQuery(sql, parametros);

                entidade = getEntidadeDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        public List<IProfessor> ListAll()
        {
            List<IProfessor> ListaDeProfessores = new List<IProfessor>();
            try
            {
                String sql = " SELECT  ID, NOME,SOBRENOME, CORDENADOR FROM PROFESSOR";

                ExecuteQuery(sql);

                ListaDeProfessores = GetEntidadesDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }
            return ListaDeProfessores;
        }

        #region Métodos Auxilires

        public void LimparBancoDeDados()
        {
            try
            {
                Execute("SET SQL_SAFE_UPDATES=0; DELETE FROM PROFESSOR;");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IProfessor getEntidadeDoDataReader()
        {
            IProfessor entidade;
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

        private List<IProfessor> GetEntidadesDoDataReader()
        {
            List<IProfessor> ListaDeProfessores = new List<IProfessor>();
            while (BaseReader.Read())
            {
                IProfessor entidade = GetEntidade();

                ListaDeProfessores.Add(entidade);
            }

            FinalizarLeitor();

            return ListaDeProfessores;
        }

        private IProfessor GetEntidade()
        {
            return Professor.CriarProfessor(Guid.Parse(BaseReader["id"].ToString()),
                                                 BaseReader["nome"].ToString(),
                                                 BaseReader["sobrenome"].ToString(),
                                                 Convert.ToBoolean(BaseReader["cordenador"]));
        }

        #endregion
    }
}
