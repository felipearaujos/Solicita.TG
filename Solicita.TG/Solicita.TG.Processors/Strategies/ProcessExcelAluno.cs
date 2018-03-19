using Solicita.TG.Domínio.Entidades;
using Solicita.TG.Domínio.Entidades.Acesso;
using Solicita.TG.Domínio.Entidades.Aluno;
using Solicita.TG.Domínio.Entidades.Curso;
using Solicita.TG.Domínio.Entidades.Matricula;
using Solicita.TG.Persistência.Repositories;
using Solicita.TG.Persistência.Repositories.AcessoRepository;
using Solicita.TG.Persistência.Repositories.CursoRepository;
using Solicita.TG.Persistência.Repositories.RepositórioAluno;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Processors.Strategies
{
 
    public class ProcessExcelAluno : IStrategy
    {
        private String Sheet = "Aluno";

        public void Processar(String connection)
        {
            AlunoRepository alunoRepository = new AlunoRepository();
            CursoRepository cursoRepository = new CursoRepository();
            AcessoRepository acessoRepository = new AcessoRepository();

            OleDbConnection conexao =
                new OleDbConnection(connection);

            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [" + Sheet + "$]", conexao);
            DataSet ds = new DataSet();

            try
            {
                conexao.Open();
                adapter.Fill(ds);

                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    String nome = linha["nome"].ToString();
                    String sobrenome = linha["sobrenome"].ToString();
                    String ra = linha["ra"].ToString();
                    String rg = linha["rg"].ToString();
                    String turno = linha["turno"].ToString();
                    Int32 Periodo = Convert.ToInt32(linha["periodo"].ToString());
                    String NomeDoCurso = linha["curso"].ToString();
                    String email = linha["email"].ToString();

                    Int32 turnoEnum = 0;

                    if (turno.ToUpper().Equals("MANHÃ") || turno.ToUpper().Equals("MANHA"))
                    {
                        turnoEnum = (Int32)Turno.Manhã;
                    }
                    else if (turno.ToUpper().Equals("TARDE"))
                    {
                        turnoEnum = (Int32)Turno.Tarde;
                    }
                    else if (turno.ToUpper().Equals("NOITE"))
                    {
                        turnoEnum = (Int32)Turno.Noite;
                    }

                    ICurso curso = cursoRepository.ListAll().Where(x => x.Nome.ToUpper().Equals(NomeDoCurso.ToUpper())).First();

                    IAluno aluno = Solicita.TG.Domínio.Entidades.Aluno.Aluno.CriarAluno(nome, sobrenome, ra, rg, (Período)Periodo, (Turno)turnoEnum, curso.Id);
                    aluno.DefinirEmail(email);
                    
                    try
                    {
                        alunoRepository.Salvar(aluno);
                    }
                    catch (Exception ex)
                    {
 
                    }

                    IAcesso acessoDoAluno = Solicita.TG.Domínio.Entidades.Acesso.Acesso.CriarAcesso(aluno.Id, ra, ra, TipoDeAcesso.Aluno);

                    try
                    {
                        acessoRepository.Salvar(acessoDoAluno);
                    }
                    catch (Exception ex)
                    {

                    }

                    String Message =  MailSave.GetMessage(aluno.Nome + " " + aluno.Sobrenome,aluno.Matrícula.RA, aluno.Matrícula.RA, TypeMessage.AcessoCriado);

                    MailSave.SaveMail(aluno.Email, "SOLICITA WEB - INFORMAÇÕES DE ACESSO", Message);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
