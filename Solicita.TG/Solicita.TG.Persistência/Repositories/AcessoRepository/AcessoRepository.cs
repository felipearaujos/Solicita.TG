using Solicita.TG.Domínio.Entidades.Acesso;
using Solicita.TG.Persistência.Repositories.Base;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Persistência.Repositories.AcessoRepository
{
    public class AcessoRepository : BaseRepository
    {
        Criptografia Criptografia = new Criptografia();

        public void Salvar(IAcesso acesso)
        {
            IAcesso acessoConsultado = GetByID(acesso.Id);

            if (acessoConsultado != null)
            {
                Atualizar(acesso);
            }
            else
            {
                Inserir(acesso);
            }

        }

        public void Excluir(IAcesso acesso)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", acesso.Id);

                sql = @"DELETE FROM acesso WHERE ID = @ID";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Inserir(IAcesso acesso)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", acesso.Id);
                parametros.Add("@USUARIO", acesso.Usuário);
                parametros.Add("@IDENTIDADEASSOCIADA", acesso.IdEntidadeAssociada);
                parametros.Add("@SENHA", acesso.Senha);
                parametros.Add("@TIPODEUSUARIO", (Int32)acesso.TipoDeUsuário);

                sql = @"INSERT INTO acesso (ID,USUARIO,SENHA,TIPODEUSUARIO,IDENTIDADEASSOCIADA) VALUES (@ID,@USUARIO,@SENHA,@TIPODEUSUARIO,@IDENTIDADEASSOCIADA)";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Atualizar(IAcesso acesso)
        {
            try
            {
                String sql = String.Empty;
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", acesso.Id);
                parametros.Add("@USUARIO", acesso.Usuário);
                parametros.Add("@SENHA", acesso.Senha);
                parametros.Add("@TIPODEUSUARIO", (Int32)acesso.TipoDeUsuário);

                sql = @"UPDATE acesso SET USUARIO = @USUARIO, "
                                       + "SENHA = @SENHA, "
                                       + "TIPODEUSUARIO = @TIPODEUSUARIO "
                    + " WHERE ID = @ID";

                Execute(sql, parametros);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IAcesso GetByID(Guid idacesso)
        {
            IAcesso entidade;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@ID", idacesso);

                String sql = "SELECT  ID, USUARIO, SENHA, TIPODEUSUARIO, IDENTIDADEASSOCIADA FROM acesso WHERE ID = @ID";

                ExecuteQuery(sql, parametros);

                entidade = getEntidadeDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        public IAcesso GetByUsuarioESenha(String Usuario, String  Senha)
        {
            IAcesso entidade;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@USUARIO", Usuario);
                parametros.Add("@SENHA", Criptografia.Encrypt(Senha));

                String sql = "SELECT  ID, USUARIO, SENHA, TIPODEUSUARIO, IDENTIDADEASSOCIADA FROM acesso WHERE USUARIO = @USUARIO AND SENHA = @SENHA";

                ExecuteQuery(sql, parametros);

                entidade = getEntidadeDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        public IAcesso GetByUsuario(String Usuario)
        {
            IAcesso entidade;
            try
            {
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                parametros.Add("@USUARIO", Usuario);

                String sql = "SELECT  ID, USUARIO, SENHA, TIPODEUSUARIO, IDENTIDADEASSOCIADA FROM acesso WHERE USUARIO = @USUARIO";

                ExecuteQuery(sql, parametros);

                entidade = getEntidadeDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }

            return entidade;
        }

        public List<IAcesso> ListAll()
        {
            List<IAcesso> ListaDeacessos = new List<IAcesso>();
            try
            {
                String sql = " SELECT  ID, USUARIO, SENHA, TIPODEUSUARIO, IDENTIDADEASSOCIADA FROM acesso";

                ExecuteQuery(sql);

                ListaDeacessos = GetEntidadesDoDataReader();

            }
            catch (Exception ex)
            {
                throw;
            }
            return ListaDeacessos;
        }

        #region Métodos Auxilires

        public void LimparBancoDeDados()
        {
            try
            {
                Execute("SET SQL_SAFE_UPDATES=0; DELETE FROM acesso;");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IAcesso getEntidadeDoDataReader()
        {
            IAcesso entidade;
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

        private List<IAcesso> GetEntidadesDoDataReader()
        {
            List<IAcesso> ListaDeacessos = new List<IAcesso>();
            while (BaseReader.Read())
            {
                IAcesso entidade = GetEntidade();

                ListaDeacessos.Add(entidade);
            }

            FinalizarLeitor();

            return ListaDeacessos;
        }

        private IAcesso GetEntidade()
        {
            return  Acesso.CriarAcesso(Guid.Parse(BaseReader["ID"].ToString()),
                                                 Guid.Parse(BaseReader["IDENTIDADEASSOCIADA"].ToString()),
                                                 BaseReader["USUARIO"].ToString(),
                                                 BaseReader["SENHA"].ToString(),
                                                 (TipoDeAcesso)Convert.ToInt32(BaseReader["TIPODEUSUARIO"].ToString()));
        }

        #endregion
    }
}
