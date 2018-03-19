using Solicita.TG.Domínio.Entidades;
using Solicita.TG.Domínio.Entidades.Acesso;
using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Curso;
using Solicita.TG.Domínio.Entidades.Disciplina;
using Solicita.TG.Domínio.Entidades.FuncionarioAcademico;
using Solicita.TG.Domínio.Entidades.Matricula;
using Solicita.TG.Domínio.Entidades.Professor;
using Solicita.TG.Domínio.Entidades.Requerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento;
using Solicita.TG.Persistência.Repositories;
using Solicita.TG.Persistência.Repositories.AcessoRepository;
using Solicita.TG.Persistência.Repositories.CursoRepository;
using Solicita.TG.Persistência.Repositories.DisciplinaRepository;
using Solicita.TG.Persistência.Repositories.FuncionarioAcademicoRepository;
using Solicita.TG.Persistência.Repositories.ProfessorRepository;
using Solicita.TG.Persistência.Repositories.RepositórioAluno;
using Solicita.TG.Persistência.Repositories.RequerimentoRepository;
using Solicita.TG.Serviços.Requerimentos;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Reset
{
    public class Reset
    {
        int minValueRandom = 3;
        int maxValueRandom = 8;

        AcessoRepository acessoRepository;
        AlunoRepository alunoRepository;
        CursoRepository cursoRepository;
        DisciplinaRepository disciplinaRepository;
        ResetRepository resetRepository;
        ProfessorRepository professorRepository;
        FuncionarioAcademicoRepository funcionarioRepository;
        RequerimentoRepository requerimentoRepository;
        IRequerimentoService requerimentoService;

        ICurso AGROCurso;
        ICurso RHCurso;
        ICurso SecretariadoCurso;
        ICurso LogísticaCurso;
        ICurso ADSCurso;

        IFuncionarioAcademico carlosFuncionario;
        IFuncionarioAcademico geraldoFuncionario;
        IFuncionarioAcademico leilaFuncionaria;

        List<IResponsável> listaDeResponsáveis;
        List<ResponsavelAcesso> listaDeResponsáveisAcesso;

        IAcesso carlosAcesso;
        IAcesso geraldoAcesso;
        IAcesso leiaAcesso;

        IAcesso rodolfoAcesso;
        IAcesso angelicaAcesso;
        IAcesso maraisaAcesso;
        IAcesso pedroAcesso;
        IAcesso alexAcesso;
        IAcesso patriciaAcesso;
        IAcesso cristianoAcesso;
        IAcesso beatrizAcesso;

        IAluno alexAluno;
        IAluno PatriciaAluna;
        IAluno CristianoAluno;
        IAluno BeatrizAluna;
        List<IAluno> alunos;

        int countDisciplinas;
        List<IDisciplina> gradeADS;
        List<IDisciplina> gradeRH;
        List<IDisciplina> gradeLogistica;
        List<IDisciplina> gradeSecretariado;
        List<IDisciplina> gradeAgro;

        #region professores

        IProfessor rodolfoProfessor;
        IProfessor angélicaProfessora;
        IProfessor pedroProfessor;
        IProfessor maraisaProfessora;

        #endregion professores

        #region disciplinas

        IDisciplina engenhariaDeSoftware;
        IDisciplina lógicaDeProgramação;
        IDisciplina estruturaDeDados;
        IDisciplina bancoDeDados;

        IDisciplina comportamentoOrganizacional;
        IDisciplina calculosTtabalhistas;
        IDisciplina economia;
        IDisciplina Endomarketing;

        IDisciplina tarifasLogisticas;
        IDisciplina movimentaçãoArmazenagem;
        IDisciplina comércioExterior;
        IDisciplina gestãoTributária;

        IDisciplina ingles;
        IDisciplina tecnicasSecretariais;
        IDisciplina espanhol;
        IDisciplina eitquetaEPostura;
        
        #endregion disciplinas

        public Reset()
        {
            acessoRepository = new AcessoRepository();
            alunoRepository = new AlunoRepository();
            cursoRepository = new CursoRepository();
            disciplinaRepository = new DisciplinaRepository();
            resetRepository = new ResetRepository();
            professorRepository = new ProfessorRepository();
            funcionarioRepository = new FuncionarioAcademicoRepository();
            requerimentoRepository = new RequerimentoRepository();
            requerimentoService = new RequerimentoService();

            listaDeResponsáveis = new List<IResponsável>();
            listaDeResponsáveisAcesso = new List<ResponsavelAcesso>();
        }

        public void Load()
        {
            Console.WriteLine(System.Environment.NewLine + "CRIANDO BANCO DE DESENVOLVIMENTO..." + System.Environment.NewLine);
            resetRepository.ResetDatabase("solicita");

            Console.WriteLine(System.Environment.NewLine + "CRIANDO BANCO DE TESTES..." + System.Environment.NewLine);
            resetRepository.ResetDatabase("solicita_testes");
            
            Console.WriteLine(System.Environment.NewLine + "CRIANDO CURSOS E DISCIPLINAS..." + System.Environment.NewLine);
            LoadCursosEDisciplinas();

            Console.WriteLine(System.Environment.NewLine + "CRIANDO ALUNOS..." + System.Environment.NewLine);
            LoadAlunos();

            Console.WriteLine(System.Environment.NewLine + "CRIANDO PROFESSORES..." + System.Environment.NewLine);
            LoadProfessores();

            Console.WriteLine(System.Environment.NewLine + "CRIANDO FUNCIONÁRIOS..." + System.Environment.NewLine);
            LoadFuncionários();

            Console.WriteLine(System.Environment.NewLine + "CRIANDO ACESSO..." + System.Environment.NewLine);
            LoadAcessos();

            Console.WriteLine(System.Environment.NewLine + "CRIANDO REQUERIMENTOS..." + System.Environment.NewLine);

            #region Novo

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeADS.Count);
                var indexAluno = random.Next(alunos.Count);
                Console.WriteLine("ADS: Criando Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeADS[index].Nome);
                CreateRequerimentos(alunos[indexAluno], gradeADS[index]);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeAgro.Count);
                var indexAluno = random.Next(alunos.Count);
                Console.WriteLine("AGRO: Criando Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeAgro[index].Nome);
                CreateRequerimentos(alunos[indexAluno], gradeAgro[index]);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeLogistica.Count);
                var indexAluno = random.Next(alunos.Count);
                Console.WriteLine("LOG: Criando Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeLogistica[index].Nome);
                CreateRequerimentos(alunos[indexAluno], gradeLogistica[index]);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeRH.Count);
                var indexAluno = random.Next(alunos.Count);
                Console.WriteLine("RH: Criando Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateRequerimentos(alunos[indexAluno], gradeRH[index]);
            }

            #endregion Novo

            #region Transferir Resonsabilidade

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeADS.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveis.Count);
                Console.WriteLine("ADS: Transferindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateTransferirRequerimentos(alunos[indexAluno], gradeADS[index], listaDeResponsáveis[indexResponsavel]);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeAgro.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveis.Count);
                Console.WriteLine("AGRO: Transferindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeAgro[index].Nome);
                CreateTransferirRequerimentos(alunos[indexAluno], gradeAgro[index], listaDeResponsáveis[indexResponsavel]);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeRH.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveis.Count);
                Console.WriteLine("RH: Transferindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateTransferirRequerimentos(alunos[indexAluno], gradeRH[index], listaDeResponsáveis[indexResponsavel]);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeLogistica.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveis.Count);
                Console.WriteLine("LOG: Transferindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateTransferirRequerimentos(alunos[indexAluno], gradeLogistica[index], listaDeResponsáveis[indexResponsavel]);
            }

            #endregion Transferir Resonsabilidade

            #region Concluir

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeADS.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("ADS: Concluindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateConcluirRequerimentos(alunos[indexAluno], gradeADS[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeAgro.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("AGRO: Concluindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeAgro[index].Nome);
                CreateConcluirRequerimentos(alunos[indexAluno], gradeAgro[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeRH.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("RH: Concluindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateConcluirRequerimentos(alunos[indexAluno], gradeRH[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeLogistica.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("LOG: Concluindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateConcluirRequerimentos(alunos[indexAluno], gradeLogistica[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            #endregion Concluir

            #region Cancelar

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeADS.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("ADS: Cancelando Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateCancelarRequerimentos(alunos[indexAluno], gradeADS[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeAgro.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("AGRO: Cancelando Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeAgro[index].Nome);
                CreateCancelarRequerimentos(alunos[indexAluno], gradeAgro[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeRH.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("RH: Cancelando Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateCancelarRequerimentos(alunos[indexAluno], gradeRH[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeLogistica.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("LOG: Cancelando Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateCancelarRequerimentos(alunos[indexAluno], gradeLogistica[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            #endregion Cancelar


            #region Indeferir

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeADS.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("ADS: Indeferindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateIndeferirRequerimentos(alunos[indexAluno], gradeADS[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeAgro.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("AGRO: Indeferindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeAgro[index].Nome);
                CreateIndeferirRequerimentos(alunos[indexAluno], gradeAgro[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeRH.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("RH: Indeferindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateIndeferirRequerimentos(alunos[indexAluno], gradeRH[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            for (int i = 0; i < new Random().Next(minValueRandom, maxValueRandom); i++)
            {
                Random random = new Random();
                var index = random.Next(gradeLogistica.Count);
                var indexAluno = random.Next(alunos.Count);
                var indexResponsavel = random.Next(listaDeResponsáveisAcesso.Count);
                Console.WriteLine("LOG: Indeferindo Requerimento aluno: " + alunos[indexAluno].Nome + " disciplina: " + gradeRH[index].Nome);
                CreateIndeferirRequerimentos(alunos[indexAluno], gradeLogistica[index], listaDeResponsáveisAcesso[indexResponsavel].Id, listaDeResponsáveisAcesso[indexResponsavel].Tipo);
            }

            #endregion Indeferir

            Console.WriteLine(System.Environment.NewLine + "--> DADOS POPULADOS COM SUCESSO." + System.Environment.NewLine);
        }

        private void LoadCursosEDisciplinas()
        {
            AGROCurso = Curso.CriarCurso("Agronegócio");
            RHCurso = Curso.CriarCurso("Recursos Humanos");
            SecretariadoCurso = Curso.CriarCurso("Secretariado");
            LogísticaCurso = Curso.CriarCurso("Logística");

            countDisciplinas = 0;

            #region Disciplinas ADS
            
            ADSCurso = Curso.CriarCurso("Análise e Desenovlvimento de Sistemas");
            
            engenhariaDeSoftware = Disciplina.CriarDisciplina("Engenharia de Software I", 40);

            lógicaDeProgramação = Disciplina.CriarDisciplina("Lógica de Programação", 40);

            estruturaDeDados = Disciplina.CriarDisciplina("Estrutura de Dados", 40);

            bancoDeDados = Disciplina.CriarDisciplina("Banco de Dados I", 40);

            gradeADS = new List<IDisciplina>();

            gradeADS.Add(engenhariaDeSoftware); countDisciplinas++;
            gradeADS.Add(lógicaDeProgramação); countDisciplinas++;
            gradeADS.Add(estruturaDeDados); countDisciplinas++;
            gradeADS.Add(bancoDeDados); countDisciplinas++;

            ADSCurso.AdicionarGradeCurricularCompleta(gradeADS);

            foreach (var umaDisciplina in gradeADS)
            {
                disciplinaRepository.Salvar(umaDisciplina);
                Console.WriteLine("Disciplina " + umaDisciplina.Nome + " Salvo.");
            }

            cursoRepository.Salvar(ADSCurso);
            Console.WriteLine("Curso " + ADSCurso.Nome + " Salvo." + System.Environment.NewLine);

            #endregion

            #region Disciplinas RH

            comportamentoOrganizacional = Disciplina.CriarDisciplina("Comportamento Organizacional I", 40);

            calculosTtabalhistas = Disciplina.CriarDisciplina("Cálculos Trabalhistas I", 40);

            economia = Disciplina.CriarDisciplina("Economia", 40);

            Endomarketing = Disciplina.CriarDisciplina("Endomarketing e Gestão de Clientes Internos", 40);

            gradeRH = new List<IDisciplina>();

            gradeRH.Add(comportamentoOrganizacional); countDisciplinas++;
            gradeRH.Add(calculosTtabalhistas); countDisciplinas++;
            gradeRH.Add(economia); countDisciplinas++;
            gradeRH.Add(Endomarketing); countDisciplinas++;

            RHCurso.AdicionarGradeCurricularCompleta(gradeRH);

            foreach (var umaDisciplina in gradeRH)
            {
                disciplinaRepository.Salvar(umaDisciplina);
                Console.WriteLine("Disciplina " + umaDisciplina.Nome + " Salvo.");
            }

            cursoRepository.Salvar(RHCurso);
            Console.WriteLine("Curso " + RHCurso.Nome + " Salvo." + System.Environment.NewLine);


            #endregion

            #region Disciplinas Logística

            tarifasLogisticas = Disciplina.CriarDisciplina("Custos e Tarifas Logísticas", 40);

            movimentaçãoArmazenagem = Disciplina.CriarDisciplina("Movimentação e Armazenagem", 40);

            comércioExterior = Disciplina.CriarDisciplina("Comércio Exterior e Logística", 40);

            gestãoTributária = Disciplina.CriarDisciplina("Gestão Tributárias", 40);

            gradeLogistica = new List<IDisciplina>();

            gradeLogistica.Add(tarifasLogisticas); countDisciplinas++;
            gradeLogistica.Add(movimentaçãoArmazenagem); countDisciplinas++;
            gradeLogistica.Add(comércioExterior); countDisciplinas++;
            gradeLogistica.Add(gestãoTributária); countDisciplinas++;

            LogísticaCurso.AdicionarGradeCurricularCompleta(gradeLogistica);

            foreach (var umaDisciplina in gradeLogistica)
            {
                disciplinaRepository.Salvar(umaDisciplina);
                Console.WriteLine("Disciplina " + umaDisciplina.Nome + " Salvo.");
            }

            cursoRepository.Salvar(LogísticaCurso);
            Console.WriteLine("Curso " + LogísticaCurso.Nome + " Salvo." + System.Environment.NewLine);

            #endregion

            #region Disciplinas Secretariado

            ingles = Disciplina.CriarDisciplina("Inglês I", 40);

            tecnicasSecretariais = Disciplina.CriarDisciplina("Técnicas Secretariais I", 40);

            espanhol = Disciplina.CriarDisciplina("Espanhol I", 40);

            eitquetaEPostura = Disciplina.CriarDisciplina("Etiqueta e Postura Empresarial", 40);

            gradeSecretariado = new List<IDisciplina>();

            gradeSecretariado.Add(ingles); countDisciplinas++;
            gradeSecretariado.Add(tecnicasSecretariais); countDisciplinas++;
            gradeSecretariado.Add(espanhol); countDisciplinas++;
            gradeSecretariado.Add(eitquetaEPostura); countDisciplinas++;

            SecretariadoCurso.AdicionarGradeCurricularCompleta(gradeSecretariado);

            foreach (var umaDisciplina in gradeSecretariado)
            {
                disciplinaRepository.Salvar(umaDisciplina);
                Console.WriteLine("Disciplina " + umaDisciplina.Nome + " Salvo.");
            }

            cursoRepository.Salvar(SecretariadoCurso);
            Console.WriteLine("Curso " + SecretariadoCurso.Nome + " Salvo." + System.Environment.NewLine);

            #endregion

            #region Disciplinas Agronegócio

            IDisciplina fundamentosDoAgro = Disciplina.CriarDisciplina("Fundamentos do Agronegócio", 40);

            IDisciplina tecnologiaDeProduçãoAnimal = Disciplina.CriarDisciplina("Tecnologia de Produção Animal", 40);

            IDisciplina saúdeSegurançaOcupacional = Disciplina.CriarDisciplina("Saúde Segurança Ocupacional", 40);

            IDisciplina comercioInternacional = Disciplina.CriarDisciplina("Comércio Internacional", 40);

            gradeAgro = new List<IDisciplina>();

            gradeAgro.Add(fundamentosDoAgro); countDisciplinas++;
            gradeAgro.Add(tecnologiaDeProduçãoAnimal); countDisciplinas++;
            gradeAgro.Add(saúdeSegurançaOcupacional); countDisciplinas++;
            gradeAgro.Add(comercioInternacional); countDisciplinas++;

            AGROCurso.AdicionarGradeCurricularCompleta(gradeAgro);

            foreach (var umaDisciplina in gradeAgro)
            {
                disciplinaRepository.Salvar(umaDisciplina);
                Console.WriteLine("Disciplina " + umaDisciplina.Nome + " Salvo.");
            }

            cursoRepository.Salvar(AGROCurso);
            Console.WriteLine("Curso " + AGROCurso.Nome + " Salvo." + System.Environment.NewLine);


            #endregion
        }

        private void LoadProfessores()
        {
            rodolfoProfessor = Professor.CriarProfessor("Rodolfo", "Callan Pereira", true);
            angélicaProfessora = Professor.CriarProfessor("Angélica", "Salles Cunha", false);
            pedroProfessor = Professor.CriarProfessor("Pedro", "Coimbra Oliveira", false);
            maraisaProfessora = Professor.CriarProfessor("Maraísa", "Sennes Castro", true);

            List<IProfessor> listaDeProfessores = new List<IProfessor>();

            listaDeProfessores.Add(rodolfoProfessor);
            listaDeProfessores.Add(angélicaProfessora);
            listaDeProfessores.Add(pedroProfessor);
            listaDeProfessores.Add(maraisaProfessora);

            foreach (var umProfessor in listaDeProfessores)
            {
                professorRepository.Salvar(umProfessor);
                Console.WriteLine("Professor " + umProfessor.Nome + " " + umProfessor.Sobrenome
                    + " Salvo.");
            }
        }

        private void LoadAlunos()
        {
            String alexRA = "1840481312001";
            alexAluno = Aluno.CriarAluno("Alex", "Nunes Santos", alexRA, "418757896", Período.Segundo, Turno.Manhã, AGROCurso.Id);
            alexAluno.DefinirEmail("jonathan.santos15a@gmail.com");

            String PatriciaRA = "1840481312002";
            PatriciaAluna = Aluno.CriarAluno("Patrícia", "Araújo Carvalho", PatriciaRA, "911225341", Período.Terceiro, Turno.Tarde, RHCurso.Id);
            PatriciaAluna.DefinirEmail("jonathan.santos15a@gmail.com");

            String CristianoRA = "1840481312003";
            CristianoAluno = Aluno.CriarAluno("Cristiano", "Neves Mendonça", CristianoRA, "42.9434121", Período.Primeiro, Turno.Noite, SecretariadoCurso.Id);
            CristianoAluno.DefinirEmail("jonathan.santos15a@gmail.com");

            String BeatrizRA = "1840481312004";
            BeatrizAluna = Aluno.CriarAluno("Beatriz", "Marília da Silva", BeatrizRA, "911225341", Período.Quinto, Turno.Noite, LogísticaCurso.Id);
            BeatrizAluna.DefinirEmail("jonathan.santos15a@gmail.com");

            alunos = new List<IAluno>();

            alunos.Add(alexAluno);
            alunos.Add(PatriciaAluna);
            alunos.Add(CristianoAluno);
            alunos.Add(BeatrizAluna);

            foreach(IAluno umAluno in alunos)
            {
                alunoRepository.Salvar(umAluno);
                Console.WriteLine("Aluno " + umAluno.Nome + " " + umAluno.Sobrenome + " Salvo.");
            }
        }
        
        private void LoadFuncionários()
        {
            carlosFuncionario = FuncionarioAcademico.CriarFuncionarioAcademico("Carlos", "Marques", Domínio.Entidades.Requerimento.StatusRequerimento.TipoDeResponsável.FuncionárioAcadêmico);
            geraldoFuncionario = FuncionarioAcademico.CriarFuncionarioAcademico("Geraldo", "Guedes", Domínio.Entidades.Requerimento.StatusRequerimento.TipoDeResponsável.Diretor);
            leilaFuncionaria = FuncionarioAcademico.CriarFuncionarioAcademico("Leia", "Pires", Domínio.Entidades.Requerimento.StatusRequerimento.TipoDeResponsável.Cordenador);

            List<IFuncionarioAcademico> listaDeFUncionarios = new List<IFuncionarioAcademico>();
            listaDeFUncionarios.Add(carlosFuncionario);
            listaDeFUncionarios.Add(geraldoFuncionario);
            listaDeFUncionarios.Add(leilaFuncionaria);

            listaDeResponsáveis.Add(carlosFuncionario);
            listaDeResponsáveis.Add(geraldoFuncionario);
            listaDeResponsáveis.Add(leilaFuncionaria);

            foreach (IFuncionarioAcademico umFuncionario in listaDeFUncionarios)
            {
                funcionarioRepository.Salvar(umFuncionario);
                Console.WriteLine("Funcionário  " + umFuncionario.Nome + " " + umFuncionario.Sobrenome + " Salvo.");
            }
        }

        private void LoadAcessos()
        {
            List<IAcesso> acessosASeremSalvos = new List<IAcesso>();

            carlosAcesso = Acesso.CriarAcesso(carlosFuncionario.Id, "carlos.func", "123", TipoDeAcesso.FuncionárioAcadêmico);
            geraldoAcesso = Acesso.CriarAcesso(geraldoFuncionario.Id, "geraldo.diretor", "123", TipoDeAcesso.Diretor);
            leiaAcesso = Acesso.CriarAcesso(leilaFuncionaria.Id, "leia.cordenador", "123", TipoDeAcesso.Cordenador);

            IAcesso Araujo = Acesso.CriarAcesso(leilaFuncionaria.Id, "araujo", "123", TipoDeAcesso.FuncionárioAcadêmico);
            IAcesso j = Acesso.CriarAcesso(leilaFuncionaria.Id, "jonathan", "123", TipoDeAcesso.FuncionárioAcadêmico);


            acessosASeremSalvos.Add(carlosAcesso);
            acessosASeremSalvos.Add(geraldoAcesso);
            acessosASeremSalvos.Add(leiaAcesso);
            acessosASeremSalvos.Add(Araujo);
            acessosASeremSalvos.Add(j);

            listaDeResponsáveisAcesso.Add(new ResponsavelAcesso(carlosAcesso.Id, TipoDeResponsável.FuncionárioAcadêmico));
            listaDeResponsáveisAcesso.Add(new ResponsavelAcesso(geraldoAcesso.Id, TipoDeResponsável.Diretor));
            listaDeResponsáveisAcesso.Add(new ResponsavelAcesso(leiaAcesso.Id, TipoDeResponsável.Cordenador));

            rodolfoAcesso = Acesso.CriarAcesso(rodolfoProfessor.Id, "rodolfo.prof", "123", TipoDeAcesso.Professor);
            angelicaAcesso = Acesso.CriarAcesso(rodolfoProfessor.Id, "angelica.prof", "123", TipoDeAcesso.Professor);
            maraisaAcesso = Acesso.CriarAcesso(rodolfoProfessor.Id, "maraisa.prof", "123", TipoDeAcesso.Professor);
            pedroAcesso = Acesso.CriarAcesso(rodolfoProfessor.Id, "pedro.prof", "123", TipoDeAcesso.Professor);

            acessosASeremSalvos.Add(rodolfoAcesso);
            acessosASeremSalvos.Add(angelicaAcesso);
            acessosASeremSalvos.Add(maraisaAcesso);
            acessosASeremSalvos.Add(pedroAcesso);

            alexAcesso = Acesso.CriarAcesso(alexAluno.Id, "1840481312001", "123", TipoDeAcesso.Aluno);
            patriciaAcesso = Acesso.CriarAcesso(PatriciaAluna.Id, "1840481312002", "123", TipoDeAcesso.Aluno);
            cristianoAcesso = Acesso.CriarAcesso(CristianoAluno.Id, "1840481312003", "123", TipoDeAcesso.Aluno);
            beatrizAcesso = Acesso.CriarAcesso(BeatrizAluna.Id, "1840481312004", "123", TipoDeAcesso.Aluno);

            acessosASeremSalvos.Add(alexAcesso);
            acessosASeremSalvos.Add(patriciaAcesso);
            acessosASeremSalvos.Add(cristianoAcesso);
            acessosASeremSalvos.Add(beatrizAcesso);

            foreach (var umAcesso in acessosASeremSalvos)
            {
                acessoRepository.Salvar(umAcesso);
                Console.WriteLine("Acesso " + umAcesso.Usuário  + " Salvo.");
            }
        }

        private void CreateRequerimentos(IAluno aluno, IDisciplina disciplina)
        {
            Guid id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.Trabalho,
                (int)TiposDeDocumentos.AtestadoDePeríodo, "Preciso comprovar que curso este período", false, true, false, true, false, true, Guid.Empty);

            requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.CompromissoMedico,
                (int)TiposDeDocumentos.AtestadoDeDisciplina, "Preciso comprovar que curso esta disciplina", true, false, true, true, false, true, disciplina.Id);

            requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.ComprovacaoDeMatricula,
                (int)TiposDeDocumentos.AtestadoDeFrequencia, "Preciso comprovar minha frequencia", false, false, true, false, true, true, Guid.Empty);

            requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.OutrosCursos,
                (int)TiposDeDocumentos.AtestadoDeMatrícula, "Preciso comprovar minha matrícula", true, true, true, false, false, false, Guid.Empty);

            requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                (int)TiposDeDocumentos.HistóricoEscolar, "Preciso do meu histórico escolar", false, false, false, false, false, false, Guid.Empty);
        }

        private void CreateTransferirRequerimentos(IAluno aluno, IDisciplina disciplina, IResponsável responsável)
        {
            
            Guid id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.CompromissoMedico,
                (int)TiposDeDocumentos.AtestadoDeDisciplina, "Preciso comprovar que curso esta disciplina", true, false, true, true, false, true, disciplina.Id);

            requerimentoService.TransferirResponsabilidade(id, responsável.Id, (int)responsável.TipoDeResponsavel, "Favor verificar o andamento da requisição");

            id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.ComprovacaoDeMatricula,
                (int)TiposDeDocumentos.AtestadoDeFrequencia, "Preciso comprovar minha frequencia", false, false, true, false, true, true, Guid.Empty);

            requerimentoService.TransferirResponsabilidade(id, responsável.Id, (int)responsável.TipoDeResponsavel, "Favor verificar o andamento da requisição");

            id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                (int)TiposDeDocumentos.HistóricoEscolar, "Preciso do meu histórico escolar", false, false, false, false, false, false, Guid.Empty);

            requerimentoService.TransferirResponsabilidade(id, responsável.Id, (int)responsável.TipoDeResponsavel, "Favor verificar o andamento da requisição");
        }

        private void CreateConcluirRequerimentos(IAluno aluno, IDisciplina disciplina, Guid idResponsavel, TipoDeResponsável tipoResponsavel)
        {
            Guid id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.Trabalho,
                (int)TiposDeDocumentos.AtestadoDePeríodo, "Preciso comprovar que curso este período", false, true, false, true, false, true, Guid.Empty);

            requerimentoService.Concluir(id, idResponsavel, (int)tipoResponsavel);

            id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.CompromissoMedico,
                (int)TiposDeDocumentos.AtestadoDeDisciplina, "Preciso comprovar que curso esta disciplina", true, false, true, true, false, true, disciplina.Id);

            requerimentoService.Concluir(id, idResponsavel, (int)tipoResponsavel);

            id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.OutrosCursos,
                (int)TiposDeDocumentos.AtestadoDeMatrícula, "Preciso comprovar minha matrícula", true, true, true, false, false, false, Guid.Empty);

            requerimentoService.Concluir(id, idResponsavel, (int)tipoResponsavel);

        }

        private void CreateCancelarRequerimentos(IAluno aluno, IDisciplina disciplina, Guid idResponsavel, TipoDeResponsável tipoResponsavel)
        {
            
            Guid id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.CompromissoMedico,
                (int)TiposDeDocumentos.AtestadoDeDisciplina, "Preciso comprovar que curso esta disciplina", true, false, true, true, false, true, disciplina.Id);

            requerimentoService.Cancelar(id, "Pq eu quis", idResponsavel, (int)tipoResponsavel);

            id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.ComprovacaoDeMatricula,
                (int)TiposDeDocumentos.AtestadoDeFrequencia, "Preciso comprovar minha frequencia", false, false, true, false, true, true, Guid.Empty);

            requerimentoService.Cancelar(id, "Pq eu quis", idResponsavel, (int)tipoResponsavel);

            id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.OutrosCursos,
                (int)TiposDeDocumentos.AtestadoDeMatrícula, "Preciso comprovar minha matrícula", true, true, true, false, false, false, Guid.Empty);

            requerimentoService.Cancelar(id, "Pq eu quis", idResponsavel, (int)tipoResponsavel);

            id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.Outros,
                (int)TiposDeDocumentos.HistóricoEscolar, "Preciso do meu histórico escolar", false, false, false, false, false, false, Guid.Empty);

            requerimentoService.Cancelar(id,"Pq eu quis", idResponsavel, (int)tipoResponsavel);

        }

        private void CreateIndeferirRequerimentos(IAluno aluno, IDisciplina disciplina, Guid idResponsavel, TipoDeResponsável tipoResponsavel)
        {
            Guid id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.Trabalho,
                (int)TiposDeDocumentos.AtestadoDePeríodo, "Preciso comprovar que curso este período", false, true, false, true, false, true, Guid.Empty);

            requerimentoService.Indeferir(id, "Não é possivel atender", idResponsavel, (int)tipoResponsavel);

            id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.CompromissoMedico,
                (int)TiposDeDocumentos.AtestadoDeDisciplina, "Preciso comprovar que curso esta disciplina", true, false, true, true, false, true, disciplina.Id);

            requerimentoService.Indeferir(id, "Não é possivel atender", idResponsavel, (int)tipoResponsavel);

            id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.ComprovacaoDeMatricula,
                (int)TiposDeDocumentos.AtestadoDeFrequencia, "Preciso comprovar minha frequencia", false, false, true, false, true, true, Guid.Empty);

            requerimentoService.Indeferir(id, "Não é possivel atender", idResponsavel, (int)tipoResponsavel);

            id = requerimentoService.CriarSolicitacoesDeDocumentos(aluno.Id, (int)TipoDeMotivosSolicitacaoDeDocumentos.OutrosCursos,
                (int)TiposDeDocumentos.AtestadoDeMatrícula, "Preciso comprovar minha matrícula", true, true, true, false, false, false, Guid.Empty);

            requerimentoService.Indeferir(id, "Não é possivel atender", idResponsavel, (int)tipoResponsavel);


        }



        protected class ResponsavelAcesso
        {
            public Guid Id { get; set; }

            public TipoDeResponsável Tipo { get; set; }

            public ResponsavelAcesso(Guid id, TipoDeResponsável tipo)
            {
                Id = id;
                Tipo = tipo;
            }
        }
    }
}
