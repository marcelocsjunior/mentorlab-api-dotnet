# Sprint 3 — Learning Tracks e Modules

## Objetivo

A Sprint 3 adiciona o domínio de trilhas de aprendizado e módulos ao MentorLab API .NET.

O objetivo técnico é demonstrar relacionamento 1:N com ASP.NET Core Web API, EF Core, SQLite, Controller, DTO, Service, Dependency Injection e migration versionada, preservando o CRUD de Students já validado na Sprint 2.

## Modelo de domínio

`LearningTrack` representa uma trilha de aprendizado.

`Module` representa um módulo dentro de uma trilha.

Relacionamento:

```text
LearningTrack 1 -> N Module
```

Cada `Module` possui `LearningTrackId` e pertence a uma única trilha. Uma `LearningTrack` possui uma coleção de `Modules`.

No EF Core, o relacionamento usa `DeleteBehavior.Restrict`. Isso evita exclusão física em cascata e combina com a estratégia de soft delete do projeto.

A entity `Module` usa a propriedade `Order`, mas o banco persiste essa informação na coluna `DisplayOrder`. O DTO também expõe `displayOrder`, que é mais claro para consumidores da API.

## Endpoints criados

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

## DTOs

Learning Tracks:

```text
DTOs/LearningTracks/CreateLearningTrackRequest.cs
DTOs/LearningTracks/UpdateLearningTrackRequest.cs
DTOs/LearningTracks/LearningTrackResponse.cs
DTOs/LearningTracks/LearningTrackWithModulesResponse.cs
```

Modules:

```text
DTOs/Modules/CreateModuleRequest.cs
DTOs/Modules/UpdateModuleRequest.cs
DTOs/Modules/ModuleResponse.cs
```

Os DTOs separam o contrato público da API das entities persistidas pelo EF Core.

## Services

Learning Tracks:

```text
ILearningTrackService
LearningTrackService
```

Modules:

```text
IModuleService
ModuleService
```

Os services concentram validações, consultas, criação, atualização e soft delete. Controllers ficam responsáveis por rotas, payloads e status codes HTTP.

## Dependency Injection

O `Program.cs` registra os services:

```csharp
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ILearningTrackService, LearningTrackService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
```

Com isso, os controllers recebem interfaces por construtor e não instanciam services diretamente.

## EF Core e migration

O `MentorLabDbContext` expõe:

```csharp
public DbSet<Student> Students => Set<Student>();
public DbSet<LearningTrack> LearningTracks => Set<LearningTrack>();
public DbSet<Module> Modules => Set<Module>();
```

A migration evolutiva da Sprint 3 é:

```text
20260602093000_AddLearningTracksAndModules
```

Ela cria as tabelas `LearningTracks` e `Modules`, a FK `Modules.LearningTrackId`, o índice `IX_Modules_LearningTrackId` e a coluna `DisplayOrder`.

## Soft delete

O projeto usa exclusão lógica com `IsActive` e `UpdatedAt`.

Ao excluir uma trilha, a trilha e seus módulos ativos são marcados como inativos. Ao excluir um módulo, apenas o módulo é marcado como inativo.

As consultas públicas retornam somente registros ativos.

## Validação local executada

Ambiente:

```text
Data: 2026-06-02
Branch: sprint-3-learning-tracks-modules
API local: http://0.0.0.0:5080
```

Comandos:

```bash
dotnet restore MentorLab.sln
dotnet build MentorLab.sln
dotnet ef database update --project src/MentorLab.Api/MentorLab.Api.csproj
dotnet run --project src/MentorLab.Api/MentorLab.Api.csproj --urls http://0.0.0.0:5080
```

Resultados:

```text
Restore OK
Build OK
0 Warning(s)
0 Error(s)
Database update OK
Swagger JSON revisado
Smoke tests OK
```

## Smoke tests

```bash
curl -s http://localhost:5080/api/status
curl -s http://localhost:5080/api/students
curl -s http://localhost:5080/api/learning-tracks
```

Criar trilha:

```bash
curl -s -X POST http://localhost:5080/api/learning-tracks \
  -H "Content-Type: application/json" \
  -d '{"title":"Fundamentos de ASP.NET Core","description":"Trilha para construção de APIs REST com .NET."}'
```

Listar trilhas:

```bash
curl -s http://localhost:5080/api/learning-tracks
```

Criar módulo:

```bash
curl -s -X POST http://localhost:5080/api/learning-tracks/1/modules \
  -H "Content-Type: application/json" \
  -d '{"title":"Controller, DTO e Service","description":"Separação de responsabilidades na API.","displayOrder":1}'
```

Listar módulos da trilha:

```bash
curl -s http://localhost:5080/api/learning-tracks/1/modules
```

Consultar módulo individual:

```bash
curl -s http://localhost:5080/api/modules/1
```

## Narrativa para banca técnica

“Nesta sprint eu evoluí o MentorLab API de um CRUD isolado para um domínio relacional. Mantive a Students API preservada e adicionei Learning Tracks e Modules com relacionamento 1:N. Usei DTOs para contratos de entrada e saída, controllers para rotas e status codes, services para regra de negócio e EF Core com SQLite para persistência. A migration é evolutiva, sem alterar a migration inicial, e o relacionamento usa `DeleteBehavior.Restrict` porque o projeto trabalha com soft delete. A trilha pode ser excluída logicamente junto com seus módulos ativos, preservando histórico no banco.”

Para demonstrar em aula, o fluxo principal é criar uma trilha, listar trilhas, criar um módulo dentro da trilha, listar os módulos da trilha e consultar o módulo individualmente.
