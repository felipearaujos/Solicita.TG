using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;
using Solicita.TG.Domínio.Entidades.Curso;
using Solicita.TG.Domínio.Entidades.Disciplina;
using System.Collections.Generic;

namespace Solicita.TG.Domínio.Testes
{
    [TestClass]
    public class CursoTestes
    {
        ICurso objetoDeTestes;

        [TestInitialize]
        public void SetUp()
        {
            objetoDeTestes = Curso.CriarCurso("ADS");
        }

        [TestMethod]
        public void testeDeveCriarUmCursoComONome()
        {
            objetoDeTestes.Nome.Should().Be.EqualTo("ADS");
            objetoDeTestes.CargaHorária.Should().Be.EqualTo(0);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(0);

        } 

        [TestMethod]
        public void testeDevePermitirCriarUmCursoComNomeEDisciplinas()
        {
            IDisciplina LinguaGemDeProgramação = Disciplina.CriarDisciplina("Linguagem de Programação", 80);
            IDisciplina EngenhariaIII = Disciplina.CriarDisciplina("Engenharia de Software III", 80);

            List<IDisciplina> disciplinas = new List<IDisciplina>();
            disciplinas.Add(LinguaGemDeProgramação);
            disciplinas.Add(EngenhariaIII);

            objetoDeTestes = Curso.CriarCurso(Guid.NewGuid(), "ADS", disciplinas);

            objetoDeTestes.CargaHorária.Should().Be.EqualTo(160);
            objetoDeTestes.Nome.Should().Be.EqualTo("ADS");
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(2);
        }

        [TestMethod]
        public void testeDeveAdicionarDisciplinasAoCursoESomarACargaHoáriaDoCurso()
        {
            IDisciplina LinguaGemDeProgramação = Disciplina.CriarDisciplina("Linguagem de Programação", 80);
            IDisciplina EngenhariaIII = Disciplina.CriarDisciplina("Engenharia de Software III", 80);

            objetoDeTestes.CargaHorária.Should().Be.EqualTo(0);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(0);

            objetoDeTestes.AdicionarDisciplina(LinguaGemDeProgramação);
            objetoDeTestes.CargaHorária.Should().Be.EqualTo(80);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(1);

            objetoDeTestes.AdicionarDisciplina(EngenhariaIII);
            objetoDeTestes.CargaHorária.Should().Be.EqualTo(160);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(2);
        }

        [TestMethod]
        public void testeNãoDeveAdicionarDisciplinasRepetidas()
        {
            IDisciplina LinguaGemDeProgramação = Disciplina.CriarDisciplina("Linguagem de Programação", 80);

            objetoDeTestes.CargaHorária.Should().Be.EqualTo(0);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(0);

            objetoDeTestes.AdicionarDisciplina(LinguaGemDeProgramação);
            objetoDeTestes.CargaHorária.Should().Be.EqualTo(80);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(1);

            objetoDeTestes.Executing(x => x.AdicionarDisciplina(LinguaGemDeProgramação)).Throws<InvalidOperationException>();

            objetoDeTestes.CargaHorária.Should().Be.EqualTo(80);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(1);
        }

        [TestMethod]
        public void testeDeveRemoverDisciplinasAoCursoESomarACargaHoáriaDoCurso()
        {
            IDisciplina LinguaGemDeProgramação = Disciplina.CriarDisciplina("Linguagem de Programação", 80);
            IDisciplina EngenhariaIII = Disciplina.CriarDisciplina("Engenharia de Software III", 80);

            objetoDeTestes.CargaHorária.Should().Be.EqualTo(0);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(0);

            objetoDeTestes.AdicionarDisciplina(LinguaGemDeProgramação);
            objetoDeTestes.CargaHorária.Should().Be.EqualTo(80);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(1);

            objetoDeTestes.AdicionarDisciplina(EngenhariaIII);
            objetoDeTestes.CargaHorária.Should().Be.EqualTo(160);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(2);

            objetoDeTestes.RemoverDisciplina(LinguaGemDeProgramação);
            objetoDeTestes.CargaHorária.Should().Be.EqualTo(80);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(1);

            objetoDeTestes.RemoverDisciplina(EngenhariaIII);
            objetoDeTestes.CargaHorária.Should().Be.EqualTo(0);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(0);
        }

        [TestMethod]
        public void testeDeveAdicionarGradeCurricularCompletaNoCurso()
        {
            IDisciplina LinguaGemDeProgramação = Disciplina.CriarDisciplina("Linguagem de Programação", 80);
            IDisciplina EngenhariaIII = Disciplina.CriarDisciplina("Engenharia de Software III", 80);

            List<IDisciplina> disciplinas = new List<IDisciplina>();
            disciplinas.Add(LinguaGemDeProgramação);
            disciplinas.Add(EngenhariaIII);

            objetoDeTestes.CargaHorária.Should().Be.EqualTo(0);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(0);

            objetoDeTestes.AdicionarGradeCurricularCompleta(disciplinas);
            objetoDeTestes.CargaHorária.Should().Be.EqualTo(160);
            objetoDeTestes.GradeCurricular.Count.Should().Be.EqualTo(2);
        }
    }
}
