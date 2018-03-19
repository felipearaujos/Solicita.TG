using Solicita.TG.Domínio.Entidades.Professor;
using Solicita.TG.Persistência.Repositories.ProfessorRepository;
using Solicita.TG.Serviços.Professor.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Solicita.TG.Serviços.Professor
{
    public class ProfessorService: IProfessorService
    {
        public Guid Criar(String Nome, String Sobrenome, Boolean Cordenador)
        {
            Guid id = Guid.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    IProfessor professorCriado = Solicita.TG.Domínio.Entidades.Professor.Professor.CriarProfessor(Nome, Sobrenome,
                        Cordenador);

                    if (professorCriado.Erros.Count > 0)
                    {
                        String result = String.Join(", ", professorCriado.Erros.ToArray());
                        FaultException fault = new FaultException(result);

                        throw fault;
                    }

                    ProfessorRepository alunoRepository = new ProfessorRepository();

                    alunoRepository.Salvar(professorCriado);

                    id = professorCriado.Id;

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

        public void Salvar(String Nome, String Sobrenome, Boolean Cordenador)
        {
            Guid id = Guid.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                   
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


        public ProfessorModel Get(Guid Id)
        {
            ProfessorRepository alunoRepository = new ProfessorRepository();
            ProfessorModel modelo = new ProfessorModel();

            modelo = CriarModeloDeView(alunoRepository.GetByID(Id));

            return modelo;
        }

        public List<ProfessorModel> ListarTodosProfessores()
        {
            ProfessorRepository alunoRepository = new ProfessorRepository();
            List<ProfessorModel> modelo = new List<ProfessorModel>();

            List<IProfessor> professores = alunoRepository.ListAll();


            foreach (IProfessor umProfessor in professores)
            {
                modelo.Add(CriarModeloDeView(umProfessor));
            }

            modelo = modelo.OrderBy(x => x.Nome).ToList();

            return modelo;
        }

        private ProfessorModel CriarModeloDeView(IProfessor umProfessor)
        {
            ProfessorModel professor = new ProfessorModel();
            professor.Id = umProfessor.Id;
            professor.Nome = umProfessor.Nome;
            professor.Sobrenome = umProfessor.Sobrenome;
            if (umProfessor.ÉCordenador)
                professor.Cordenador = "Sim";
            else
                professor.Cordenador = "Não";

            return professor;
        }
    }
}
