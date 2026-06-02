# Resumo Executivo — MentorLab API .NET

## 1. Visão geral

O **MentorLab API .NET** é uma API educacional construída em ASP.NET Core Web API para demonstrar, de forma prática, fundamentos de back-end .NET aplicados a um cenário didático de mentoria técnica.

O projeto foi preparado como portfólio técnico e base prática para a aula teste do Processo Seletivo 01645/2026 — Mentor Educacional de TI — Desenvolvedor Back-End .NET — SENAI/SC LAB365.

## 2. Tema técnico atendido

```text
Construindo uma API REST com ASP.NET Core:
Controller, DTO, Service, Dependency Injection e persistência com EF Core
```

## 3. Stack utilizada

- .NET 8
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- xUnit
- WebApplicationFactory
- GitHub Actions

## 4. Arquitetura didática

A solução foi organizada por responsabilidade:

```text
src/MentorLab.Api
├── Controllers
├── DTOs
├── Entities
├── Services
├── Data
└── Program.cs
```

Fluxo principal da API:

```text
Requisição HTTP
-> Controller
-> DTO
-> Service
-> EF Core / DbContext
-> SQLite
-> Resposta JSON
```

## 5. Funcionalidades implementadas

### Students

- CRUD de alunos.
- DTOs de criação, atualização e resposta.
- Service com regras de negócio.
- Normalização de e-mail.
- Bloqueio de e-mail duplicado ativo.
- Soft delete.

### LearningTracks e Modules

- Trilhas de aprendizado.
- Módulos vinculados a trilhas.
- Relacionamento 1:N.
- Ordenação por `DisplayOrder`.
- Soft delete em trilha e módulos ativos.

## 6. Persistência

A API utiliza EF Core com SQLite.

Itens técnicos relevantes:

- `MentorLabDbContext`.
- `DbSet<Student>`.
- `DbSet<LearningTrack>`.
- `DbSet<Module>`.
- Migrations versionadas.
- Índice de e-mail em Students.
- FK `Modules -> LearningTracks` com `DeleteBehavior.Restrict`.

## 7. Testes automatizados

O projeto possui testes com xUnit e WebApplicationFactory.

Cobertura principal:

- `StudentServiceTests`.
- `LearningTrackServiceTests`.
- `ModuleServiceTests`.
- `ApiEndpointTests`.

Estratégia:

- SQLite em memória.
- Testes isolados.
- Validação de services.
- Validação de endpoints principais.
- Regressão do CRUD Students.

Baseline validada:

```text
22 testes executados
0 falhas
```

## 8. CI e governança

O projeto possui workflow GitHub Actions:

```text
.NET CI
```

Valida automaticamente:

```text
dotnet restore MentorLab.sln
dotnet build MentorLab.sln --configuration Release --no-restore
dotnet test MentorLab.sln --configuration Release --no-build --verbosity normal
```

Também há documentação de Branch Protection e Required CI Checks para transformar o CI em quality gate da `main`.

## 9. Versão final de portfólio

Tag final publicada:

```text
v1.0.0-portfolio-ready
```

Essa tag congela o estado validado do projeto para apresentação e banca técnica.

## 10. Narrativa para banca

O projeto demonstra que o candidato consegue:

- construir API REST com ASP.NET Core;
- organizar camadas de forma didática;
- separar Controller, DTO, Service e Entity;
- aplicar Dependency Injection;
- usar EF Core para persistência;
- versionar schema com migrations;
- criar testes automatizados;
- validar qualidade com GitHub Actions;
- documentar governança técnica.

## 11. Recorte didático da aula

A aula de 20 minutos deve focar no domínio `Students`, pois ele cobre o tema central com clareza:

- Controller;
- DTO;
- Service;
- Dependency Injection;
- EF Core;
- SQLite;
- endpoints REST.

LearningTracks, Modules, testes e CI devem aparecer como diferenciais técnicos, sem roubar o foco da aula principal.

## 12. Frase síntese

> O MentorLab API .NET não é apenas um CRUD. É uma base didática para demonstrar como organizar uma API REST com separação de responsabilidades, persistência, testes e governança de qualidade.
