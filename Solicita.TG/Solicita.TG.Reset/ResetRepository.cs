using Solicita.TG.Persistência.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Reset
{
    public class ResetRepository : BaseRepository, IResetRepository
    {
        public void ResetDatabase(String databaseName)
        {
            try
            {
                String SQL =

                    @"DROP database " + databaseName + @" ; 

                    CREATE database " + databaseName + @" ;

                    USE " + databaseName + @" ;

                    create table curso(
	                    id varchar(50),
                        nome varchar(50),
                        CONSTRAINT pk_curso_Id PRIMARY KEY (id)
                    );

                    CREATE table aluno(
	                    id varchar(50),
	                    nome varchar(50),
                        sobrenome varchar(50),
                        email varchar(50),
                        RA varchar(50),
	                    RG varchar(50),
                        curso varchar(50),
                        Periodo varchar(50),
                        turno varchar(50),
	                    CONSTRAINT pk_Aluno_Id PRIMARY KEY (id),
	                    CONSTRAINT UN_RA_ALUNO UNIQUE (RA), 
                        CONSTRAINT FOREIGN KEY fk_curso_idcurso (curso) REFERENCES curso(id)
                    );

                    CREATE table acesso(
	                    ID varchar(50),
                        IDENTIDADEASSOCIADA varchar(50),
	                    USUARIO varchar(50),
                        SENHA varchar(50),
                        TIPODEUSUARIO integer(10),
	                    CONSTRAINT pk_Acesso_Id PRIMARY KEY (id),
	                    CONSTRAINT UN_USUARIO_ACESSO UNIQUE (USUARIO)
                    );

                    CREATE table professor(
	                    id varchar(50),
	                    nome varchar(50),
	                    sobrenome varchar(50),
                        cordenador bit(1),
	                    CONSTRAINT pk_Professor_Id PRIMARY KEY (id)
                    );

                    CREATE table funcionarioAcademico(
	                    id varchar(50),
	                    nome varchar(50),
	                    sobrenome varchar(50),
                        tipoderesponsavel int,
	                    CONSTRAINT pk_FuncionarioAcademico_Id PRIMARY KEY (id)
                    );

                    create table disciplina(
	                    id varchar(50),
	                    nome varchar(50),
                        cargahoraria integer(10),
	                    CONSTRAINT pk_disciplina_Id PRIMARY KEY (id)
                    );

                    create table gradeCurricular
                    (
	                    id_curso varchar(50),
	                    id_disciplina varchar(50),
	                    CONSTRAINT fk_curso_gradecurricular FOREIGN KEY (id_curso) REFERENCES curso(id),
                        CONSTRAINT FOREIGN KEY fk_disciplina_gradecurricular (id_disciplina) REFERENCES disciplina(id)
                    );


                    create table tipodeRequerimento
                    (
	                    id  varchar(50),
                        CONSTRAINT id_treq_pk PRIMARY KEY (id)
                    );

					create table statusRequerimento
					(
						id varchar(50),
						id_requerimento varchar(50),
						dataDeEntrada varchar(250),
						dataDeSaida varchar(250),
						tipoDeStatus tinyint,
						idResponsavel varchar(50),
                        tipoResponsavel tinyint,
						observacao varchar(250),
	            CONSTRAINT pk_statusRequerimento_Id PRIMARY KEY (id)
					);
						  
					create table solicitacaoDeDocumentos
					(
						id varchar(50),
						id_requerimento varchar(50),
						tipoDeMotivo tinyint,
						tipoDeDocumento tinyint,
						motivoEspecificado varchar(250),
						informarSemestreAtual bit,
						informarCargaHoraria bit,
						informarAulaAosSabados bit,
						informarPrevisaoDeConclusao bit,
						informarDisciplina bit,
						informarPeriodo bit,
						id_disciplina varchar(50),
	            CONSTRAINT pk_statusRequerimento_Id PRIMARY KEY (id)
					);
						
                    create table requerimento
                    (
                    	id  varchar(50),
                        id_aluno varchar(50),
	                    tipo tinyint,
                        id_statusAtual varchar(50),
                        dataAbertura datetime,
                        dataPrevistaDeTermino datetime,
	                    CONSTRAINT pk_requerimento_Id PRIMARY KEY (id)
                    );

                    ";


                Execute(SQL);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
