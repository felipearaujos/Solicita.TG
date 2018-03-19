using Solicita.TG.Domínio.Entidades.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Disciplina
{
    public interface IDisciplina : IEntidade
    {
        String Nome { get; }
        Int32 CargaHorária { get; }

        void DefinirNome(String nome);
        void DefinirCargaHorária(Int32 cargaHoraria);
    }
}
