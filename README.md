# MentorLab API .NET

[![.NET CI](https://github.com/marcelocsjunior/mentorlab-api-dotnet/actions/workflows/dotnet-ci.yml/badge.svg)](https://github.com/marcelocsjunior/mentorlab-api-dotnet/actions/workflows/dotnet-ci.yml)

API educacional em ASP.NET Core Web API para demonstrar fundamentos de back-end .NET com uma arquitetura simples, didática e explicável.

Nesta sprint, o projeto expande a API com Learning Tracks e Modules, mantendo o CRUD de Students preservado.

## Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- xUnit
- WebApplicationFactory

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

Rodar testes automatizados:

```bash
dotnet test MentorLab.sln
```

Gerar migration inicial:

```bash
dotnet ef migrations add InitialCreate \
  --project src/MentorLab.Api/MentorLab.Api.csproj \
  --output-dir Migrations
```

Gerar migration evolutiva da Sprint 3:

```bash
dotnet ef migrations add AddLearningTracksAndModules \
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

Learning Tracks:

```http
GET    /api/learning-tracks
GET    /api/learning-tracks/{id}
POST   /api/learning-tracks
PUT    /api/learning-tracks/{id}
DELETE /api/learning-tracks/{id}
GET    /api/learning-tracks/{trackId}/modules
POST   /api/learning-tracks/{trackId}/modules
```

Modules:

```http
GET    /api/modules/{id}
PUT    /api/modules/{id}
DELETE /api/modules/{id}
```

Payload para criar aluno:

```json
{
  "fullName": "Ana Silva",
  "email": "ana.silva@example.com"
}
```

Payload para criar trilha:

```json
{
  "title": "Fundamentos de ASP.NET Core",
  "description": "Trilha para construção de APIs REST com .NET."
}
```

Payload para criar módulo em uma trilha:

```json
{
  "title": "Controller, DTO e Service",
  "description": "Separação de responsabilidades na API.",
  "displayOrder": 1
}
```

## Arquitetura atual

O Controller recebe as requisições HTTP, chama o Service e devolve os status codes corretos.

Os DTOs definem os contratos de entrada e saída da API, sem expor diretamente a entidade persistida.

O Service concentra as regras de negócio, como validações de entrada, prevenção de duplicidade ativa quando aplicável, ordenação de módulos e soft delete.

Dependency Injection registra `IStudentService`, `ILearningTrackService` e `IModuleService`, injetando as implementações nos controllers.

O EF Core usa o `MentorLabDbContext` para mapear `Student`, `LearningTrack` e `Module` e persistir os dados no SQLite.

Na Sprint 3, `LearningTrack` possui relacionamento 1:N com `Module`. A FK `Module.LearningTrackId` usa `DeleteBehavior.Restrict`, e a propriedade `Module.Order` é mapeada para a coluna `DisplayOrder`.

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

## Testes automatizados

A Sprint 4 adiciona testes automatizados em `tests/MentorLab.Api.Tests`.

Os testes de services exercitam regras de negócio de `StudentService`, `LearningTrackService` e `ModuleService` usando SQLite em memória.

Os testes de endpoints usam `WebApplicationFactory<Program>` para subir a API em memória e validar os endpoints principais sem depender do banco local `mentorlab.db`.

Comando:

```bash
dotnet test MentorLab.sln
```

## CI / Quality Gate

A Sprint 5 adiciona um workflow de GitHub Actions em `.github/workflows/dotnet-ci.yml`.

Todo push para `main` e todo Pull Request direcionado para `main` executa automaticamente:

```bash
dotnet restore MentorLab.sln
dotnet build MentorLab.sln --configuration Release --no-restore
dotnet test MentorLab.sln --configuration Release --no-build --verbosity normal
```

Esse quality gate impede que mudanças sejam aprovadas sem validar restore, build e testes automatizados.

## Branch Protection / Quality Gate

A branch `main` deve ser protegida no GitHub para transformar o workflow `.NET CI` em um quality gate real.

Com a proteção ativa, PRs para `main` devem passar pelo check `Restore, build and test` antes do merge. A regra também deve bloquear force push e deleção da `main`.

## Status

```text
Status: Sprint 6 — Branch Protection e Required CI Checks
```

## Documentação por sprint

- [Sprint 2 — Students API](docs/sprint-2-students-api.md)
- [Sprint 3 — Learning Tracks e Modules](docs/sprint-3-learning-tracks-modules.md)
- [Sprint 4 — Automated Tests](docs/sprint-4-automated-tests.md)
- [Sprint 5 — CI Quality Gate](docs/sprint-5-ci-quality-gate.md)
- [Sprint 6 — Branch Protection](docs/sprint-6-branch-protection.md)

## Licença

Distribuído sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE).
