# Plano de Aula — Aula Teste SENAI/SC LAB365

## 1. Identificação

| Item | Informação |
|---|---|
| Processo seletivo | 01645/2026 |
| Cargo | Mentor Educacional de TI — Desenvolvedor Back-End .NET |
| Tema | Construindo uma API REST com ASP.NET Core: Controller, DTO, Service, Dependency Injection e persistência com EF Core |
| Tempo da aula | 20 minutos |
| Questionamentos da banca | 10 minutos |
| Projeto de apoio | MentorLab API .NET |
| Stack | .NET 8, ASP.NET Core Web API, C#, EF Core, SQLite, Swagger, xUnit e GitHub Actions |

## 2. Tema

Construção de uma API REST com ASP.NET Core, demonstrando a separação de responsabilidades entre Controller, DTO, Service, Dependency Injection e persistência com Entity Framework Core.

A aula utilizará o projeto **MentorLab API .NET** como base prática, com foco principal no domínio `Students`.

## 3. Objetivo geral

Demonstrar, de forma prática e didática, como construir uma API REST organizada em ASP.NET Core, aplicando boas práticas de separação de camadas, contratos de entrada e saída, regras de negócio, injeção de dependência e persistência com Entity Framework Core.

## 4. Objetivos específicos

Ao final da aula, o participante deverá ser capaz de:

- compreender o papel de uma API REST;
- identificar a função do Controller em uma aplicação ASP.NET Core;
- diferenciar Entity e DTO;
- compreender por que regras de negócio devem ficar no Service;
- entender o papel da Dependency Injection no ASP.NET Core;
- relacionar DbContext, DbSet, migrations e persistência com EF Core;
- validar a API em execução por meio de endpoints HTTP.

## 5. Conteúdo programático

1. Conceito de API REST.
2. Estrutura de um projeto ASP.NET Core Web API.
3. Controllers e métodos HTTP.
4. DTOs como contratos de entrada e saída.
5. Services e regras de negócio.
6. Dependency Injection no `Program.cs`.
7. Entity Framework Core com SQLite.
8. Teste rápido dos endpoints.
9. Testes automatizados e CI como maturidade complementar.

## 6. Metodologia

A aula será conduzida em formato **expositivo-prático**, usando o fluxo:

```text
problema -> arquitetura -> código -> execução -> validação
```

A condução prioriza clareza operacional, evitando excesso de teoria e mantendo foco no fluxo real de uma API:

```text
Requisição HTTP -> Controller -> DTO -> Service -> EF Core -> SQLite -> Resposta JSON
```

## 7. Recursos didáticos

| Recurso | Uso na aula |
|---|---|
| Notebook com .NET 8 | Execução do projeto |
| VS Code ou editor equivalente | Leitura dos arquivos principais |
| Terminal Linux | Execução da API e comandos de validação |
| Swagger ou curl | Teste dos endpoints |
| GitHub | Evidência de versionamento, testes e CI |
| MentorLab API .NET | Base prática da aula |

## 8. Desenvolvimento da aula — 20 minutos

| Tempo | Bloco | Conteúdo | Estratégia |
|---:|---|---|---|
| 0:00–2:00 | Abertura | Contexto do problema e objetivo da API | Apresentar o que será construído e entendido |
| 2:00–4:00 | Arquitetura | Estrutura do projeto | Mostrar pastas: Controllers, DTOs, Services, Entities e Data |
| 4:00–6:30 | Entity e DTO | `Student`, `CreateStudentRequest`, `StudentResponse` | Explicar persistência versus contrato |
| 6:30–8:30 | EF Core | `MentorLabDbContext`, SQLite e migrations | Mostrar persistência e schema |
| 8:30–11:30 | Service | `IStudentService`, `StudentService` | Explicar regra de negócio |
| 11:30–14:30 | Controller | `StudentsController` | Mostrar endpoints REST |
| 14:30–16:00 | Dependency Injection | `Program.cs` | Explicar `AddScoped` |
| 16:00–18:30 | Demonstração | Testes com API em execução | Validar endpoints |
| 18:30–19:30 | Testes e CI | xUnit e GitHub Actions | Mostrar maturidade do projeto |
| 19:30–20:00 | Fechamento | Síntese técnica | Reforçar aprendizado |

## 9. Demonstração prática prevista

Endpoints previstos para demonstração:

```text
GET /api/status
GET /api/students
POST /api/students
GET /api/students
GET /api/learning-tracks
```

Fluxo narrado durante a demonstração:

```text
O cliente envia uma requisição HTTP.
O Controller recebe.
O DTO transporta os dados.
O Service aplica as regras.
O EF Core persiste no SQLite.
A API devolve uma resposta JSON.
```

## 10. Avaliação da aprendizagem

Como se trata de aula teste, a avaliação será feita por observação da banca, considerando:

| Critério | Evidência |
|---|---|
| Clareza didática | Explicação objetiva das camadas |
| Domínio técnico | Uso correto de ASP.NET Core, DI e EF Core |
| Organização do raciocínio | Sequência lógica entre conceito e prática |
| Aplicação prática | API executando e respondendo endpoints |
| Boas práticas | Separação entre Controller, DTO, Service e Entity |
| Maturidade técnica | Testes automatizados e CI no GitHub Actions |

## 11. Resultados esperados

Ao final da aula, espera-se que a banca observe que o candidato consegue:

- explicar arquitetura de API REST;
- demonstrar código real e funcional;
- separar responsabilidades corretamente;
- usar EF Core para persistência;
- aplicar Dependency Injection;
- relacionar teoria com prática;
- conduzir uma aula técnica dentro do tempo.

## 12. Fechamento previsto

Fala sugerida:

> Com essa estrutura, conseguimos construir uma API REST organizada, persistente e testável. O Controller cuida da entrada HTTP, os DTOs definem os contratos, o Service concentra as regras de negócio, a injeção de dependência reduz acoplamento e o EF Core resolve a persistência. Essa separação torna o projeto mais claro para aprender, manter e evoluir.

## 13. Diferencial técnico do projeto

Embora a aula foque no CRUD de `Students`, o projeto demonstra maturidade adicional com:

- relacionamento 1:N entre LearningTracks e Modules;
- soft delete;
- testes automatizados com xUnit;
- WebApplicationFactory;
- SQLite em memória nos testes;
- GitHub Actions;
- branch protection documentada;
- tag final `v1.0.0-portfolio-ready`.
