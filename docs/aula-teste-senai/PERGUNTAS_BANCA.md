# Perguntas Prováveis da Banca — Aula Teste SENAI/SC

## 1. Por que separar Controller, DTO, Service e Entity?

Para separar responsabilidades e reduzir acoplamento. Controller cuida da entrada HTTP, DTO define contrato de comunicação, Service concentra regra de negócio e Entity representa o modelo persistido.

## 2. Qual é a diferença entre DTO e Entity?

Entity é o modelo usado para persistência. DTO é o contrato usado para entrada e saída de dados na API.

Exemplo: `Student` possui `Id`, `IsActive`, `CreatedAt` e `UpdatedAt`; `CreateStudentRequest` recebe apenas `FullName` e `Email`.

## 3. Por que não colocar regra de negócio no Controller?

Porque o Controller deve orquestrar HTTP. Regras de negócio ficam no Service para facilitar manutenção, reuso e testes.

## 4. O que é Dependency Injection?

É o mecanismo que registra dependências e permite que o ASP.NET Core entregue as instâncias necessárias em tempo de execução.

No projeto, `StudentsController` depende de `IStudentService`, resolvido como `StudentService` no `Program.cs`.

## 5. Por que usar interface no Service?

Para depender de uma abstração, não da implementação concreta. Isso reduz acoplamento e melhora testabilidade.

## 6. Por que usar `AddScoped`?

Porque Service e DbContext devem viver dentro do escopo da requisição HTTP. `Scoped` combina bem com o ciclo de vida do EF Core DbContext.

## 7. O que é DbContext?

`DbContext` é a ponte entre a aplicação C# e o banco. Ele controla entidades, consultas, alterações e persistência.

## 8. Por que usar EF Core?

Porque permite trabalhar com objetos C# e persistir em banco relacional com migrations, LINQ, relacionamentos e integração nativa com ASP.NET Core.

## 9. Por que SQLite?

Porque é leve, local e adequado para uma aula prática. Reduz dependência externa e mantém foco na arquitetura da API.

## 10. O que são migrations?

São o versionamento do schema do banco. Quando o modelo muda no código, a migration registra como aplicar essa mudança no banco.

## 11. Por que soft delete?

Para preservar histórico. Em vez de apagar fisicamente, o registro é marcado como `IsActive = false`.

## 12. Como evitar e-mail duplicado?

A regra está no `StudentService`. O e-mail é normalizado para lowercase e o serviço verifica duplicidade entre alunos ativos.

## 13. O que significa REST no projeto?

Uso de recursos, métodos HTTP e respostas padronizadas: GET consulta, POST cria, PUT atualiza e DELETE remove logicamente.

## 14. Qual a função do Swagger?

Documentar e testar a API de forma interativa, exibindo endpoints, payloads e respostas.

## 15. Por que criar testes automatizados?

Para validar comportamento e proteger contra regressões. Teste automatizado transforma confiança em evidência.

## 16. O que é WebApplicationFactory?

É um recurso para subir a aplicação em ambiente de teste e validar endpoints sem depender de servidor externo.

## 17. Por que SQLite em memória nos testes?

Para isolar os testes e evitar dependência do banco local da aplicação.

## 18. O que o GitHub Actions valida?

Executa restore, build e test automaticamente. Se não passa, não deveria entrar na main.

## 19. Por que documentar Branch Protection?

Porque CI só vira quality gate real quando a main exige checks antes do merge.

## 20. O projeto está pronto para produção?

Não diretamente. Está pronto como projeto didático e de portfólio técnico. Para produção, eu adicionaria autenticação, autorização, tratamento global de exceções, logs estruturados, validação robusta, banco gerenciado, observabilidade e pipeline com ambientes.

## 21. Por que não usar Repository Pattern?

Porque neste escopo o EF Core já cumpre bem o papel de abstração de acesso a dados. Adicionar Repository aumentaria complexidade sem ganho didático proporcional.

## 22. Por que não usar AutoMapper?

Porque mapeamento manual é mais explícito e didático para a aula. AutoMapper faria sentido em projetos maiores.

## 23. Por que não usar Minimal APIs?

Porque o tema da aula pede Controller, DTO, Service, DI e EF Core. Controllers deixam a separação mais didática.

## 24. Como lidar com alunos em níveis diferentes?

Trabalhando em camadas: iniciantes entendem o fluxo HTTP; intermediários entram em DI e EF Core; avançados discutem testes, CI e governança.

## 25. Como organizar tudo em 20 minutos?

Com recorte de escopo. O foco é o fluxo essencial: Controller, DTO, Service, DI e EF Core, usando o domínio simples `Students`.

## Resposta-chave para pressão da banca

> Construir API não é apenas criar endpoints. É organizar responsabilidades. Quando Controller, DTO, Service, DI e EF Core estão bem separados, o projeto fica mais fácil de entender, testar e evoluir.
