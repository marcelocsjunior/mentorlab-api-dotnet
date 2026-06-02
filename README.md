# MentorLab API .NET

API educacional em ASP.NET Core Web API para demonstrar fundamentos de back-end .NET com uma arquitetura simples, didática e explicável.

Nesta sprint, o projeto entrega o primeiro CRUD real da aplicação: Students.

## Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI

## Como executar localmente

Pré-requisito:

- .NET SDK 8 ou superior
- `dotnet-ef` instalado para gerar e aplicar migrations

Restaurar dependências:

```bash
dotnet restore MentorLab.sln
```

Compilar:

```bash
dotnet build MentorLab.sln
```

Gerar migration:

```bash
dotnet ef migrations add InitialCreate \
  --project src/MentorLab.Api/MentorLab.Api.csproj \
  --output-dir Migrations
```

Aplicar migration no SQLite:

```bash
dotnet ef database update --project src/MentorLab.Api/MentorLab.Api.csproj
```

Executar a API:

```bash
dotnet run --project src/MentorLab.Api/MentorLab.Api.csproj --urls http://0.0.0.0:5080
```

Swagger:

```text
http://localhost:5080/swagger
```

## Endpoints

Status da API:

```http
GET /api/status
```

Students:

```http
GET    /api/students
GET    /api/students/{id}
POST   /api/students
PUT    /api/students/{id}
DELETE /api/students/{id}
```

Payload para criar aluno:

```json
{
  "fullName": "Ana Silva",
  "email": "ana.silva@example.com"
}
```

## Arquitetura da Sprint 2

O Controller recebe as requisições HTTP, chama o Service e devolve os status codes corretos.

Os DTOs definem os contratos de entrada e saída da API, sem expor diretamente a entidade persistida.

O Service concentra as regras de negócio, como validação de nome, validação de e-mail, prevenção de e-mail duplicado ativo e soft delete.

Dependency Injection registra o `IStudentService` e injeta a implementação no controller, reduzindo acoplamento.

O EF Core usa o `MentorLabDbContext` para mapear a entidade `Student` e persistir os dados no SQLite.

Fluxo:

```text
Cliente -> Controller -> Service -> DbContext -> SQLite -> Response
```

## Configuração de banco

O SQLite usa a connection string em `src/MentorLab.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=mentorlab.db"
  }
}
```

Arquivos locais de banco, como `mentorlab.db`, `*.db`, `*.sqlite`, `*.sqlite3`, não devem ser versionados.

## Status

```text
Status: Sprint 2 — Students API com EF Core
```

## Licença

Distribuído sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE).
