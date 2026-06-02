# Sprint 4 — Automated Tests

## Objetivo

A Sprint 4 adiciona testes automatizados ao MentorLab API .NET sem alterar o comportamento funcional da API.

O objetivo técnico é validar regras de negócio e endpoints principais com xUnit, `WebApplicationFactory` e SQLite em memória, preservando Students, LearningTracks e Modules.

## xUnit

xUnit é o framework de testes usado no projeto `tests/MentorLab.Api.Tests`.

Ele executa testes unitários e de integração com métodos marcados por `[Fact]`. Nesta sprint, os testes validam criação, atualização, soft delete, ordenação, duplicidade e regressão de endpoints.

## WebApplicationFactory

`WebApplicationFactory<Program>` permite subir a aplicação ASP.NET Core em memória durante os testes.

Isso permite testar endpoints HTTP reais sem iniciar manualmente a API com `dotnet run`.

Para habilitar o acesso ao `Program` gerado por top-level statements, o projeto principal declara:

```csharp
public partial class Program { }
```

Essa alteração não muda o comportamento da API.

## SQLite em memória

Os testes usam SQLite em memória com conexão aberta durante o ciclo do teste.

Essa abordagem evita dependência do banco local `mentorlab.db`, mantém os testes determinísticos e preserva o mapeamento relacional real do EF Core, incluindo chaves, índices e foreign keys.

## Testes de services

`StudentServiceTests` valida:

- criação de aluno válido
- normalização de e-mail para lowercase
- bloqueio de e-mail duplicado ativo
- atualização de aluno existente
- soft delete
- ocultação de aluno inativo em `GetAll` e `GetById`

`LearningTrackServiceTests` valida:

- criação de trilha válida
- bloqueio de título duplicado ativo
- retorno de trilha com módulos ativos ordenados
- soft delete da trilha e dos módulos ativos

`ModuleServiceTests` valida:

- criação de módulo em trilha ativa
- retorno `null` ao criar módulo para trilha inexistente
- validação de `DisplayOrder` maior que zero
- atualização de módulo
- soft delete
- listagem de módulos ativos por trilha ordenados por `DisplayOrder`

## Testes de endpoints

`ApiEndpointTests` usa `WebApplicationFactory` para validar:

```http
GET  /api/status
GET  /api/students
POST /api/students
GET  /api/learning-tracks
POST /api/learning-tracks
POST /api/learning-tracks/{trackId}/modules
GET  /api/learning-tracks/{trackId}/modules
GET  /api/modules/{id}
```

Esses testes funcionam como smoke tests automatizados e regressão dos endpoints principais.

## Regressão de Students

Students continua coberto por testes de service e endpoint.

A suíte garante que o CRUD de Students preserve criação, atualização, normalização de e-mail, bloqueio de duplicidade ativa, soft delete e consultas apenas de alunos ativos.

## Validação local executada

Ambiente:

```text
Data: 2026-06-02
Branch: sprint-4-automated-tests
Baseline: v0.3.0-learning-tracks-modules
```

Comandos:

```bash
dotnet restore MentorLab.sln
dotnet build MentorLab.sln
dotnet test MentorLab.sln
```

Resultados:

```text
Restore OK
Build OK
0 Warning(s)
0 Error(s)
Test OK
22 testes executados
0 falhas
```

## Narrativa para banca técnica

“Nesta sprint eu adicionei uma camada de segurança técnica ao MentorLab API. A API já tinha Students, LearningTracks e Modules funcionando; agora ela passa a ter testes automatizados para proteger esse comportamento. Usei xUnit para escrever os testes, SQLite em memória para evitar dependência do banco local e `WebApplicationFactory` para testar endpoints HTTP reais em memória. Isso permite validar regras de negócio nos services e também garantir que os principais endpoints continuem respondendo corretamente após mudanças futuras.”

Para demonstrar em aula, o fluxo principal é rodar `dotnet test MentorLab.sln`, abrir os testes de services para explicar regras de negócio e abrir os testes de endpoints para mostrar a API sendo exercitada sem subir servidor manualmente.
