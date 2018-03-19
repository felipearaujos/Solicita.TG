using Solicita.TG.Domínio.Entidades.Acesso;
using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Persistência.Repositories.AcessoRepository;
using Solicita.TG.Persistência.Repositories.RepositórioAluno;
using Solicita.TG.Serviços.Acesso.Modelos;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Solicita.TG.Serviços.Acesso
{
    public class AcessoService: IAcessoService
    {

        public Guid CriarAcesso(Guid IdEntidadeAssociada, String Usuario, String Senha, Int32 TipoDeUsuario)
        {
            Guid idAcesso = Guid.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    IAcesso umAcesso = Solicita.TG.Domínio.Entidades.Acesso.Acesso.CriarAcesso(IdEntidadeAssociada, Usuario, Senha, (TipoDeAcesso)TipoDeUsuario);

                    if (umAcesso.Erros.Count > 0)
                    {
                        String result = String.Join(", ", umAcesso.Erros.ToArray());
                        FaultException fault = new FaultException(result);

                        throw fault;
                    }

                    AcessoRepository acessoRepository = new AcessoRepository();

                    acessoRepository.Salvar(umAcesso);

                    idAcesso = umAcesso.Id;

                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {

            }
            catch (ApplicationException ex)
            {
                // aqui serão tratadas as exceptions de domínio.
            }

            return idAcesso;
        }

        public void SalvarAcesso(Guid Id, Guid IdEntidadeAssociada, String Usuario, String Senha, Int32 TipoDeUsuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    AcessoRepository acessoRepository = new AcessoRepository();

                    IAcesso umAcesso = acessoRepository.GetByID(Id);

                    umAcesso.AlterarSenha(Senha);

                    if (umAcesso.Erros.Count > 0)
                    {
                        String result = String.Join(", ", umAcesso.Erros.ToArray());
                        FaultException fault = new FaultException(result);

                        throw fault;
                    }

                    acessoRepository.Salvar(umAcesso);

                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {

            }
            catch (ApplicationException ex)
            {
                // aqui serão tratadas as exceptions de domínio.
            }
        }

        public AcessoModel Get(Guid Id)
        {
            AcessoRepository acessoRepository = new AcessoRepository();
            AcessoModel modelo = new AcessoModel();

            modelo = CriarModeloDeView(acessoRepository.GetByID(Id));

            return modelo;
        }

        public void AlterarSenha(String Usuario, String Senha)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    AcessoRepository acessoRepository = new AcessoRepository();

                    IAcesso umAcesso = acessoRepository.GetByUsuarioESenha(Usuario, Senha);

                    if (umAcesso != null)
                        umAcesso.AlterarSenha(Senha);
                    else
                    {
                        String result = "Usuário não existente na base de dados";
                        FaultException fault = new FaultException(result);

                        throw fault;
                    }

                    if (umAcesso.Erros.Count > 0)
                    {
                        String result = String.Join(", ", umAcesso.Erros.ToArray());
                        FaultException fault = new FaultException(result);

                        throw fault;
                    }

                    
                    acessoRepository.Salvar(umAcesso);
                    
                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {

            }
            catch (ApplicationException ex)
            {
                // aqui serão tratadas as exceptions de domínio.
            }
        }

        public List<AcessoModel> ListarTodosAcessos()
        {
            AcessoRepository acessoRepository = new AcessoRepository();
            List<AcessoModel> modelo = new List<AcessoModel>();

            List<IAcesso> acessos = acessoRepository.ListAll();


            foreach (IAcesso umAcesso in acessos)
            {
                modelo.Add(CriarModeloDeView(umAcesso));
            }

            modelo = modelo.OrderBy(x => x.Usuario).ToList();

            return modelo;
        }

        public List<TiposModel> ListarTipos()
        {
            List<TiposModel> modelo = new List<TiposModel>();

            modelo.Add(new TiposModel() { Text = TipoDeAcesso.Administrador.ToString(), Value = (Int32)TipoDeAcesso.Administrador });
            modelo.Add(new TiposModel() { Text = TipoDeAcesso.Diretor.ToString(), Value = (Int32)TipoDeAcesso.Diretor });
            modelo.Add(new TiposModel() { Text = TipoDeAcesso.FuncionárioAcadêmico.ToString(), Value = (Int32)TipoDeAcesso.FuncionárioAcadêmico });
            modelo.Add(new TiposModel() { Text = TipoDeAcesso.Professor.ToString(), Value = (Int32)TipoDeAcesso.Professor });

            return modelo;
        }

        private AcessoModel CriarModeloDeView(IAcesso umAcesso)
        {
            AcessoModel acesso = new AcessoModel();
            acesso.Id = umAcesso.Id;
            acesso.Usuario = umAcesso.Usuário;
            acesso.Senha = umAcesso.Senha;
            acesso.TipoDeUsuario.NomeDoTipo = umAcesso.TipoDeUsuário.ToString();
            acesso.TipoDeUsuario.Tipo = (Int32)umAcesso.TipoDeUsuário;

            return acesso;
        }

        public UsuarioLogado BuscarUsuarioLogado(String usuario)
        {
            UsuarioLogado modelo = new UsuarioLogado();

            AcessoRepository acessoRepository = new AcessoRepository();

            IAcesso umAcesso = acessoRepository.GetByUsuario(usuario);

            try
            {
                modelo.Usuario = usuario;
                modelo.Tipo = umAcesso.TipoDeUsuário.ToString();
                modelo.TipoValue = Convert.ToInt32(umAcesso.TipoDeUsuário);
                modelo.Id = umAcesso.Id;
            }
            catch (Exception e)
            {
                modelo.Usuario = "Não Logado";
                modelo.Tipo = "Não Logado";

                return modelo;
            }

            return modelo;
        }

        public InfoDeLogon Login(String Usuario, String Senha, Int32 TipoDeUsuario)
        {
            AcessoRepository acessoRepository = new AcessoRepository();

            InfoDeLogon modelo = new InfoDeLogon();

            IAcesso acessoEncontrado = acessoRepository.GetByUsuarioESenha(Usuario, Senha);


            if (acessoEncontrado == null)
            {

                String[] usuárioPadrao = ConfigurationManager.AppSettings["UsuarioPadrao"].Split(';');

                if (Usuario.Equals(usuárioPadrao[0]) && Senha.Equals(usuárioPadrao[1]))
                {
                    modelo.AcessoLiberado = true;
                    modelo.NomeDoUsuario = usuárioPadrao[0];
                    modelo.Login = usuárioPadrao[0];
                    modelo.TipoDeUsuario.Tipo = (Int32)TipoDeAcesso.Administrador;
                    modelo.TipoDeUsuario.NomeDoTipo = TipoDeAcesso.Administrador.ToString();
                    modelo.Response = "Você será redirecionado para Página Inicial";
                }
                else
                {
                    modelo.AcessoLiberado = false;
                    modelo.NomeDoUsuario = String.Empty;
                    modelo.TipoDeUsuario.NomeDoTipo = String.Empty;
                    modelo.TipoDeUsuario.Tipo = (Int32)TipoDeAcesso.NãoDefinido;
                    modelo.Response = "Usuário ou senha incorretos.";
                }
            }
            else if (acessoEncontrado.TipoDeUsuário.Equals(TipoDeAcesso.Aluno))
            {
                modelo.AcessoLiberado = false;
                modelo.NomeDoUsuario = String.Empty;
                modelo.TipoDeUsuario.NomeDoTipo = String.Empty;
                modelo.TipoDeUsuario.Tipo = (Int32)TipoDeAcesso.NãoDefinido;
                modelo.Response = "Acesso negado á ÁREA ACADÊMICA, tente realizar autenticação através da ÁREA DO ALUNO";
            }
            else
            {
                modelo.AcessoLiberado = true;
                modelo.NomeDoUsuario = acessoEncontrado.Usuário;
                modelo.Login = acessoEncontrado.Usuário;
                modelo.TipoDeUsuario.Tipo = (Int32)acessoEncontrado.TipoDeUsuário;
                modelo.TipoDeUsuario.NomeDoTipo = acessoEncontrado.TipoDeUsuário.ToString();
                modelo.Response = "Você será redirecionado para Página Inicial";
            }

            return modelo;
        }

        public InfoDeLogon LogarComoALuno(String RA, String RG, String Senha)
        {
            AcessoRepository acessoRepository = new AcessoRepository();
            AlunoRepository alunoRepository = new AlunoRepository();

            InfoDeLogon modelo = new InfoDeLogon();

            IAcesso acessoEncontrado = acessoRepository.GetByUsuarioESenha(RA, Senha);

            if (acessoEncontrado == null)
            {
                modelo.AcessoLiberado = false;
                modelo.NomeDoUsuario = String.Empty;
                modelo.TipoDeUsuario.NomeDoTipo = String.Empty;
                modelo.TipoDeUsuario.Tipo = (Int32)TipoDeAcesso.NãoDefinido;
            }
            else
            {
                IAluno aluno = alunoRepository.GetByRA(RA);

                if (aluno == null)
                {
                    modelo.AcessoLiberado = false;
                    modelo.NomeDoUsuario = String.Empty;
                    modelo.TipoDeUsuario.NomeDoTipo = String.Empty;
                    modelo.TipoDeUsuario.Tipo = (Int32)TipoDeAcesso.NãoDefinido;
                }
                else
                {
                    modelo.AcessoLiberado = true;
                    modelo.NomeDoUsuario = aluno.Nome + " " + aluno.Sobrenome;
                    modelo.TipoDeUsuario.Tipo = (Int32)TipoDeAcesso.Aluno;
                    modelo.Login = acessoEncontrado.Usuário;
                    modelo.TipoDeUsuario.NomeDoTipo = TipoDeAcesso.Aluno.ToString();
                }
            }

            return modelo;
        }


        public Boolean RecuperarSenha(String RA, String RG, String Senha)
        {
            AcessoRepository acessoRepository = new AcessoRepository();
            AlunoRepository alunoRepository = new AlunoRepository();

            InfoDeLogon modelo = new InfoDeLogon();

            IAcesso acessoEncontrado = acessoRepository.GetByUsuarioESenha(RA, Senha);


            IAluno aluno = alunoRepository.GetByRA(RA);


            if (aluno.Matrícula.RA.Equals(RA) && aluno.RG.Equals(RG) && aluno.Email.ToUpper().Equals(Senha.ToUpper()))
            {

                String Message = MailSave.GetMessage(aluno.Nome + " " + aluno.Sobrenome, aluno.Matrícula.RA, aluno.Matrícula.RA, TypeMessage.AcessoCriado);

                MailSave.SaveMail(aluno.Email, "SOLICITA WEB - INFORMAÇÕES DE ACESSO", Message);

                return true;
            }

            return false;            
        }
    }
}
