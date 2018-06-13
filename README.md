# Agendamento
Projeto para realizar o Agendamento de paciente.

# Tecnologias utilizadas
- ASP.NET Core 2.0
- Entity Framework
- Framework CSS Bulma (https://bulma.io)
- jQuery
- Docker (Opcional - necessário para subir SQL Server em Linux/Mac)

# Banco de dados - SQL Server
1. Criar base de dados
```sql
CREATE DATABASE agendamento;
```
2. Usar a base criada
```sql
USE agendamento;
```
3. Criar tabelas
```sql
CREATE TABLE Usuarios (
	UsuarioID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Login varchar(255),
	Senha varchar(255)
);

CREATE TABLE Pacientes (
	PacienteID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Nome varchar(255)
);

CREATE TABLE Medicos (
	MedicoID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Nome varchar(255)
);

CREATE TABLE Agendamentos (
	AgendamentoID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Data datetime,
	PacienteID int NULL,
	MedicoID int NULL
);
```
4. Inserir dados
```sql
INSERT INTO Pacientes (Nome) VALUES ('Antônio Azevedo Costa');
INSERT INTO Pacientes (Nome) VALUES ('Miguel Azevedo Barros');
INSERT INTO Pacientes (Nome) VALUES ('Kauã Ferreira Cavalcanti');

INSERT INTO Medicos (Nome) VALUES ('John A. Miller');
INSERT INTO Medicos (Nome) VALUES ('Jennifer S. Purdie');
INSERT INTO Medicos (Nome) VALUES ('Sophia Cardoso Carvalho');
INSERT INTO Medicos (Nome) VALUES ('Brenda Ribeiro Santos');

INSERT INTO `Agendamentos` VALUES (1,'2018-06-12 00:00:00',1,4);
INSERT INTO `Agendamentos` VALUES (2,'2018-06-13 00:00:00',2,1);
INSERT INTO `Agendamentos` VALUES (3,'2018-06-14 00:00:00',3,3);

INSERT Usuarios (Login, Senha) VALUES ('admin', 'admin');
```

# Usuário/senha para logar no sistema
- Usuário: admin
- Senha: admin

# Configurar conexão com o banco de dados - appsettings.json
Default (host: localhost, porta: 1433, nome do bd: agendamento, usuário do bd: SA, senha do bd: SENHA)
```
  "ConnectionStrings": {
    "BaseAgendamento": "Server=tcp:localhost,1433;Initial Catalog=agendamento;Persist Security Info=True;User Id=SA;Password=SENHA;"
  },
 ```

# Possíveis melhorias
- Paginação nas listagens de pacientes, médicos, agendamentos e usuários
- Busca utilizando os dados das tabelas nas listagens de pacientes, médicos, agendamentos e usuários
- Verificação de usuário logado nas páginas internas
- Criptografar senha
- Utilizar algum serviço em Nuvem, como o Azure, para a realização de login
- Testes unitários

# Autor
Leandro Peres Gonçalves