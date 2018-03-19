using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Domínio.Entidades.Disciplina
{
    public class Disciplina : IDisciplina
    {
        #region Atributos

        public Guid Id { get; private set; }

        public String Nome { get; private set; }

        public Int32 CargaHorária { get; private set; }

        public List<String> Erros { get; private set; }

        #endregion Atributos

        #region Construtores

        public Disciplina()
        {
            Erros = new List<String>();
        }

        public static Disciplina CriarDisciplina(String nome, Int32 cargaHorária)
        {
            Disciplina disciplina = new Disciplina();

            disciplina.ValidarDadosDisplina(nome, cargaHorária);

            disciplina.Id = Guid.NewGuid();
            disciplina.DefinirNome(nome);
            disciplina.DefinirCargaHorária(cargaHorária);

            return disciplina;
        }
        public static Disciplina CriarDisciplina(Guid id, String nome, Int32 cargaHorária)
        {
            Disciplina disciplina = new Disciplina();
            disciplina.Id = id;
            disciplina.DefinirNome(nome);
            disciplina.DefinirCargaHorária(cargaHorária);

            return disciplina;
        }

        #endregion Construtores

        #region Métodos

        public void DefinirCargaHorária(Int32 cargaHoraria)
        {
            if (cargaHoraria == 0)
                throw new InvalidOperationException("Carga Horário não deve Ser Zero");
            else if (cargaHoraria < 0)
                throw new InvalidOperationException("Carga Horário não deve ser negativa");

            this.CargaHorária = cargaHoraria;
        }

        public void DefinirNome(String nome)
        {
            if (nome.Equals(String.Empty))
                throw new ArgumentException("O nome da Disciplina não pode ser vazio!");

            this.Nome = nome;
        }


        //ALERT: TESTANDO COISAS
        private void ValidarDadosDisplina(String nome, Int32 cargaHoraria)
        {
            String exception = String.Empty;

            if (cargaHoraria == 0)
                Erros.Add("Carga Horário não deve Ser Zero");

            else if (cargaHoraria < 0)
                Erros.Add("Carga Horário não deve ser negativa");

            if (nome.Equals(String.Empty))
                Erros.Add("O nome da Disciplina não pode ser vazio!");

            if (Erros.Count > 0)
            {
                String result = String.Join(", ", Erros.ToArray());

                throw new InvalidOperationException(result);
            }

        }
        
        #endregion Métodos

    }
}
