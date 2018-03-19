using Solicita.TG.Domínio.Entidades.Requerimento;
using Solicita.TG.Serviços.Requerimentos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Mapper
{
    public interface IRequerimentoModelMapper
    {
        RequerimentoModel GetEspecificAttributes(IRequerimento requerimento);

        Type ReturnEspecificType(String type);
    }
}
