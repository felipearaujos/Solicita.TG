using Solicita.TG.Domínio.Entidades.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Persistência.Repositories.Base
{
    public interface IRepository<T> where T : IEntidade
    {
        void Salvar(T entidade);
        void Excluir(T entidade);

        T GetByID(Guid id);
        List<T> ListAll();

        void LimparBancoDeDados();
        
        
       
    }
}
