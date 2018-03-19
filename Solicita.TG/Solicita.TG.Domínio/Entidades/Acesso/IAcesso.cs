using Solicita.TG.Domínio.Entidades.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Acesso
{
    public interface IAcesso: IEntidade
    {
        String Usuário { get; }
        String Senha { get; }
        TipoDeAcesso TipoDeUsuário { get; }
        Guid IdEntidadeAssociada { get; }

        void AlterarSenha(String senha);
    }
}
