# Sprint 2 — Students API

## 1. O que é Controller

Controller é a camada que recebe requisições HTTP, interpreta rota, verbo e payload, chama a regra de negócio e retorna uma resposta HTTP com status code adequado.

No MentorLab, o `StudentsController` expõe os endpoints `GET`, `POST`, `PUT` e `DELETE` de `/api/students`.

## 2. O que é DTO

DTO significa Data Transfer Object. Ele representa o contrato de entrada ou saída da API.

Na Sprint 2, `CreateStudentRequest` e `UpdateStudentRequest` recebem dados do cliente. `StudentResponse` define o formato devolvido pela API.

## 3. O que é Service

Service é a camada que concentra regras de negócio.

No MentorLab, o `StudentService` valida nome, valida e-mail, impede e-mail duplicado de aluno ativo, cria alunos, atualiza dados e faz soft delete.

## 4. O que é Dependency Injection

Dependency Injection é a técnica de registrar dependências no contêiner da aplicação e recebê-las por construtor.

O controller recebe `IStudentService` sem precisar criar manualmente um `StudentService`. Isso reduz acoplamento e deixa o código mais organizado.

## 5. O que é EF Core

EF Core é o ORM do .NET. Ele permite trabalhar com banco de dados usando classes C# e LINQ, sem escrever SQL manualmente para as operações comuns.

Na Sprint 2, o EF Core persiste a entidade `Student` no SQLite.

## 6. O que é DbContext

DbContext é a classe que representa a sessão de acesso ao banco de dados.

No MentorLab, o `MentorLabDbContext` expõe `DbSet<Student> Students` e configura chave primária, campos obrigatórios, tamanhos máximos e índice de e-mail.

## 7. O que é SQLite

SQLite é um banco de dados leve, baseado em arquivo, útil para aprendizado, protótipos e aplicações pequenas.

Nesta sprint, o banco local é criado a partir da connection string `Data Source=mentorlab.db`.

## 8. O que é soft delete

Soft delete é uma exclusão lógica. O registro continua no banco, mas deixa de aparecer nas consultas padrão.

No MentorLab, ao excluir um aluno, `IsActive` vira `false` e `UpdatedAt` recebe a data e hora da exclusão.

## 9. Fluxo

```text
Cliente -> Controller -> Service -> DbContext -> SQLite -> Response
```

O cliente chama a API. O Controller recebe a requisição. O Service executa as regras de negócio. O DbContext acessa o SQLite usando EF Core. A resposta volta para o cliente com o status code correto.

## 10. Como explicar isso em uma aula de 20 minutos

1. Apresente o problema: criar um CRUD de alunos com persistência real.
2. Mostre a entidade `Student` e explique que ela representa a tabela no banco.
3. Mostre os DTOs e explique por que entrada e saída da API não precisam ser iguais à entidade.
4. Mostre o Controller e destaque rotas, verbos HTTP e status codes.
5. Mostre o Service e explique onde ficam as regras de negócio.
6. Mostre o `MentorLabDbContext` e a configuração do EF Core.
7. Mostre o registro de DI no `Program.cs`.
8. Rode a API, abra o Swagger e execute um POST.
9. Execute GET, PUT e DELETE.
10. Explique que o DELETE é soft delete e que o GET por ID retorna 404 depois da exclusão.

## 11. Como defender esta sprint na banca

“Nesta sprint implementei o primeiro CRUD real do MentorLab API. Usei Controller para receber requisições HTTP, DTOs para definir contratos de entrada e saída, Service para concentrar regra de negócio, Dependency Injection para desacoplar dependências e EF Core com SQLite para persistência. A ideia foi construir uma API simples, didática e explicável para estudantes iniciantes, exatamente no formato de uma aula prática de back-end .NET.”

Para defender tecnicamente, destaque que a regra de negócio não fica no Controller, que o banco é acessado pelo DbContext, que a API retorna status codes coerentes e que a exclusão usa soft delete para preservar histórico.
