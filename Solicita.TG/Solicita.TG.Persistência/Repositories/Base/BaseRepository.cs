using Solicita.TG.Domínio.Entidades.Entidade;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Persistência.Repositories.Base
{
    public abstract class BaseRepository : IRepository<IEntidade>
    {

        #region Atributos 
        private DbCommand comando;
        private DbConnection BaseConn;
        private DbProviderFactory BaseProvider;
        protected DbDataReader BaseReader;
        private DbParameter BaseParam;
        #endregion

        #region Construtor

        public BaseRepository()
        {
            String nomeProvider = ConfigurationManager.AppSettings["dbProvider"];
            try
            {
                if (BaseConn == null || string.IsNullOrEmpty(BaseConn.ConnectionString))
                {
                    BaseProvider = DbProviderFactories.GetFactory(nomeProvider);
                    BaseConn = BaseProvider.CreateConnection();
                    BaseConn.ConnectionString = RetornaStringConexao();
                }
            }
            catch (NullReferenceException)
            {
                throw new Exception("String de conexão informada não existe no arquivo de configuração.");
            }
        }

        #endregion

        #region Conexão
        private string RetornaStringConexao()
        {
            String ConexaoUtilizada = ConfigurationManager.AppSettings["conexaoUtilizada"];
            return ConfigurationManager.ConnectionStrings[ConexaoUtilizada].ConnectionString;
            
        }

        private void AbrirConexaoComBancoDados()
        {
            BaseConn.Open();
        }

        private void DesconectarComBancoDados()
        {
            if (BaseReader != null && !BaseReader.IsClosed)
            {
                BaseReader.Close();
                BaseReader.Dispose();
            }

            if (comando != null)
            {
                comando.Dispose();
            }

            if (BaseConn.State == ConnectionState.Open)
            {
                BaseConn.Close();
                BaseConn.Dispose();
            }
        }
        #endregion

        public void Execute(String commandTextSQL,Dictionary<String, Object> ListaDeParametros)
        {
            CriarComando(commandTextSQL);
            CriarListaDeParametros(ListaDeParametros);
            ExecutarComando();
        }

        public void Execute(String commandTextSQL, Dictionary<String, Object> ListaDeParametros, DbTransaction transaction)
        {
            CriarComando(commandTextSQL, transaction);
            CriarListaDeParametros(ListaDeParametros);
            ExecutarComando(transaction);
        }

        public void Execute(String commandTextSQL)
        {
            CriarComando(commandTextSQL);
            ExecutarComando();
        }

        public void ExecuteQuery(String commandTextSQL, Dictionary<String, Object> ListaDeParametros)
        {
            CriarComando(commandTextSQL);

            LimparParametros();

            CriarListaDeParametros(ListaDeParametros);

            CriarLeitorExecutarComando();

        }

        public void ExecuteQuery(String commandTextSQL)
        {
            CriarComando(commandTextSQL);

            LimparParametros();

            CriarLeitorExecutarComando();
        }

        public DbTransaction BeginTransaction()
        {
            AbrirConexaoComBancoDados();

            return BaseConn.BeginTransaction();
        }

        public void Commit(DbTransaction transaction)
        {
            transaction.Commit();

            DesconectarComBancoDados();
        }

        public void Rollback(DbTransaction transaction)
        {
            transaction.Rollback();

            DesconectarComBancoDados();
        }

        #region Comando

        private void CriarComando(String commandTextSQL)
        {
            System.Data.CommandType commandType = CommandType.Text;

            BaseConn.ConnectionString = RetornaStringConexao();

            comando = BaseConn.CreateCommand();
            comando.CommandText = commandTextSQL;
            comando.CommandType = commandType;

        }


        private void CriarComando(String commandTextSQL, DbTransaction transaction)
        {
            System.Data.CommandType commandType = CommandType.Text;

            comando = BaseConn.CreateCommand();
            comando.CommandText = commandTextSQL;
            comando.CommandType = commandType;

        }

        private void ExecutarComando()
        {
            AbrirConexaoComBancoDados();

            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                DesconectarComBancoDados();
            }
        }

        private void ExecutarComando(DbTransaction transaction)
        {
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion

        #region Parâmetro

        private void CriarParametro(String nomeParametro, Object valorParametro)
        {
            BaseParam = comando.CreateParameter();
            BaseParam.ParameterName = nomeParametro;
            BaseParam.Value = valorParametro;

            comando.Parameters.Add(BaseParam);

        }

        private void CriarListaDeParametros(Dictionary<String, Object> ListaDeParametros)
        {
            foreach (KeyValuePair<string, Object> parametro in ListaDeParametros)
            {
                BaseParam = comando.CreateParameter();
                BaseParam.ParameterName = parametro.Key;
                BaseParam.Value = parametro.Value;

                comando.Parameters.Add(BaseParam);
            }
        }

        private void LimparParametros()
        {
            comando.Parameters.Clear();
        }

        #endregion

        #region Leitor

        private void CriarLeitorExecutarComando()
        {
            AbrirConexaoComBancoDados();

            try
            {
                BaseReader = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void FinalizarLeitor()
        {
            DesconectarComBancoDados();
        }

        #endregion


        void IRepository<IEntidade>.Salvar(IEntidade entidade)
        {
            throw new NotImplementedException();
        }

        void IRepository<IEntidade>.Excluir(IEntidade entidade)
        {
            throw new NotImplementedException();
        }

        IEntidade IRepository<IEntidade>.GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        List<IEntidade> IRepository<IEntidade>.ListAll()
        {
            throw new NotImplementedException();
        }

        void IRepository<IEntidade>.LimparBancoDeDados()
        {
            throw new NotImplementedException();
        }
    }
}