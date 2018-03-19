using Solicita.TG.Domínio.Entidades.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Pessoa
{
    public interface IPessoa : IEntidade
    {
        String Nome { get; }
        String Sobrenome { get; }

        void AlterarNome(String Nome);
        void AlterarSobrenome(String Sobrenome);
    }
}
