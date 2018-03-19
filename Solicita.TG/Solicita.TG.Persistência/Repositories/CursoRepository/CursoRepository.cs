using Solicita.TG.Domínio.Entidades.Curso;
using Solicita.TG.Domínio.Entidades.Disciplina;
using Solicita.TG.Persistência.Repositories.Base;
using Solicita.TG.Persistência.Repositories.DisciplinaRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Persistência.Repositories.CursoRepository
{
    public class CursoRepository : BaseRepository
    {
        private Dictionary<String, Object> parametros;
        private String sql;
        private const String Tabela = "CURSO";

        public CursoRepository()
        {
            parametros = new Dictionary<String, Object>();
            sql = string.Empty;
        }

        public void Salvar(ICurso entidade)
        {
            ICurso entidadeConsultada = GetByID(entidade.Id);
            if (entidadeConsultada == null)
            {
                Inserir(entidade);
            }
            else
            {
                Atualizar(entidade);
            }
        }

        public void Excluir(ICurso entidade)
        {
            try
            {
                parametros = new Dictionary<String, Object>();
                parametros.Add("@id_curso", entidade.Id);

                sql = @"DELETE FROM GRADECURRICULAR WHERE ID_CURSO = @id_curso";

                Execute(sql, parametros);

                parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);

                sql = @"DELETE FROM "+ Tabela +" WHERE ID = @ID";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Inserir(ICurso entidade)
        {
            try
            {
                parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);
                parametros.Add("@Nome", entidade.Nome);

                sql = String.Empty;

                sql = @"INSERT INTO " + Tabela + " (ID,NOME) VALUES (@ID, @NOME)";

                Execute(sql, parametros);

                foreach (IDisciplina disciplina in entidade.GradeCurricular)
                {
                    parametros = new Dictionary<String, Object>();
                    parametros.Add("@id_curso", entidade.Id);
                    parametros.Add("@id_disciplina", disciplina.Id);

                    sql = @"INSERT INTO GRADECURRICULAR (id_curso, id_disciplina) VALUES (@id_curso, @id_disciplina)";

                    Execute(sql, parametros);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Atualizar(ICurso entidade)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", entidade.Id);
                parametros.Add("@NOME", entidade.Nome);


                sql = @"UPDATE "+ Tabela +" SET NOME = @NOME "
                    + " WHERE ID = @Id";

                Execute(sql, parametros);


                //ALERT: Era isso ou dar get e validar listas
                parametros = new Dictionary<String, Object>();
                parametros.Add("@id_curso", entidade.Id);

                sql = @"DELETE FROM GRADECURRICULAR WHERE ID_CURSO = @id_curso";

                Execute(sql, parametros);


                foreach (IDisciplina disciplina in entidade.GradeCurricular)
                {
                    parametros = new Dictionary<String, Object>();
                    parametros.Add("@id_curso", entidade.Id);
                    parametros.Add("@id_disciplina", disciplina.Id);

                    sql = @"INSERT INTO GRADECURRICULAR (id_curso, id_disciplina) VALUES (@id_curso, @id_disciplina)";

                    Execute(sql, parametros);
                }
                

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICurso GetByID(Guid id)
        {
            ICurso entidade;
            try
            {
                parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", id);

                String sql = "SELECT  id, nome FROM " + Tabela + " WHERE id = @Id";

                ExecuteQuery(sql, parametros);

                entidade = getEntidadeDoDataReader();
            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        public List<ICurso> ListAll()
        {
            List<ICurso> listaDeEntidades = new List<ICurso>();
            try
            {
                String sql = " SELECT  ID, NOME FROM " + Tabela;

                ExecuteQuery(sql);

                listaDeEntidades = GetEntidadesDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }
            return listaDeEntidades;
        }

        #region Métodos Auxilires

        public void LimparBancoDeDados()
        {
            try
            {
                Execute("SET SQL_SAFE_UPDATES=0; DELETE FROM GRADECURRICULAR;DELETE FROM DISCIPLINA; DELETE FROM CURSO;");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private ICurso getEntidadeDoDataReader()
        {
            ICurso entidade;
            if (BaseReader.Read() && BaseReader.HasRows)
            {
                entidade = GetEntidade();
            }
            else
            {
                entidade = null;
            }

            FinalizarLeitor();

            if(entidade != null)
                entidade.AdicionarGradeCurricularCompleta(GetDisciplinasDoCurso(entidade.Id));

            return entidade;
        }

        private List<ICurso> GetEntidadesDoDataReader()
        {
            List<ICurso> ListaDeCursos = new List<ICurso>();
            while (BaseReader.Read())
            {
                ICurso entidade = GetEntidade();

                ListaDeCursos.Add(entidade);
            }

            FinalizarLeitor();

            if (ListaDeCursos.Count != 0)
            {
                foreach (ICurso curso in ListaDeCursos)
                {
                    curso.AdicionarGradeCurricularCompleta(GetDisciplinasDoCurso(curso.Id));
                }
            }

            return ListaDeCursos;
        }

        private ICurso GetEntidade()
        {

            ICurso entidade = Curso.CriarCurso((Guid.Parse(BaseReader["id"].ToString())),
                                                     BaseReader["nome"].ToString());



            return entidade;
        }


        private List<IDisciplina> GetDisciplinasDoCurso(Guid idCurso)
        {
            
            List<Guid> listaDeGuids = new List<Guid>();

            try
            {
                parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", idCurso);

                sql = "SELECT ID_DISCIPLINA  FROM " + Tabela + " c " 
				    + "JOIN gradecurricular g ON(c.ID = g.ID_CURSO) " 
                     + "WHERE c.ID = @ID";

                ExecuteQuery(sql, parametros);

                while (BaseReader.Read())
                {
                    Guid id_disciplina = (Guid.Parse(BaseReader["ID_DISCIPLINA"].ToString()));
                    listaDeGuids.Add(id_disciplina);
                }

                FinalizarLeitor();

            }
            catch (Exception ex)
            {
                throw;
            }

            DisciplinaRepository.DisciplinaRepository disciplinaRepository = new DisciplinaRepository.DisciplinaRepository();

            List<IDisciplina> disciplinasDoCurso = new List<IDisciplina>();
            foreach(Guid id in listaDeGuids)
            {
                disciplinasDoCurso.Add(disciplinaRepository.GetByID(id));
            }

            return disciplinasDoCurso;
        }

        #endregion
    }
}
