# Status do MentorLab API .NET

## Baseline atual

```text
Versão: v0.2.0-students-api
Data de validação local: 2026-06-02
Branch validada: main
Status: validado localmente
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
