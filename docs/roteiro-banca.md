# Roteiro para banca técnica

## Apresentação curta

O MentorLab API .NET é uma API REST desenvolvida em ASP.NET Core Web API para acompanhar alunos em trilhas de aprendizado técnico.

O projeto foi pensado para demonstrar domínio de back-end .NET, C#, POO, APIs REST, banco de dados, Entity Framework Core, GitHub e boas práticas de mentoria educacional.

---

## Pitch de 1 minuto

Desenvolvi este projeto como uma API educacional para gestão de mentoria técnica. A aplicação permite cadastrar alunos, trilhas, módulos, exercícios, entregas e feedbacks do mentor.

A escolha desse domínio foi proposital, porque conecta desenvolvimento back-end com prática educacional. Com ele, consigo demonstrar API REST, C#, POO, Entity Framework Core, banco de dados, DTOs, services, controllers, Swagger, Git/GitHub e também explicar como usaria o sistema para acompanhar evolução técnica dos alunos.

---

## Conceitos que o projeto permite explicar

### API REST

Uma API REST expõe recursos por meio de endpoints HTTP. No projeto, recursos como alunos, trilhas e exercícios serão manipulados por verbos como GET, POST, PUT e DELETE.

### DTO

DTO é um contrato de entrada ou saída usado para evitar expor diretamente a entidade persistida. Isso melhora segurança, clareza e controle dos dados trafegados pela API.

### Controller

Controller recebe a requisição HTTP, chama a camada de serviço e retorna uma resposta adequada com status code correto.

### Service

Service concentra regra de negócio. Isso evita controller gordo e facilita teste, manutenção e evolução do sistema.

### Entity Framework Core

EF Core será usado como ORM para mapear entidades C# para tabelas do banco de dados, executar consultas e controlar migrations.

### Git/GitHub

O projeto usa GitHub como portfólio técnico e também como prática de versionamento. A evolução ideal é por branches, commits pequenos e pull requests.

---

## Perguntas prováveis e respostas-base

### O que é uma API REST?

É uma forma de expor recursos de um sistema usando HTTP. Cada recurso possui endpoints e operações bem definidas. Por exemplo, `/api/students` pode permitir consultar, cadastrar, alterar e desativar alunos.

### Por que usar DTO?

Para separar o contrato da API do modelo interno do banco. Isso evita expor campos indevidos, facilita validação e permite evoluir a API sem quebrar a persistência.

### Por que não colocar regra de negócio no controller?

Porque controller deve cuidar da entrada e saída HTTP. A regra de negócio em services melhora manutenção, teste e reaproveitamento.

### Por que SQLite no MVP?

Porque é simples, portátil e suficiente para demonstrar EF Core, migrations e persistência sem depender de servidor externo. Para produção, o projeto pode evoluir para SQL Server.

### Como você usaria esse projeto em aula?

Eu usaria como projeto guiado. Primeiro explicaria o domínio, depois criaria uma entidade simples, em seguida controller, DTO, service, persistência e testes. Cada etapa vira uma entrega incremental para o aluno.

---

## Mini-aula sugerida

Tema: Criando uma API REST para cadastro de alunos em ASP.NET Core.

Estrutura:

1. Contexto do problema.
2. O que é recurso em uma API.
3. Como desenhar endpoints.
4. Diferença entre entidade e DTO.
5. Controller recebendo requisição.
6. Service executando regra.
7. DbContext persistindo dados.
8. Teste pelo Swagger.
9. Erros comuns.
10. Exercício para o aluno.

---

## Fechamento

O diferencial do projeto é unir tecnologia e didática. Ele não é apenas um CRUD: é uma base para ensinar desenvolvimento back-end de forma prática, progressiva e conectada a problemas reais.
