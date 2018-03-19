using Solicita.TG.Domínio.Entidades.FuncionarioAcademico;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using Solicita.TG.Persistência.Repositories.FuncionarioAcademicoRepository;
using Solicita.TG.Serviços.FuncionarioAcademico.Modelos;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Solicita.TG.Serviços.FuncionarioAcademico
{
    public class FuncionarioAcademicoService : IFuncionarioAcademicoService
    {
        public Guid CriarFuncionario(String Nome, String Sobrenome, Int32 TipoResponsavel)
        {
            Guid id = Guid.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    IFuncionarioAcademico funcionarioCriado = Solicita.TG.Domínio.Entidades.FuncionarioAcademico.FuncionarioAcademico.
                        CriarFuncionarioAcademico(Nome, Sobrenome, (TipoDeResponsável)TipoResponsavel);
                    
                    if (funcionarioCriado.Erros.Count > 0)
                    {
                        String result = String.Join(", ", funcionarioCriado.Erros.ToArray());
                        FaultException fault = new FaultException(result);

                        throw fault;
                    }

                    FuncionarioAcademicoRepository funcionarioAcademicoRepository = new FuncionarioAcademicoRepository();

                    funcionarioAcademicoRepository.Salvar(funcionarioCriado);

                    id = funcionarioCriado.Id;

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

            return id;
        }

        public FuncionarioModel Get(Guid Id)
        {
            FuncionarioAcademicoRepository funcionarioAcademicoRepository = new FuncionarioAcademicoRepository();
            FuncionarioModel modelo = new FuncionarioModel();

            modelo = CriarModeloDeView(funcionarioAcademicoRepository.GetByID(Id));

            return modelo;
        }

        public List<FuncionarioModel> ListarTodosFuncionarios()
        {
            FuncionarioAcademicoRepository funcionarioAcademicoRepository = new FuncionarioAcademicoRepository();
            List<FuncionarioModel> modelo = new List<FuncionarioModel>();

            List<IFuncionarioAcademico> funcionarios = funcionarioAcademicoRepository.ListAll();


            foreach (IFuncionarioAcademico umFuncionario in funcionarios)
            {
                modelo.Add(CriarModeloDeView(umFuncionario));
            }

            modelo = modelo.OrderBy(x => x.Nome).ToList();

            return modelo;
        }

        public List<FuncionarioModel> ListarPorNome(String nome)
        {
            FuncionarioAcademicoRepository funcionarioAcademicoRepository = new FuncionarioAcademicoRepository();
            List<FuncionarioModel> modelo = new List<FuncionarioModel>();

            List<IFuncionarioAcademico> funcionarios = funcionarioAcademicoRepository.ListAll().Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToList();


            foreach (IFuncionarioAcademico umFuncionario in funcionarios)
            {
                modelo.Add(CriarModeloDeView(umFuncionario));
            }

            modelo = modelo.OrderBy(x => x.Nome).ToList();

            return modelo;
        }

        public List<TipoDeResponsavelModel> ListarTipoDeResponsável()
        {
            List<TipoDeResponsavelModel> modelos = new List<TipoDeResponsavelModel>();

            Array values = Enum.GetValues(typeof(TipoDeResponsável));

            foreach (TipoDeResponsável val in values)
            {
                TipoDeResponsavelModel modelo = new TipoDeResponsavelModel();

                modelo.Tipo = Enumerations.GetEnumDescription(val);
                modelo.Identificador = ((Int32)val).ToString();

                modelos.Add(modelo);
            }

            return modelos;
        }

        private FuncionarioModel CriarModeloDeView(IFuncionarioAcademico umFuncionarioAcademico)
        {
            FuncionarioModel funcionarioModel = new FuncionarioModel();
            funcionarioModel.Id = umFuncionarioAcademico.Id;
            funcionarioModel.Nome = umFuncionarioAcademico.Nome;
            funcionarioModel.Sobrenome = umFuncionarioAcademico.Sobrenome;
            
            return funcionarioModel;
        }
    }
}
