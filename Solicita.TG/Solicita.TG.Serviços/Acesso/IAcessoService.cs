using Solicita.TG.Serviços.Acesso.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Acesso
{
    [ServiceContract]
    public interface IAcessoService
    {
        [OperationContract]
        [FaultContract(typeof(MyServiceFault))]
        Guid CriarAcesso(Guid IdEntidadeAssociada, String Usuario, String Senha, Int32 TipoDeUsuario);
        
        [OperationContract]
        void SalvarAcesso(Guid Id, Guid IdEntidadeAssociada, String Usuario, String Senha, Int32 TipoDeUsuario);

        [OperationContract]
        UsuarioLogado BuscarUsuarioLogado(String usuario);

        [OperationContract]
        List<AcessoModel> ListarTodosAcessos();

        [OperationContract]
        AcessoModel Get(Guid Id);

        [OperationContract]
        void AlterarSenha(String Usuario, String Senha);

        [OperationContract]
        [FaultContract(typeof(MyServiceFault))]
        InfoDeLogon Login(String Usuario, String Senha, Int32 TipoDeUsuario);

        [OperationContract]
        List<TiposModel> ListarTipos();

        [OperationContract]
        InfoDeLogon LogarComoALuno(String RA, String RG, String Senha);

        [OperationContract]
        Boolean RecuperarSenha(String RA, String RG, String Email);
    }
}
