# DexianTest-back

Este projeto é uma API RESTful desenvolvida com ASP.NET Core 8.0 que fornece endpoints para gerenciamento de usuários, escolas e alunos, com autenticação JWT e armazenamento de dados no MongoDB.

## Tecnologias Utilizadas

- **ASP.NET Core 8.0**: Framework para desenvolvimento de aplicações web
- **MongoDB**: Banco de dados NoSQL utilizado para armazenamento de dados
- **JWT (JSON Web Token)**: Mecanismo de autenticação e autorização
- **Swagger/OpenAPI**: Documentação interativa da API
- **BCrypt.Net-Next**: Biblioteca para criptografia de senhas

## Estrutura do Projeto

O projeto segue uma arquitetura em camadas com separação clara de responsabilidades:

- **Controllers**: Responsáveis por receber as requisições HTTP e retornar as respostas
- **Services**: Implementam a lógica de negócio da aplicação
- **Interfaces**: Definem contratos para as implementações de serviços
- **Models**: Representam as entidades do domínio e DTOs (Data Transfer Objects)

## Configuração do Banco de Dados

O projeto utiliza MongoDB como banco de dados. As configurações de conexão estão no arquivo `appsettings.json`:

```json
"MongoDbConnection": {
  "ConnectionString": "mongodb+srv://...",
  "DatabaseName": "Storage"
}
```

## Autenticação e Segurança

O sistema implementa autenticação baseada em JWT (JSON Web Token) com as seguintes características:

- Tokens JWT são gerados no login e expiram após 60 minutos
- Senhas são armazenadas de forma segura utilizando BCrypt
- Todos os endpoints (exceto login) requerem autenticação
- Swagger UI configurado com suporte para autenticação JWT para facilitar testes

Configurações JWT no `appsettings.json`:

```json
"JwtSettings": {
  "Key": "YourSuperSecretKeyWithAtLeast32Characters",
  "Issuer": "DexianTest",
  "Audience": "DexianTestUsers",
  "ExpiryMinutes": 60
}
```

## APIs Disponíveis

### Autenticação

#### Login
- **Endpoint**: POST /auth/login
- **Descrição**: Autentica um usuário e retorna um token JWT
- **Corpo da Requisição**:
  ```json
  {
    "Nome": "string",
    "Password": "string"
  }
  ```
- **Resposta de Sucesso**:
  ```json
  {
    "token": "string",
    "user": {
      "id": "string",
      "name": "string",
      "codUser": 0
    }
  }
  ```
- **Autenticação**: Não requerida

### Usuários

#### Obter Todos os Usuários
- **Endpoint**: GET /user
- **Descrição**: Retorna a lista de todos os usuários cadastrados
- **Autenticação**: Requerida (JWT)

#### Criar Usuário
- **Endpoint**: POST /user
- **Descrição**: Cria um novo usuário
- **Corpo da Requisição**:
  ```json
  {
    "SNome": "string",
    "SSenha": "string",
    "ICodUsuario": 0
  }
  ```
- **Autenticação**: Requerida (JWT)

#### Atualizar Usuário
- **Endpoint**: PUT /user/{codUser}
- **Descrição**: Atualiza os dados de um usuário existente
- **Parâmetros de Rota**:
  - codUser: Código do usuário a ser atualizado
- **Corpo da Requisição**:
  ```json
  {
    "SNome": "string",
    "SSenha": "string",
    "ICodUsuario": 0
  }
  ```
- **Autenticação**: Requerida (JWT)

#### Excluir Usuário
- **Endpoint**: DELETE /user/{codUser}
- **Descrição**: Remove um usuário do sistema
- **Parâmetros de Rota**:
  - codUser: Código do usuário a ser removido
- **Autenticação**: Requerida (JWT)

### Escolas

#### Obter Todas as Escolas
- **Endpoint**: GET /school
- **Descrição**: Retorna a lista de todas as escolas cadastradas
- **Autenticação**: Requerida (JWT)

#### Criar Escola
- **Endpoint**: POST /school
- **Descrição**: Cria uma nova escola
- **Corpo da Requisição**:
  ```json
  {
    "SDescricao": "string",
    "ICodEscola": 0
  }
  ```
- **Autenticação**: Requerida (JWT)

#### Atualizar Escola
- **Endpoint**: PUT /school/{codEscola}
- **Descrição**: Atualiza os dados de uma escola existente
- **Parâmetros de Rota**:
  - codEscola: Código da escola a ser atualizada
- **Corpo da Requisição**:
  ```json
  {
    "SDescricao": "string",
    "ICodEscola": 0
  }
  ```
- **Autenticação**: Requerida (JWT)

#### Excluir Escola
- **Endpoint**: DELETE /school/{codEscola}
- **Descrição**: Remove uma escola do sistema
- **Parâmetros de Rota**:
  - codEscola: Código da escola a ser removida
- **Autenticação**: Requerida (JWT)

#### Buscar Escola por Descrição
- **Endpoint**: GET /school/find-by-desc?desc={desc}
- **Descrição**: Busca escolas que contenham a descrição informada
- **Parâmetros de Consulta**:
  - desc: Texto para busca na descrição da escola
- **Autenticação**: Requerida (JWT)

### Alunos

#### Obter Todos os Alunos
- **Endpoint**: GET /students
- **Descrição**: Retorna a lista de todos os alunos cadastrados
- **Autenticação**: Requerida (JWT)

#### Criar Aluno
- **Endpoint**: POST /students
- **Descrição**: Cria um novo aluno
- **Corpo da Requisição**:
  ```json
  {
    "SNome": "string",
    "ICodAluno": 0,
    "ICodEscola": 0
  }
  ```
- **Autenticação**: Requerida (JWT)

#### Atualizar Aluno
- **Endpoint**: PUT /students/{codAluno}
- **Descrição**: Atualiza os dados de um aluno existente
- **Parâmetros de Rota**:
  - codAluno: Código do aluno a ser atualizado
- **Corpo da Requisição**:
  ```json
  {
    "SNome": "string",
    "ICodAluno": 0,
    "ICodEscola": 0
  }
  ```
- **Autenticação**: Requerida (JWT)

#### Excluir Aluno
- **Endpoint**: DELETE /students/{codAluno}
- **Descrição**: Remove um aluno do sistema
- **Parâmetros de Rota**:
  - codAluno: Código do aluno a ser removido
- **Autenticação**: Requerida (JWT)

#### Buscar Aluno por Nome
- **Endpoint**: GET /students/find-by-name?name={name}
- **Descrição**: Busca alunos que contenham o nome informado
- **Parâmetros de Consulta**:
  - name: Texto para busca no nome do aluno
- **Autenticação**: Requerida (JWT)

## Executando o Projeto

### Pré-requisitos
- .NET 8.0 SDK
- Acesso a uma instância do MongoDB (local ou na nuvem)

### Passos para Execução
1. Clone o repositório
2. Configure a string de conexão do MongoDB no arquivo `appsettings.json`
3. Execute o comando `dotnet restore` para restaurar as dependências
4. Execute o comando `dotnet run` para iniciar a aplicação
5. Acesse a documentação Swagger em `https://localhost:5001/swagger` ou `http://localhost:5000/swagger`

## Testando a API com Swagger

1. Acesse a interface do Swagger
2. Execute o endpoint de login para obter um token JWT
3. Clique no botão "Authorize" no topo da página
4. Insira o token no formato: `Bearer {seu_token}`
5. Agora você pode testar todos os endpoints protegidos

## Segurança

- Todas as senhas são armazenadas de forma segura utilizando BCrypt
- A comunicação com a API deve ser feita através de HTTPS em ambiente de produção
- Tokens JWT são configurados com tempo de expiração para minimizar riscos
