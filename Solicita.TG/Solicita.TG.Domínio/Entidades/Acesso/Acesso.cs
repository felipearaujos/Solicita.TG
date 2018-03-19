using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Acesso
{
    public class Acesso: IAcesso
    {

        #region Construtores

        Criptografia criptor;

        public Acesso()
        {
            criptor = new Criptografia();
            this.Usuário = String.Empty;
            this.Senha = String.Empty;
            this.TipoDeUsuário = TipoDeAcesso.NãoDefinido;
            this.Id = Guid.NewGuid();
            this.Erros = new List<string>();
        }

        public Acesso(Guid idEntidadeAssociada, String usuário, String senha, TipoDeAcesso tipoDeUsuário)
        {
            criptor = new Criptografia();
            this.Usuário = usuário;
            this.Senha = criptor.Encrypt(senha);
            this.TipoDeUsuário = tipoDeUsuário;
            this.Id = Guid.NewGuid();
            this.IdEntidadeAssociada = idEntidadeAssociada;
            this.Erros = new List<string>();
        }

        public Acesso(Guid Id, Guid idEntidadeAssociada, String usuário, String senha, TipoDeAcesso tipoDeUsuário)
        {
            criptor = new Criptografia();
            this.Usuário = usuário;
            this.Senha = senha;
            this.TipoDeUsuário = tipoDeUsuário;
            this.IdEntidadeAssociada = idEntidadeAssociada;
            this.Id = Id;
            this.Erros = new List<string>();
        }

        public static IAcesso CriarAcesso(Guid idEntidadeAssociada, String usuário, String senha, TipoDeAcesso tipoDeUsuário)
        {
            IAcesso umAcesso = new Acesso(idEntidadeAssociada, usuário, senha, tipoDeUsuário);

            return umAcesso;
        }

        public static IAcesso CriarAcesso(Guid Id, Guid idEntidadeAssociada, String usuário, String senha, TipoDeAcesso tipoDeUsuário)
        {
            IAcesso umAcesso = new Acesso(Id, idEntidadeAssociada, usuário, senha, tipoDeUsuário);

            return umAcesso;
        }

        #endregion Construtores

        #region Atributos

        public Guid Id { get; private set; }

        public String Usuário { get; private set; }

        public String Senha { get; private set; }

        public TipoDeAcesso TipoDeUsuário { get; private set; }

        public List<String> Erros { get; private set; }

        public Guid IdEntidadeAssociada { get; set; }

        #endregion Atributos

        #region Métodos

        public void AlterarSenha(String senha)
        {
            if (String.IsNullOrEmpty(senha))
                Erros.Add("Não é possível adicionar uma senha vazia!");

            if (senha.Length < 6)
                Erros.Add("A senha deve conter no mínimo 6 caracteres");

            this.Senha = criptor.Encrypt(senha);
        }

        #endregion

    }
}
