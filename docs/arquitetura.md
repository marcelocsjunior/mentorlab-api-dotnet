# Arquitetura — MentorLab API .NET

## Visão geral

O MentorLab API .NET será uma API REST educacional construída em ASP.NET Core Web API para gestão de mentoria técnica.

O objetivo arquitetural é manter o projeto simples, didático e defensável em banca técnica, sem excesso de abstração.

---

## Princípios

- Separação clara de responsabilidades.
- Código legível e explicável.
- Regras de negócio fora dos controllers.
- DTOs para entrada e saída da API.
- Persistência via Entity Framework Core.
- Banco SQLite no MVP.
- Evolução futura para SQL Server.
- Testes automatizados em regras críticas.

---

## Camadas previstas

| Camada | Responsabilidade |
|---|---|
| Controllers | Receber requisições HTTP e retornar respostas adequadas |
| DTOs | Definir contratos de entrada e saída |
| Services | Concentrar regras de negócio e orquestração |
| Entities | Representar modelos persistidos no banco |
| Data | Configuração do DbContext e acesso ao banco |
| Validators | Validações de entrada e regras simples |
| Tests | Validar comportamento esperado |

---

## Estrutura prevista

```text
src/
  MentorLab.Api/
    Controllers/
    Data/
    DTOs/
    Entities/
    Services/
    Validators/
    Program.cs
    appsettings.json

tests/
  MentorLab.Tests/

docs/
  arquitetura.md
  roteiro-banca.md
  exemplos-requisicoes.md
```

---

## Modelo de domínio inicial

### Student

Representa um aluno acompanhado pelo mentor.

Campos previstos:

- Id
- FullName
- Email
- IsActive
- CreatedAt

### LearningTrack

Representa uma trilha de aprendizado.

Campos previstos:

- Id
- Name
- Description
- IsActive

### Module

Representa um módulo dentro de uma trilha.

Campos previstos:

- Id
- LearningTrackId
- Title
- Description
- Order

### Exercise

Representa uma atividade técnica.

Campos previstos:

- Id
- ModuleId
- Title
- Description
- Difficulty

### Submission

Representa uma entrega feita por aluno.

Campos previstos:

- Id
- StudentId
- ExerciseId
- RepositoryUrl
- Notes
- SubmittedAt

### MentorFeedback

Representa o feedback técnico do mentor sobre uma entrega.

Campos previstos:

- Id
- SubmissionId
- Score
- Comment
- CreatedAt

---

## Decisão inicial

O projeto começa com arquitetura simples por camadas. Clean Architecture completa fica fora do MVP para evitar overengineering.

A prioridade é demonstrar domínio prático de API REST, C#, POO, EF Core, banco, GitHub e didática técnica.
