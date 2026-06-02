# Status do MentorLab API .NET

## Baseline atual

```text
Versão: v0.3.0-learning-tracks-modules
Data de validação local: 2026-06-02
Branch validada: main
Status: validado localmente
```

## Sprint 4 em validação

```text
Sprint: Sprint 4 — Automated Tests
Issue relacionada: #9 — Sprint 4 — Automated Tests com xUnit e WebApplicationFactory
Data de validação local: 2026-06-02
Branch validada: sprint-4-automated-tests
Status: validado localmente para Pull Request
```

A Sprint 4 foi rebaseada sobre `origin/main` em `2026-06-02` e não altera o comportamento funcional da API.

Comandos executados:

```bash
git fetch origin
git checkout sprint-4-automated-tests
git rebase origin/main
dotnet restore MentorLab.sln
dotnet build MentorLab.sln
dotnet test MentorLab.sln
```

Resultado registrado:

```text
Rebase OK
Restore OK
Build OK
0 Warning(s)
0 Error(s)
Test OK
22 testes executados
0 falhas
```

Escopo testado:

```text
StudentService tests      -> OK
LearningTrackService tests -> OK
ModuleService tests       -> OK
Endpoint tests            -> OK
Regressão Students        -> OK
SQLite em memória         -> OK
```

## Sprint 3 em validação

```text
Sprint: Sprint 3 — Learning Tracks e Modules
Data de validação local: 2026-06-02
Branch validada: sprint-3-learning-tracks-modules
Status: validado localmente para Pull Request
```

A Sprint 3 foi rebaseada sobre `origin/main` em `2026-06-02` e preserva a API de Students existente.

Comandos executados:

```bash
git fetch origin
git checkout sprint-3-learning-tracks-modules
git rebase origin/main
dotnet restore MentorLab.sln
dotnet build MentorLab.sln
dotnet ef database update --project src/MentorLab.Api/MentorLab.Api.csproj
```

Resultado registrado:

```text
Rebase OK
Restore OK
Build OK
0 Warning(s)
0 Error(s)
Database update OK
Swagger JSON revisado
```

Smoke tests HTTP executados:

```bash
curl -s http://localhost:5080/api/status
curl -s http://localhost:5080/api/students
curl -s http://localhost:5080/api/learning-tracks
curl -s -X POST http://localhost:5080/api/learning-tracks \
  -H "Content-Type: application/json" \
  -d '{"title":"Fundamentos de ASP.NET Core","description":"Trilha para construção de APIs REST com .NET."}'
curl -s http://localhost:5080/api/learning-tracks
curl -s -X POST http://localhost:5080/api/learning-tracks/1/modules \
  -H "Content-Type: application/json" \
  -d '{"title":"Controller, DTO e Service","description":"Separação de responsabilidades na API.","displayOrder":1}'
curl -s http://localhost:5080/api/learning-tracks/1/modules
curl -s http://localhost:5080/api/modules/1
```

Resultado esperado/observado:

```text
GET /api/status                         -> 200 OK
GET /api/students                       -> 200 OK
GET /api/learning-tracks                -> 200 OK
POST /api/learning-tracks               -> 201 Created
POST /api/learning-tracks/1/modules     -> 201 Created
GET /api/learning-tracks/1/modules      -> 200 OK
GET /api/modules/1                      -> 200 OK
```

## Validação local registrada

A baseline `v0.2.0-students-api` foi validada localmente em `2026-06-02` no ambiente de desenvolvimento.

Comandos executados:

```bash
git checkout main
git pull
git status
dotnet restore MentorLab.sln
dotnet build MentorLab.sln
dotnet ef database update --project src/MentorLab.Api/MentorLab.Api.csproj
```

Resultado registrado:

```text
Working tree clean
Restore OK
Build OK
0 Warning(s)
0 Error(s)
Database already up to date
```

Smoke test HTTP executado:

```bash
curl -s http://localhost:5080/api/status
curl -s http://localhost:5080/api/students
```

Resultado esperado/observado:

```text
GET /api/status   -> 200 OK
GET /api/students -> 200 OK
Students          -> []
```

## Escopo validado

- ASP.NET Core Web API em .NET 8.
- Swagger/OpenAPI configurado.
- Endpoint `GET /api/status` operacional.
- CRUD `Students` implementado.
- Controller, DTO, Service e Dependency Injection aplicados.
- EF Core com SQLite configurado.
- Migration inicial versionada.
- Banco SQLite local fora do versionamento.

## Narrativa de portfólio

Esta baseline demonstra o primeiro CRUD real do MentorLab API usando Controller para receber requisições HTTP, DTOs para definir contratos de entrada e saída, Service para concentrar regra de negócio, Dependency Injection para desacoplar dependências e EF Core com SQLite para persistência.

O projeto está em estado apresentável para portfólio técnico e pode ser usado como base prática para a aula teste da banca.

Na Sprint 3, a narrativa evolui para modelagem relacional: uma trilha de aprendizado possui vários módulos, cada módulo pertence a uma única trilha, e a API expõe esse relacionamento com endpoints REST aninhados e services separados por domínio.

Na Sprint 4, a narrativa evolui para confiabilidade: a API passa a ter testes automatizados de services e endpoints, com SQLite em memória para isolamento e `WebApplicationFactory` para regressão dos endpoints principais.
