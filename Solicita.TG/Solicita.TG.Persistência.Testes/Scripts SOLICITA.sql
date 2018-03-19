-- CRIA O BANCO 
CREATE database solicita;

-- USA O BANCO
USE SOLICITA;

-- CRIA TABELA DE ALUNO
CREATE table aluno(
	id varchar(50),
	nome varchar(50),
    sobrenome varchar(50),
    RA varchar(50),
	RG varchar(50),
    Periodo varchar(50),
    turno varchar(50),
	CONSTRAINT pk_Aluno_Id PRIMARY KEY (id),
	CONSTRAINT UN_RA_ALUNO UNIQUE (RA)
);

-- CRIA TABELA DE ACESSO
CREATE table acesso(
	ID varchar(50),
	USUARIO varchar(50),
    SENHA varchar(50),
    TIPODEUSUARIO integer(10),
	CONSTRAINT pk_Acesso_Id PRIMARY KEY (id),
	CONSTRAINT UN_USUARIO_ACESSO UNIQUE (USUARIO)
);

-- CRIA TABELA DE PROFESSOR
CREATE table professor(
	id varchar(50),
	nome varchar(50),
	sobrenome varchar(50),
    cordenador bit(1),
	CONSTRAINT pk_Professor_Id PRIMARY KEY (id)
);

-- CRIA TABELA DE FUNCIONARIO ACADEMICO
CREATE table funcionarioAcademico(
	id varchar(50),
	nome varchar(50),
	sobrenome varchar(50),
	CONSTRAINT pk_FuncionarioAcademico_Id PRIMARY KEY (id)
);

-- CRIA TABELA DE DISCIPLINA

create table disciplina(
	id varchar(50),
	nome varchar(50),
    cargahoraria integer(10),
	CONSTRAINT pk_disciplina_Id PRIMARY KEY (id)
);

-- CRIA TABELA DE CURSO

create table curso(
	id varchar(50),
    nome varchar(50),
    CONSTRAINT pk_curso_Id PRIMARY KEY (id)
);

-- CRIA GRADE CURRICULAR DO CURSO -> um curso possui N disciplinas E uma disciplina pode estar na grade de varios cursos

create table gradeCurricular
(
	id_curso varchar(50),
	id_disciplina varchar(50),
	CONSTRAINT fk_curso_gradecurricular FOREIGN KEY (id_curso) REFERENCES curso(id),
    CONSTRAINT FOREIGN KEY fk_disciplina_gradecurricular (id_disciplina) REFERENCES disciplina(id)
);


create table statusRequerimento
(
	id  varchar(50),
    CONSTRAINT id_treq_pk PRIMARY KEY (id)
);

create table requerimento
(
	id  varchar(50),
	aluno varchar(50),
	tipo int,
	dataAbertura varchar(50),
	dataPrevistaDeTermino varchar(50),
	datadeAbertura varchar(50),
    CONSTRAINT id_req_pk PRIMARY KEY (id) -- ,
    CONSTRAINT fk_tipoderequerimento FOREIGN KEY (id_tipo) REFERENCES tipodeRequerimento(id)
);




