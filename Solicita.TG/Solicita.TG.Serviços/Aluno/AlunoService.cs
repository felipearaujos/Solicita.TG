using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Curso;
using Solicita.TG.Domínio.Entidades.Matricula;
using Solicita.TG.Persistência.Repositories.CursoRepository;
using Solicita.TG.Persistência.Repositories.RepositórioAluno;
using Solicita.TG.Serviços.Aluno.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Solicita.TG.Serviços.Aluno
{
    public class AlunoService : IAlunoService
    {
        public void Criar(String Nome, String Sobrenome, String RA, String RG, Int32 Periodo, Int32 Turno, Guid Curso)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    IAluno alunoCriado = Solicita.TG.Domínio.Entidades.Aluno.Aluno.CriarAluno(Nome, Sobrenome,
                        RA, RG, (Período)Periodo, (Turno)Turno, Curso);

                    if (alunoCriado.Erros.Count > 0)
                    {
                        String result = String.Join(", ", alunoCriado.Erros.ToArray());
                        FaultException fault = new FaultException(result);

                        throw fault;
                    }

                    AlunoRepository alunoRepository = new AlunoRepository();

                    alunoRepository.Salvar(alunoCriado);


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

        public void Salvar(Guid Id, String Nome, String Sobrenome, String RA, String RG, Int32 Periodo, Int32 Turno, Guid Curso)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    AlunoRepository alunoRepository = new AlunoRepository();

                    IAluno alunoCriado = alunoRepository.GetByID(Id);

                    alunoCriado.AlterarNome(Nome);
                    alunoCriado.AlterarSobrenome(Sobrenome);
                    alunoCriado.AlterarPeríodo((Período)Periodo);
                    alunoCriado.AlterarTurno((Turno)Turno);
                    alunoCriado.DefinirMatrícula(RA, ((Período)Periodo), ((Turno)Turno), Curso);
                    //alunoCriado.alterarRG? ? wjhat ? qdo lembrar eu faço 

                    if (alunoCriado.Erros.Count > 0)
                    {
                        String result = String.Join(", ", alunoCriado.Erros.ToArray());
                        FaultException fault = new FaultException(result);

                        throw fault;
                    }

                    alunoRepository.Salvar(alunoCriado);

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

        public List<AlunoModel> Listar(String RA)
        {
            AlunoRepository alunoRepository = new AlunoRepository();
            List<AlunoModel> modelo = new List<AlunoModel>();

            List<IAluno> alunos = alunoRepository.ListAll();

            if (RA != String.Empty)
                alunos = alunos.Where(x => x.Matrícula.RA.Equals(RA)).ToList();

            foreach (IAluno umAluno in alunos)
            {
                modelo.Add(CriarModeloDeView(umAluno));
            }

            modelo = modelo.OrderBy(x => x.Nome).ToList();

            return modelo;
        }

        public Modelos.AlunoModel Get(Guid id)
        {
            AlunoRepository alunoRepository = new AlunoRepository();

            IAluno entidade = alunoRepository.GetByID(id);

            return CriarModeloDeView(entidade);
        }

        public Modelos.AlunoModel GetByRA(String RA)
        {
            AlunoRepository alunoRepository = new AlunoRepository();

            IAluno entidade = alunoRepository.GetByRA(RA);

            return CriarModeloDeView(entidade);
        }

        private AlunoModel CriarModeloDeView(IAluno umAluno)
        {
            CursoRepository cursoRepository = new CursoRepository();
            ICurso curso = cursoRepository.GetByID(umAluno.Matrícula.Curso);

            AlunoModel aluno = new AlunoModel();
            aluno.Id = umAluno.Id;
            aluno.Nome = umAluno.Nome;
            aluno.Email = umAluno.Email;
            aluno.Sobrenome = umAluno.Sobrenome;
            aluno.Matricula.Curso = curso.Nome;
            aluno.RG = umAluno.RG;
            aluno.Matricula.RA = umAluno.Matrícula.RA;
            aluno.Matricula.Periodo = ((Int32)umAluno.Matrícula.Período).ToString();
            aluno.Matricula.Turno = umAluno.Matrícula.Turno.ToString();

            return aluno;
        }
    }
}
