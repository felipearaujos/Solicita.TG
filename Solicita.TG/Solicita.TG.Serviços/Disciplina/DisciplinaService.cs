using Solicita.TG.Domínio.Entidades.Disciplina;
using Solicita.TG.Persistência.Repositories.DisciplinaRepository;
using Solicita.TG.Serviços.DisciplinaService.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Transactions;

namespace Solicita.TG.Serviços.DisciplinaService
{

    public class DisciplinaService : IDisciplinaService
    {
        private DisciplinaRepository disciplinaRepository;
        
        public Guid Criar(String nome, Int32 cargaHoraria)
        {
            Guid Id = Guid.Empty;
            
            disciplinaRepository = new DisciplinaRepository();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    IDisciplina disciplina = Disciplina.CriarDisciplina(nome, cargaHoraria);

                    disciplinaRepository.Salvar(disciplina);

                    scope.Complete();

                    Id =  disciplina.Id;
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

        public void Salvar(Guid id, String novoNome, Int32 novaCargaHoraria)
        {
            disciplinaRepository = new DisciplinaRepository();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    IDisciplina disciplina = disciplinaRepository.GetByID(id);

                    disciplina.DefinirNome(novoNome);
                    disciplina.DefinirCargaHorária(novaCargaHoraria);

                    disciplinaRepository.Salvar(disciplina);

                    scope.Complete();
 
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

        public void Excluir(Guid id)
        {
            disciplinaRepository = new DisciplinaRepository();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    IDisciplina disciplina = disciplinaRepository.GetByID(id);

                    disciplinaRepository.Excluir(disciplina);

                    scope.Complete();
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

        public DisciplinaModel Get(Guid Id)
        {
            disciplinaRepository = new DisciplinaRepository();

            IDisciplina disciplina = disciplinaRepository.GetByID(Id);

            return CriarModeloDeView(disciplina);
        }

        public List<DisciplinaModel> Listar()
        {
            disciplinaRepository = new DisciplinaRepository();

            List<IDisciplina> disciplinas = disciplinaRepository.ListAll();

            List<DisciplinaModel> modelos = new List<DisciplinaModel>();

            foreach (IDisciplina disciplina in disciplinas)
            {
                DisciplinaModel modelo = CriarModeloDeView(disciplina);

                modelos.Add(modelo);
            }

            return modelos;
        }

        public List<DisciplinaModel> ListarDisciplinasPorNome(String nome)
        {
            disciplinaRepository = new DisciplinaRepository();
            
            List<IDisciplina> disciplinas = disciplinaRepository.ListAll().Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToList();

            List<DisciplinaModel> modelos = new List<DisciplinaModel>();

            foreach (IDisciplina disciplina in disciplinas)
            {
                DisciplinaModel modelo = CriarModeloDeView(disciplina);

                modelos.Add(modelo);
            }

            return modelos;
        }

        private DisciplinaModel CriarModeloDeView(IDisciplina disciplina)
        {
            if (disciplina == null)
            {
                return null;
            }

            DisciplinaModel modelo = new DisciplinaModel();

            modelo.Id = disciplina.Id;
            modelo.Nome = disciplina.Nome;
            modelo.CargaHoraria = disciplina.CargaHorária;

            return modelo;
        }
    }
}
