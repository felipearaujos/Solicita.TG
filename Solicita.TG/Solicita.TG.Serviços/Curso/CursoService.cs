using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Curso;
using Solicita.TG.Domínio.Entidades.Disciplina;
using Solicita.TG.Persistência.Repositories.CursoRepository;
using Solicita.TG.Persistência.Repositories.DisciplinaRepository;
using Solicita.TG.Persistência.Repositories.RepositórioAluno;
using Solicita.TG.Serviços.cursoService.Modelos;
using Solicita.TG.Serviços.DisciplinaService.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Transactions;

namespace Solicita.TG.Serviços.cursoService
{
   
    public class CursoService : ICursoService
    {
        private CursoRepository cursoRepository;
        private DisciplinaRepository disciplinaRepository;
        private AlunoRepository alunoRepository;

        public Guid Criar(String nome, List<Guid> idDisciplinas)
        {
            Guid Id = Guid.Empty;

            cursoRepository = new CursoRepository();
            disciplinaRepository = new DisciplinaRepository();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ICurso curso = Curso.CriarCurso(nome);

                    
                    foreach(Guid id in idDisciplinas)
                    {
                        IDisciplina disciplina = disciplinaRepository.GetByID(id);
                        curso.AdicionarDisciplina(disciplina);
                    }

                    cursoRepository.Salvar(curso);

                    scope.Complete();

                    Id = curso.Id;
                }

            }
            catch (TransactionAbortedException ex)
            {

            }
            catch (InvalidOperationException ex)
            {
                throw new FaultException(ex.Message);
            }

            return Id;
        }

        public void Salvar(Guid id, String nome, List<Guid> idDisciplinas)
        {
            Guid Id = Guid.Empty;

            cursoRepository = new CursoRepository();
            disciplinaRepository = new DisciplinaRepository();
            
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    ICurso curso = cursoRepository.GetByID(id);

                    curso.DefinirNome(nome);

                    List<IDisciplina> GradeCurricularOld = curso.GradeCurricular.ToList();
                    foreach (var umaDisciplina in GradeCurricularOld)
                    {
                        if (!idDisciplinas.Any(x => x.Equals(umaDisciplina.Id)))
                            curso.RemoverDisciplina(umaDisciplina);

                    }

                    foreach (Guid idDisciplina in idDisciplinas)
                    {
                        IDisciplina disciplina = disciplinaRepository.GetByID(idDisciplina);

                        if(!curso.GradeCurricular.Any(x=> x.Id.Equals(idDisciplina)))
                            curso.AdicionarDisciplina(disciplina);
                        
                    }
                    
                    cursoRepository.Salvar(curso);

                    scope.Complete();

                    Id = curso.Id;
                }

            }
            catch (TransactionAbortedException ex)
            {

            }
            catch (InvalidOperationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        
        public Modelos.CursoModel Get(Guid id)
        {
            cursoRepository = new CursoRepository();

            ICurso curso = cursoRepository.GetByID(id);

            return CriarModeloDeView(curso);
        }

        public List<Modelos.CursoModel> Listar()
        {
            cursoRepository = new CursoRepository();

            List<ICurso> cursos = cursoRepository.ListAll();

            List<CursoModel> modelos = new List<CursoModel>();

            foreach (ICurso disciplina in cursos)
            {
                CursoModel modelo = CriarModeloDeView(disciplina);

                modelos.Add(modelo);
            }

            return modelos;
        }

        public List<Modelos.CursoModel> ListarCursosDoAluno(Guid idAluno)
        {
            cursoRepository = new CursoRepository();

            alunoRepository = new AlunoRepository();

            IAluno aluno = alunoRepository.GetByID(idAluno);

            ICurso cursos = cursoRepository.GetByID(aluno.Id);

            List<CursoModel> modelos = new List<CursoModel>();

            //foreach (ICurso disciplina in cursos)
            //{
            //    CursoModel modelo = CriarModeloDeView(disciplina);

            //    modelos.Add(modelo);
            //}

            return modelos;
        }

        public void Excluir(Guid id)
        {
            throw new NotImplementedException();
        }

        private Modelos.CursoModel CriarModeloDeView(ICurso curso)
        {
            if (curso == null)
            {
                return null;
            }

            Modelos.CursoModel modelo = new Modelos.CursoModel();

            modelo.Id = curso.Id;
            modelo.CargaHoraria = curso.CargaHorária;
            modelo.Nome = curso.Nome;

            foreach (IDisciplina disciplina in curso.GradeCurricular)
            {
                DisciplinaModel disciplinadocurso = new DisciplinaModel();

                disciplinadocurso.Id = disciplina.Id;
                disciplinadocurso.Nome = disciplina.Nome;
                disciplinadocurso.CargaHoraria = disciplina.CargaHorária;

                modelo.GradeCurricular.Add(disciplinadocurso);
            }

            return modelo;
        }
    }
}
