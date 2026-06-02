# Roteiro de Apresentação — 20 minutos

## Preparação antes da banca

Deixar aberto:

```text
VS Code:
- README.md
- Program.cs
- StudentsController.cs
- StudentService.cs
- IStudentService.cs
- Student.cs
- CreateStudentRequest.cs
- StudentResponse.cs
- MentorLabDbContext.cs

Terminal 1:
- API rodando em http://localhost:5080

Terminal 2:
- comandos curl prontos

Navegador:
- GitHub do projeto
- GitHub Actions verde
```

Comando para subir a API:

```bash
cd ~/mentorlab-api-dotnet
dotnet run --project src/MentorLab.Api/MentorLab.Api.csproj --urls http://0.0.0.0:5080
```

---

## 0:00 — 2:00 | Abertura e objetivo

### Tela

README ou estrutura do projeto no VS Code.

### Fala

> Bom dia. Nesta aula prática, vou apresentar a construção de uma API REST com ASP.NET Core, usando uma estrutura simples, didática e evolutiva.
>
> O foco será entender o papel de cada camada: Controller, DTO, Service, Dependency Injection e persistência com Entity Framework Core.
>
> A ideia não é apenas criar endpoints que funcionam, mas organizar a API de forma que ela seja fácil de manter, testar e evoluir.

### Ponto de impacto

```text
API funcionando é o mínimo. API organizada é o que sustenta evolução.
```

---

## 2:00 — 4:00 | Visão da arquitetura

### Tela

Mostrar a árvore do projeto:

```text
Controllers
DTOs
Entities
Services
Data
Program.cs
```

### Fala

> A estrutura do projeto foi separada por responsabilidade.
>
> Em Controllers ficam os pontos de entrada HTTP. Em DTOs ficam os contratos de entrada e saída. Em Entities ficam os modelos persistidos. Em Services ficam as regras de negócio. Em Data fica o DbContext, que faz a ponte com o banco usando EF Core.
>
> Essa separação evita que o Controller vire uma classe gigante misturando HTTP, regra de negócio e persistência.

---

## 4:00 — 6:30 | Entity e DTO

### Tela

Abrir:

```text
Student.cs
CreateStudentRequest.cs
StudentResponse.cs
```

### Fala

> Aqui temos a entidade Student. Ela representa o modelo que será persistido no banco.
>
> Ela possui Id, FullName, Email, IsActive, CreatedAt e UpdatedAt.
>
> Mas quando o cliente da API vai criar um aluno, ele não precisa enviar todos esses campos. Por isso usamos DTOs.
>
> O CreateStudentRequest recebe apenas os dados necessários para criação: nome completo e e-mail. Já o StudentResponse define o formato de resposta que a API devolve para o cliente.

### Frase-chave

```text
Entity é modelo de persistência. DTO é contrato de comunicação.
```

---

## 6:30 — 8:30 | EF Core e DbContext

### Tela

Abrir:

```text
MentorLabDbContext.cs
appsettings.json
Migrations
```

### Fala

> O Entity Framework Core entra como camada de acesso a dados.
>
> O MentorLabDbContext representa a sessão de comunicação com o banco. Nele declaramos os DbSets, que representam as tabelas, e também configuramos regras do modelo.
>
> Neste exemplo, temos Students, LearningTracks e Modules.
>
> A API usa SQLite para persistência local, o que facilita a demonstração e mantém o ambiente leve.
>
> As migrations versionam a evolução do schema do banco. Então, quando o modelo muda, conseguimos refletir isso no banco de forma controlada.

---

## 8:30 — 11:30 | Service e regra de negócio

### Tela

Abrir:

```text
IStudentService.cs
StudentService.cs
```

### Fala

> Agora entramos na camada de Service.
>
> O Controller não deve concentrar regra de negócio. Ele deve receber a requisição, chamar o serviço e devolver a resposta HTTP.
>
> No StudentService ficam regras como validar nome, normalizar e-mail, impedir duplicidade de e-mail ativo e aplicar soft delete.
>
> Por exemplo, ao criar um aluno, o serviço verifica se o nome e o e-mail são válidos, transforma o e-mail para lowercase e impede cadastro duplicado.

### Frase-chave

```text
O Controller orquestra HTTP. O Service decide regra de negócio.
```

---

## 11:30 — 14:30 | Controller e endpoints REST

### Tela

Abrir:

```text
StudentsController.cs
```

### Fala

> O StudentsController é a camada de entrada HTTP.
>
> Ele expõe endpoints REST para listar, buscar, criar, atualizar e remover alunos.
>
> Aqui temos métodos HTTP bem definidos: GET para consulta, POST para criação, PUT para atualização e DELETE para remoção lógica.
>
> Também temos respostas HTTP apropriadas, como 200 OK, 201 Created, 204 No Content e 404 Not Found.

### Endpoints

```text
GET    /api/students
GET    /api/students/{id}
POST   /api/students
PUT    /api/students/{id}
DELETE /api/students/{id}
```

---

## 14:30 — 16:00 | Dependency Injection

### Tela

Abrir:

```text
Program.cs
```

### Fala

> No Program.cs configuramos os serviços da aplicação.
>
> Aqui registramos o DbContext com SQLite e também registramos o StudentService na injeção de dependência.
>
> A linha AddScoped informa que, a cada escopo de requisição, quando alguém pedir IStudentService, o ASP.NET Core deve entregar uma instância de StudentService.
>
> Isso permite que o Controller dependa da abstração IStudentService, e não diretamente da implementação concreta.

### Linha para destacar

```csharp
builder.Services.AddScoped<IStudentService, StudentService>();
```

---

## 16:00 — 18:30 | Demonstração prática

### Tela

Terminal 2.

### Fala

> Agora vou validar rapidamente a API em execução.
>
> A ideia é mostrar o fluxo completo: requisição HTTP, Controller, DTO, Service, EF Core, SQLite e resposta JSON.

### Comandos

```bash
curl -s http://localhost:5080/api/status
```

```bash
curl -s http://localhost:5080/api/students
```

```bash
curl -s -X POST http://localhost:5080/api/students \
  -H "Content-Type: application/json" \
  -d '{"fullName":"Aluno Demo SENAI","email":"aluno.demo.senai@example.com"}'
```

```bash
curl -s http://localhost:5080/api/students
```

### Fala durante o retorno

> Aqui vemos a API recebendo dados em JSON, aplicando regra de negócio, persistindo no banco e retornando uma resposta estruturada.
>
> Esse é o ciclo básico de uma API REST bem organizada.

---

## 18:30 — 19:30 | Testes e CI

### Tela

Terminal ou GitHub Actions.

### Fala

> Além da implementação, o projeto possui testes automatizados com xUnit e WebApplicationFactory.
>
> Os testes cobrem Services e endpoints principais, usando SQLite em memória.
>
> Também existe um workflow no GitHub Actions que executa restore, build e test automaticamente a cada pull request para a main.

### Frase estratégica

```text
Teste automatizado transforma confiança em evidência.
```

---

## 19:30 — 20:00 | Fechamento

### Fala final

> Com essa estrutura, conseguimos construir uma API REST organizada, persistente e testável.
>
> O Controller recebe a requisição HTTP. Os DTOs definem contratos de entrada e saída. O Service concentra regra de negócio. A injeção de dependência reduz acoplamento. E o EF Core resolve a persistência com controle de schema via migrations.
>
> Essa separação deixa o projeto mais claro para aprender, manter e evoluir.

---

## Plano B

### Se a API não responder

```bash
dotnet test MentorLab.sln
```

Fala:

> Como o foco da aula é arquitetura e fluxo, se a execução local apresentar falha, sigo pela validação já registrada: build, testes automatizados e GitHub Actions executados com sucesso.

### Se o POST duplicar

Usar e-mail alternativo:

```text
aluno.demo.senai2@example.com
```

### Se o tempo apertar

Cortar detalhes de LearningTracks, Modules, GitHub Actions e Branch Protection. Manter obrigatoriamente Controller, DTO, Service, DI, EF Core e demo básica.
