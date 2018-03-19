using Solicita.TG.Domínio.Entidades.Disciplina;
using Solicita.TG.Domínio.Entidades.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Curso
{
    public interface ICurso : IEntidade
    {
        String Nome { get; }
        List<IDisciplina> GradeCurricular { get; }
        Int32 CargaHorária { get; }
        //IProfessor Coodenador{get};

        void AdicionarDisciplina(IDisciplina disciplina);
        void RemoverDisciplina(IDisciplina disciplina);
        void DefinirNome(String nome);
        void AdicionarGradeCurricularCompleta(List<IDisciplina> gradeCurricular);
        //void DefinirProfessorCoordenador(IProfessor professor);

    }
}
