using Solicita.TG.Compartilhado;
using Solicita.TG.Domínio.Entidades.Acesso;
using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Curso;
using Solicita.TG.Domínio.Entidades.FuncionarioAcademico;
using Solicita.TG.Domínio.Entidades.Matricula;
using Solicita.TG.Domínio.Entidades.Motivo;
using Solicita.TG.Domínio.Entidades.Professor;
using Solicita.TG.Domínio.Entidades.Requerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento;
using Solicita.TG.Domínio.Factories.Requerimentos;
using Solicita.TG.Persistência.Repositories.AcessoRepository;
using Solicita.TG.Persistência.Repositories.CursoRepository;
using Solicita.TG.Persistência.Repositories.FuncionarioAcademicoRepository;
using Solicita.TG.Persistência.Repositories.ProfessorRepository;
using Solicita.TG.Persistência.Repositories.RepositórioAluno;
using Solicita.TG.Persistência.Repositories.RequerimentoRepository;
using Solicita.TG.Serviços.Mapper;
using Solicita.TG.Serviços.Requerimentos.Modelos;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Transactions;

namespace Solicita.TG.Serviços.Requerimentos
{

    public class RequerimentoService : IRequerimentoService
    {
        public AlunoRepository alunoRepository;
        public RequerimentoRepository requerimentoRepository;
        public CursoRepository cursoRepository;
        public AcessoRepository acessoRepository;

        public void Concluir(Guid id, Guid idResponsavel, Int32 tipoResponsavel)
        {
            requerimentoRepository = new RequerimentoRepository();
            acessoRepository = new AcessoRepository();
            alunoRepository = new AlunoRepository();

            try
            {
                var tipoDeRequerimento = requerimentoRepository.GetTipoDoRequerimento(id);

                IRequerimento requerimento = requerimentoRepository.GetByIDAndType(id, new RequerimentoModelMapper().ReturnEspecificEnumToType(tipoDeRequerimento));

                IAcesso acesso = acessoRepository.GetByID(idResponsavel);

                var responsável = GetResponsável(acesso.IdEntidadeAssociada, tipoResponsavel);

                requerimento.Finalizar(responsável);

                requerimentoRepository.AtualizarStatus(requerimento);

                IAluno aluno = alunoRepository.GetByID(requerimento.Aluno);

                MailSave.SaveMail(aluno.Email, "SOLICITA WEB - NOTIFICAÇÃO DE ACOMPANHAMENTO",
                    MailSave.GetMessage(aluno.Nome + " " + aluno.Sobrenome, aluno.Nome + " " + aluno.Sobrenome, requerimento.Id.ToString(), TypeMessage.Concluido));

            }
            catch (MainException e)
            {
                throw new FaultException(e.Error);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public void Indeferir(Guid id, String motivo, Guid idResponsavel, Int32 tipoResponsavel)
        {
            requerimentoRepository = new RequerimentoRepository();
            acessoRepository = new AcessoRepository();
            alunoRepository = new AlunoRepository();

            try
            {
                var tipoDeRequerimento = requerimentoRepository.GetTipoDoRequerimento(id);

                IRequerimento requerimento = requerimentoRepository.GetByIDAndType(id, new RequerimentoModelMapper().ReturnEspecificEnumToType(tipoDeRequerimento));

                IAcesso acesso = acessoRepository.GetByID(idResponsavel);

                var responsável = GetResponsável(acesso.IdEntidadeAssociada, tipoResponsavel);

                requerimento.Indeferir(motivo, responsável);

                requerimentoRepository.AtualizarStatus(requerimento);

                IAluno aluno = alunoRepository.GetByID(requerimento.Aluno);

                MailSave.SaveMail(aluno.Email, "SOLICITA WEB - NOTIFICAÇÃO DE ACOMPANHAMENTO",
                    MailSave.GetMessage(aluno.Nome + " " + aluno.Sobrenome, aluno.Nome + " " + aluno.Sobrenome, requerimento.Id.ToString(), TypeMessage.Indeferido));

            }
            catch (MainException e)
            {
                throw new FaultException(e.Error);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        private IResponsável GetResponsável(Guid idResponsavel, Int32 tipoResponsavel)
        {
            var tipoResponsavelConverted = (TipoDeResponsável)tipoResponsavel;

            FuncionarioAcademicoRepository funcionarioRepository = new FuncionarioAcademicoRepository();
            IFuncionarioAcademico funcionário;

            switch (tipoResponsavelConverted)
            {
                case TipoDeResponsável.Aluno:

                    AlunoRepository alunoRepository = new AlunoRepository();
                    IAluno aluno = alunoRepository.GetByID(idResponsavel);

                    return aluno;

                case TipoDeResponsável.Cordenador:

                    funcionário = funcionarioRepository.GetByID(idResponsavel);

                    if (funcionário == null)
                        return Solicita.TG.Domínio.Entidades.FuncionarioAcademico.FuncionarioAcademico.CriarFuncionarioAcademico(tipoResponsavelConverted);

                    return funcionário;

                case TipoDeResponsável.Diretor:

                    funcionário = funcionarioRepository.GetByID(idResponsavel);

                    if (funcionário == null)
                        return Solicita.TG.Domínio.Entidades.FuncionarioAcademico.FuncionarioAcademico.CriarFuncionarioAcademico(tipoResponsavelConverted);

                    return funcionário;

                case TipoDeResponsável.FuncionárioAcadêmico:

                    funcionário = funcionarioRepository.GetByID(idResponsavel);

                    if (funcionário == null)
                        return Solicita.TG.Domínio.Entidades.FuncionarioAcademico.FuncionarioAcademico.CriarFuncionarioAcademico(tipoResponsavelConverted);

                    return funcionário;

                case TipoDeResponsável.Professor:
                    ProfessorRepository professorRepository = new ProfessorRepository();
                    IProfessor professor = professorRepository.GetByID(idResponsavel);

                    return professor;
            }

            return null;
        }

        public void Cancelar(Guid id, String motivo, Guid idResponsavel, Int32 tipoResponsavel)
        {
            requerimentoRepository = new RequerimentoRepository();
            acessoRepository = new AcessoRepository();
            alunoRepository = new AlunoRepository();
            try
            {
                var tipoDeRequerimento = requerimentoRepository.GetTipoDoRequerimento(id);

                IRequerimento requerimento = requerimentoRepository.GetByIDAndType(id, new RequerimentoModelMapper().ReturnEspecificEnumToType(tipoDeRequerimento));

                IAcesso acesso = acessoRepository.GetByID(idResponsavel);

                var responsável = GetResponsável(acesso.IdEntidadeAssociada, tipoResponsavel);

                requerimento.Cancelar(motivo, responsável);

                requerimentoRepository.AtualizarStatus(requerimento);

                IAluno aluno = alunoRepository.GetByID(requerimento.Aluno);

                MailSave.SaveMail(aluno.Email, "SOLICITA WEB - NOTIFICAÇÃO DE ACOMPANHAMENTO",
                    MailSave.GetMessage(aluno.Nome + " " + aluno.Sobrenome, aluno.Nome + " " + aluno.Sobrenome, requerimento.Id.ToString(), TypeMessage.Cancelado));

            }
            catch (MainException e)
            {
                throw new FaultException(e.Error);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public List<MotivoModel> ListarMotivos()
        {
            List<MotivoModel> motivos = new List<MotivoModel>();

            foreach (var MotivoTemp in Enum.GetValues(typeof(Motivo)).Cast<Motivo>())
            {
                motivos.Add(new MotivoModel() { Text = Enumerations.GetEnumDescription(MotivoTemp), Value = (Int32)MotivoTemp });
            }

            return motivos;
        }

        public void TransferirResponsabilidade(Guid id, Guid idResponsavel, Int32 tipoResponsavel, String observação)
        {
            requerimentoRepository = new RequerimentoRepository();
            alunoRepository = new AlunoRepository();


            try
            {
                var tipoDeRequerimento = requerimentoRepository.GetTipoDoRequerimento(id);

                IRequerimento requerimento = requerimentoRepository.GetByIDAndType(id, new RequerimentoModelMapper().ReturnEspecificEnumToType(tipoDeRequerimento));

                IAluno aluno = alunoRepository.GetByID(requerimento.Aluno);

                requerimento.TransferirResponsabilidade(GetResponsável(idResponsavel, tipoResponsavel), observação);

                requerimentoRepository.AtualizarStatus(requerimento);

                MailSave.SaveMail(aluno.Email, "SOLICITA WEB - NOTIFICAÇÃO DE ACOMPANHAMENTO",
                    MailSave.GetMessage(aluno.Nome + " " + aluno.Sobrenome, aluno.Nome + " " + aluno.Sobrenome, requerimento.Id.ToString(), TypeMessage.TransferirResponsabilidade));

            }
            catch (MainException e)
            {
                throw new FaultException(e.Error);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public Guid CriarSolicitacoesDeDocumentos(Guid idAluno, Int32 tipoDoMotivo, Int32 tipoDeDocumento, String especificacaoDoMotivo,
            Boolean informarSemestreAtual, Boolean informarCargaHoraria, Boolean informarAulaAosSabados, Boolean informarPrevisaoDeConclusao,
            Boolean informarDisciplina, Boolean informarPeriodo, Guid disciplinaInformada)
        {
            try
            {

                requerimentoRepository = new RequerimentoRepository();
                alunoRepository = new AlunoRepository();

                IAluno aluno = alunoRepository.GetByID(idAluno);

                if (aluno == null)
                    throw new MainException("É necessário informar um aluno!");

                IRequerimento requerimentoCriado = RequerimentoFactory.CreateSolicitacaoDeDocumentos(aluno,
                    (TipoDeMotivosSolicitacaoDeDocumentos)tipoDoMotivo, (TiposDeDocumentos)tipoDeDocumento, especificacaoDoMotivo, informarSemestreAtual, informarCargaHoraria,
                    informarAulaAosSabados, informarPrevisaoDeConclusao, disciplinaInformada, informarDisciplina, informarPeriodo);

                if (String.IsNullOrEmpty(aluno.Email))
                    throw new MainException("O aluno deve possuir um email cadastrado!");

                requerimentoRepository.Salvar(requerimentoCriado);

                //TODO: Colocar Email no Alunoh
                try
                {
                    MailSave.SaveMail(aluno.Email, "SOLICITA WEB - NOTIFICAÇÃO DE ACOMPANHAMENTO",
                        MailSave.GetMessage(aluno.Nome + " " + aluno.Sobrenome, aluno.Nome + " " + aluno.Sobrenome, requerimentoCriado.Id.ToString(), TypeMessage.Novo));
                }
                catch (Exception e)
                {
                    throw new MainException(e.Message);
                }

                return requerimentoCriado.Id;
            }
            catch (MainException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new MainException(e.Message);
            }
        }

        public SolicitacaoDeDocumentosModel GetSolicitacaoDeDocumentos(Guid idRequerimento, String type)
        {
            requerimentoRepository = new RequerimentoRepository();

            IRequerimento requerimento = requerimentoRepository.GetByIDAndType(idRequerimento, new RequerimentoModelMapper().ReturnEspecificType(type));

            return CriarModeloCompleto(requerimento) as SolicitacaoDeDocumentosModel;
        }

        public List<RequerimentoSimplesModel> List(String RA, String nome, Guid curso, Int32 periodo, String type)
        {
            List<RequerimentoSimplesModel> model = new List<RequerimentoSimplesModel>();

            requerimentoRepository = new RequerimentoRepository();

            var requerimentoMapper = new RequerimentoModelMapper();

            Guid alunoId = Guid.Empty;

            if(!String.IsNullOrEmpty(RA))
            {
                alunoRepository = new AlunoRepository();
                IAluno aluno = alunoRepository.GetByRA(RA);
                alunoId = aluno.Id;
            }

            List<IRequerimento> requerimentos = requerimentoRepository.ListAllByFilters(requerimentoMapper.ReturnEspecificType(type), requerimentoMapper.ReturnEspecificTypeEnum(type), alunoId);

            foreach (var umRequerimento in requerimentos)
            {
                model.Add(CriarModeloSimples(umRequerimento));
            }

            return model;
        }

        public List<RequerimentoSimplesModel> ListJáAtendidas(String RA, String nome, Guid curso, Int32 periodo, String type)
        {
            List<RequerimentoSimplesModel> model = new List<RequerimentoSimplesModel>();

            requerimentoRepository = new RequerimentoRepository();

            var requerimentoMapper = new RequerimentoModelMapper();

            Guid alunoId = Guid.Empty;

            if (!String.IsNullOrEmpty(RA))
            {
                alunoRepository = new AlunoRepository();
                IAluno aluno = alunoRepository.GetByRA(RA);
                alunoId = aluno.Id;
            }

            List<IRequerimento> requerimentos = requerimentoRepository.ListAllByFiltersJáAtendidas(requerimentoMapper.ReturnEspecificType(type), requerimentoMapper.ReturnEspecificTypeEnum(type), alunoId);

            foreach (var umRequerimento in requerimentos.Where(x => x.StatusAtual.StatusType.Equals(TipoDeStatus.Concluido)))
            {
                model.Add(CriarModeloSimples(umRequerimento));
            }

            return model;
        }


        public List<Requerimentos.Modelos.TiposRequerimentoModel> ListarTipos()
        {

            List<Requerimentos.Modelos.TiposRequerimentoModel> modelos = new List<Modelos.TiposRequerimentoModel>();

            Array values = Enum.GetValues(typeof(TipoRequerimento));

            foreach (TipoRequerimento val in values)
            {
                Requerimentos.Modelos.TiposRequerimentoModel modelo = new Modelos.TiposRequerimentoModel();

                modelo.Tipo = Enumerations.GetEnumDescription(val);
                modelo.Identificador = ((Int32)val).ToString();

                modelos.Add(modelo);
            }

            return modelos;
        }

        public RequerimentoModel CriarModeloCompleto(IRequerimento requerimento)
        {

            alunoRepository = new AlunoRepository();
            cursoRepository = new CursoRepository();

            IRequerimentoModelMapper requerimentoMapper = new RequerimentoModelMapper();

            IAluno aluno = alunoRepository.GetByID(requerimento.Aluno);
            ICurso curso = cursoRepository.GetByID(aluno.Matrícula.Curso);

            Modelos.RequerimentoModel modelo = requerimentoMapper.GetEspecificAttributes(requerimento);

            modelo.Aluno.Nome = aluno.Nome;
            modelo.Aluno.Sobrenome = aluno.Sobrenome;
            modelo.Aluno.Matricula.Periodo = Enumerations.GetEnumDescription(aluno.Matrícula.Período);
            modelo.Aluno.Matricula.Turno = Enumerations.GetEnumDescription(aluno.Matrícula.Turno);
            modelo.Aluno.Matricula.RA = aluno.Matrícula.RA;
            modelo.Aluno.Matricula.AnoIngresso = "Precisa colocar no Aluno";
            modelo.Aluno.Matricula.Curso = curso.Nome;
            modelo.Aluno.Matricula.CargaHorária = curso.CargaHorária.ToString();
            modelo.Aluno.RG = aluno.RG;
            modelo.DataAbertura = requerimento.dataAbertura.ToShortDateString();
            modelo.DataPrevistaTermino = requerimento.dataPrevistaTérmino.ToShortDateString();
            modelo.Id = requerimento.Id;

            foreach (var umStatus in requerimento.HistóricoDeStatus.OrderByDescending(x => x.DataEntrada))
            {
                var statusModel = CriarModeloStatus(umStatus);
                modelo.HistoricoDeStatus.Add(statusModel);

                if (umStatus.Id == requerimento.StatusAtual.Id)
                    modelo.StatusAtual = statusModel;
            }

            return modelo;
        }

        private StatusModel CriarModeloStatus(IStatus status)
        {
            StatusModel statusModel = new StatusModel();
            statusModel.DataEntrada = status.DataEntrada.ToString("g");

            if (status.DataSaida.Equals(DateTime.MinValue) && !status.StatusType.Equals(TipoDeStatus.Cancelado) && !status.StatusType.Equals(TipoDeStatus.Concluido) && !status.StatusType.Equals(TipoDeStatus.Indeferido))
                statusModel.DataSaida = "Em Andamento";
            else if(status.DataSaida.Equals(DateTime.MinValue) && (status.StatusType.Equals(TipoDeStatus.Cancelado) || status.StatusType.Equals(TipoDeStatus.Concluido) || status.StatusType.Equals(TipoDeStatus.Indeferido)))
                statusModel.DataSaida = status.DataEntrada.ToString("g");
            else
                statusModel.DataSaida = status.DataSaida.ToString("g");


            statusModel.Id = status.Id;
            statusModel.Observação = status.Observação;
            statusModel.Responsável = GetModeloResponsavel(status.IdResponsável, status.TipoDeResponsável);
            statusModel.StatusNome = Enumerations.GetEnumDescription(status.StatusType);

            return statusModel;
        }

        private RequerimentoSimplesModel CriarModeloSimples(IRequerimento requerimento)
        {
            Modelos.RequerimentoSimplesModel modelo = new RequerimentoSimplesModel();

            alunoRepository = new AlunoRepository();

            IAluno aluno = alunoRepository.GetByID(requerimento.Aluno);

            modelo.Id = requerimento.Id;
            modelo.Aluno = aluno.Nome + " " + aluno.Sobrenome;
            modelo.DataAbertura = requerimento.dataAbertura.ToShortDateString();

            if (requerimento.HistóricoDeStatus.Any(x => x.StatusType.Equals(TipoDeStatus.Concluido)))
            {
                List<IStatus> viaciadoEmLinq = requerimento.HistóricoDeStatus.Where(x => x.StatusType.Equals(TipoDeStatus.Concluido)).ToList();

                IStatus status = viaciadoEmLinq.First();

                modelo.DataTermino = status.DataEntrada.ToShortDateString();
            }
            else
            {
                modelo.DataTermino = requerimento.dataPrevistaTérmino.ToShortDateString();
            }
            
            modelo.Responsavel = GetModeloResponsavel(requerimento.StatusAtual.IdResponsável, requerimento.StatusAtual.TipoDeResponsável);
            modelo.Status = Enumerations.GetEnumDescription(requerimento.StatusAtual.StatusType);

            return modelo;
        }

        private String GetModeloResponsavel(Guid idResponsavel, TipoDeResponsável tipoDeResponsavel)
        {
            FuncionarioAcademicoRepository funcionarioRepository = new FuncionarioAcademicoRepository();
            IFuncionarioAcademico funcionário;

            switch (tipoDeResponsavel)
            {
                case TipoDeResponsável.Aluno:

                    AlunoRepository alunoRepository = new AlunoRepository();
                    IAluno aluno = alunoRepository.GetByID(idResponsavel);

                    return aluno.Nome + " " + aluno.Sobrenome + " / " + Enumerations.GetEnumDescription(tipoDeResponsavel);

                case TipoDeResponsável.Cordenador:

                    if (idResponsavel == Guid.Empty)
                        return Enumerations.GetEnumDescription(tipoDeResponsavel);

                    funcionário = funcionarioRepository.GetByID(idResponsavel);

                    return funcionário.Nome + " " + funcionário.Sobrenome + " / " + Enumerations.GetEnumDescription(tipoDeResponsavel);

                case TipoDeResponsável.Diretor:

                    if (idResponsavel == Guid.Empty)
                        return Enumerations.GetEnumDescription(tipoDeResponsavel);

                    funcionário = funcionarioRepository.GetByID(idResponsavel);

                    return funcionário.Nome + " " + funcionário.Sobrenome + " / " + Enumerations.GetEnumDescription(tipoDeResponsavel);

                case TipoDeResponsável.FuncionárioAcadêmico:

                    if (idResponsavel == Guid.Empty)
                        return Enumerations.GetEnumDescription(tipoDeResponsavel);

                    funcionário = funcionarioRepository.GetByID(idResponsavel);

                    return funcionário.Nome + " " + funcionário.Sobrenome + " / " + Enumerations.GetEnumDescription(tipoDeResponsavel);

                case TipoDeResponsável.Professor:
                    ProfessorRepository professorRepository = new ProfessorRepository();
                    IProfessor professor = professorRepository.GetByID(idResponsavel);

                    return professor.Nome + " " + professor.Sobrenome + " / " + Enumerations.GetEnumDescription(tipoDeResponsavel); 
            }

            return null;
        }

        public List<SolicitacaoDeDocumentosModel> ListarSolicitacoesDeDocumentos()
        {
            requerimentoRepository = new RequerimentoRepository();

            List<IRequerimento> requerimentos =
                requerimentoRepository.ListAll().Where(x => x.Tipo.Equals(TipoRequerimento.SolicitaçãoDeDocumentos)).ToList();

            List<SolicitacaoDeDocumentosModel> modelo = new List<SolicitacaoDeDocumentosModel>();

            return modelo;
        }

        public List<RequerimentoSimplesModel> ListarDeclaracoesDeProva()
        {
            requerimentoRepository = new RequerimentoRepository();

            List<IRequerimento> requerimentos =
                requerimentoRepository.ListAll().Where(x => x.Tipo.Equals(TipoRequerimento.DeclaraçãoDeProva)).ToList();

            List<RequerimentoSimplesModel> modelo = new List<RequerimentoSimplesModel>();

            foreach (var umReq in requerimentos)
            {
                modelo.Add(CriarModeloSimples(umReq));
            }

            return modelo;
        }

        public List<RequerimentoSimplesModel> ListarSolicitacoesDePasse()
        {
            requerimentoRepository = new RequerimentoRepository();

            List<IRequerimento> requerimentos =
                requerimentoRepository.ListAll().Where(x => x.Tipo.Equals(TipoRequerimento.SolicitaçãoDePasseEscolar)).ToList();

            List<RequerimentoSimplesModel> modelo = new List<RequerimentoSimplesModel>();

            foreach (var umReq in requerimentos)
            {
                modelo.Add(CriarModeloSimples(umReq));
            }

            return modelo;
        }

        public List<RequerimentoSimplesModel> ListarReaproveitamentos()
        {
            requerimentoRepository = new RequerimentoRepository();

            List<IRequerimento> requerimentos =
                requerimentoRepository.ListAll().Where(x => x.Tipo.Equals(TipoRequerimento.SolicitaçãoREaproveitamento)).ToList();

            List<RequerimentoSimplesModel> modelo = new List<RequerimentoSimplesModel>();

            foreach (var umReq in requerimentos)
            {
                modelo.Add(CriarModeloSimples(umReq));
            }

            return modelo;
        }

        public List<RequerimentoSimplesModel> ListarTrancamentosDeCurso()
        {
            requerimentoRepository = new RequerimentoRepository();

            List<IRequerimento> requerimentos =
                requerimentoRepository.ListAll().Where(x => x.Tipo.Equals(TipoRequerimento.TrancamentoDeCurso)).ToList();

            List<RequerimentoSimplesModel> modelo = new List<RequerimentoSimplesModel>();

            foreach (var umReq in requerimentos)
            {
                modelo.Add(CriarModeloSimples(umReq));
            }

            return modelo;
        }

        public List<RequerimentoSimplesModel> ListarTrancamentosDeMatricula()
        {
            requerimentoRepository = new RequerimentoRepository();

            List<IRequerimento> requerimentos =
                requerimentoRepository.ListAll().Where(x => x.Tipo.Equals(TipoRequerimento.TrancamentoDeMateria)).ToList();

            List<RequerimentoSimplesModel> modelo = new List<RequerimentoSimplesModel>();

            foreach (var umReq in requerimentos)
            {
                modelo.Add(CriarModeloSimples(umReq));
            }

            return modelo;
        }

        public List<TipoDeStatusModel> ListarTipoDeStatus()
        {
            List<TipoDeStatusModel> modelos = new List<TipoDeStatusModel>();

            Array values = Enum.GetValues(typeof(TipoDeStatus));

            foreach (TipoDeStatus val in values)
            {
                TipoDeStatusModel modelo = new TipoDeStatusModel();

                modelo.Value = Enumerations.GetEnumDescription(val);
                modelo.Id = ((Int32)val).ToString();

                modelos.Add(modelo);
            }

            return modelos;
        }


    }
}
