# MentorLab API .NET

Projeto educacional em **ASP.NET Core Web API** para demonstrar fundamentos de **back-end .NET**, **C#**, **POO**, **APIs REST**, **Entity Framework Core**, **banco de dados**, **Git/GitHub** e boas práticas de **mentoria técnica**.

Este repositório foi criado como projeto de estudo e portfólio técnico para consolidar conhecimento em desenvolvimento back-end com .NET, com foco em clareza didática, arquitetura simples e aplicabilidade prática.

---

## Objetivo

Construir uma API REST para gestão de mentoria técnica, permitindo acompanhar:

- alunos;
- trilhas de aprendizado;
- módulos;
- exercícios;
- entregas;
- feedbacks do mentor;
- evolução técnica dos alunos.

A proposta é treinar desenvolvimento back-end de forma aplicada, usando um domínio educacional diretamente conectado à prática de mentoria.

---

## Stack prevista

- .NET 8 ou superior
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQLite no MVP
- SQL Server em evolução futura
- Swagger / OpenAPI
- xUnit para testes
- GitHub Actions em fase posterior

---

## Escopo do MVP

### Entidades principais

- `Student`
- `LearningTrack`
- `Module`
- `Exercise`
- `Submission`
- `MentorFeedback`

### Funcionalidades iniciais

- CRUD de alunos
- CRUD de trilhas
- CRUD de módulos
- CRUD de exercícios
- Registro de entregas
- Registro de feedbacks
- Dashboard/resumo operacional via API

---

## Endpoints previstos

```http
GET    /api/students
GET    /api/students/{id}
POST   /api/students
PUT    /api/students/{id}
DELETE /api/students/{id}

GET    /api/tracks
POST   /api/tracks

GET    /api/modules
POST   /api/modules

GET    /api/exercises
POST   /api/exercises

POST   /api/submissions
GET    /api/submissions/student/{studentId}

POST   /api/feedbacks
GET    /api/dashboard/summary
```

---

## Arquitetura prevista

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

A arquitetura inicial será simples e didática, com separação clara entre:

| Camada | Responsabilidade |
|---|---|
| Controller | Entrada e saída HTTP |
| DTO | Contrato de entrada/saída da API |
| Service | Regras de negócio |
| Entity | Modelo persistido |
| DbContext | Acesso ao banco via EF Core |
| Tests | Validação de regras e comportamentos |

---

## Roadmap

### Fase 1 — Baseline

- README
- licença
- `.gitignore`
- documentação inicial
- definição de escopo

### Fase 2 — MVP técnico

- criação do projeto ASP.NET Core Web API
- configuração do Swagger
- SQLite + EF Core
- CRUD inicial de alunos
- migrations

### Fase 3 — Qualidade

- DTOs
- validações
- tratamento global de erro
- logs
- status codes adequados
- paginação e filtros
- testes unitários

### Fase 4 — Segurança e portfólio

- autenticação JWT
- perfis de acesso
- GitHub Actions
- documentação final
- roteiro de apresentação técnica

---

## Uso educacional

Este projeto também será usado como material de apoio para explicar conceitos como:

- o que é uma API REST;
- diferença entre entidade e DTO;
- separação entre controller, service e persistência;
- boas práticas de status code;
- versionamento com Git/GitHub;
- revisão de código;
- evolução técnica orientada por feedback.

---

## Status

```text
Status: Planejamento e baseline inicial
```

---

## Licença

Distribuído sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE).
