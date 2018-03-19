using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Matricula;
using Solicita.TG.Persistência.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Persistência.Repositories.RepositórioAluno
{
    public class AlunoRepository : BaseRepository
    {

        public void Salvar(IAluno aluno)
        {
            IAluno alunoConsultado = GetByID(aluno.Id);

            if (alunoConsultado != null)
            {
                Atualizar(aluno);
            }
            else
            {   
                Inserir(aluno);
            }
            
        }

        public void Excluir(IAluno aluno)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", aluno.Id);

                sql = @"DELETE FROM ALUNO WHERE ID = @ID";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Inserir(IAluno aluno)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", aluno.Id);
                parametros.Add("@Nome", aluno.Nome);
                parametros.Add("@Sobrenome", aluno.Sobrenome);
                parametros.Add("@RG", aluno.RG);
                parametros.Add("@RA", aluno.Matrícula.RA);
                parametros.Add("@PERIODO", aluno.Matrícula.Período);
                parametros.Add("@TURNO", aluno.Matrícula.Turno);
                parametros.Add("@CURSO", aluno.Matrícula.Curso);
                parametros.Add("@EMAIL", aluno.Email);

                sql = @"INSERT INTO ALUNO (ID,NOME,SOBRENOME,RG,RA,PERIODO ,TURNO, CURSO, EMAIL) VALUES (@ID,@NOME,@SOBRENOME,@RG,@RA,@PERIODO,@TURNO, @CURSO, @EMAIL)";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Atualizar(IAluno aluno)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", aluno.Id);
                parametros.Add("@NOME", aluno.Nome);
                parametros.Add("@SOBRENOME", aluno.Sobrenome);

                sql = @"UPDATE ALUNO SET NOME = @NOME, "
                                       + "SOBRENOME = @SOBRENOME "
                    + " WHERE ID = @Id";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IAluno GetByID(Guid idAluno)
        {
            IAluno entidade;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", idAluno);

                String sql = "SELECT  id, nome, sobrenome, ra, rg, periodo,turno, curso, email FROM aluno WHERE id = @Id";

                ExecuteQuery(sql, parametros);

                entidade = getEntidadeDoDataReader();
                
            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        public IAluno GetByRA(String RA)
        {
            IAluno entidade;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@RA", RA);

                String sql = "SELECT  id, nome, sobrenome, ra, rg, periodo,turno, curso, email FROM aluno WHERE RA = @RA";

                ExecuteQuery(sql, parametros);

                entidade = getEntidadeDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        public List<IAluno> ListAll()
        {
            List<IAluno> ListaDeAlunos = new List<IAluno>();
            try
            {
                String sql = " SELECT  ID, NOME,RA,RG, SOBRENOME,PERIODO, TURNO, CURSO, EMAIL FROM ALUNO";

                ExecuteQuery(sql);

                ListaDeAlunos = GetEntidadesDoDataReader();
                
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListaDeAlunos;
        }

        #region Métodos Auxilires

        public void LimparBancoDeDados()
        {
            try
            {
                Execute("SET SQL_SAFE_UPDATES=0; DELETE FROM ALUNO;");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IAluno getEntidadeDoDataReader()
        {
            IAluno entidade;
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

        private List<IAluno> GetEntidadesDoDataReader()
        {
            List<IAluno> ListaDeAlunos = new List<IAluno>();
            while (BaseReader.Read())
            {
                IAluno entidade = GetEntidade();

                ListaDeAlunos.Add(entidade);
            }

            FinalizarLeitor();
            
            return ListaDeAlunos;
        }

        private IAluno GetEntidade()
        {
            IAluno aluno = Aluno.CriarAluno(             Guid.Parse(BaseReader["id"].ToString()),
                                                 BaseReader["nome"].ToString(),
                                                 BaseReader["sobrenome"].ToString(),
                                                 BaseReader["ra"].ToString(),
                                                 BaseReader["RG"].ToString(),
                                                 (Período)Convert.ToInt32(BaseReader["Periodo"].ToString()),
                                                 (Turno)Convert.ToInt32(BaseReader["Turno"].ToString()),
                                                 Guid.Parse(BaseReader["curso"].ToString()));

            aluno.DefinirEmail(BaseReader["email"].ToString());

            return aluno;
        }

        #endregion

    }
}
