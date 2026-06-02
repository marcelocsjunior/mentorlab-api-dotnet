# Checklist do Dia da Aula — SENAI/SC LAB365

## 1. Validar baseline local

```bash
cd ~/mentorlab-api-dotnet

git checkout main
git pull origin main
git status

dotnet restore MentorLab.sln
dotnet build MentorLab.sln
dotnet test MentorLab.sln
```

Resultado esperado:

```text
working tree clean
restore OK
build OK
0 warnings
0 errors
22 testes
0 falhas
```

## 2. Garantir banco e migrations

```bash
dotnet ef database update --project src/MentorLab.Api/MentorLab.Api.csproj
```

Resultado esperado:

```text
Done.
```

## 3. Subir API local

Terminal 1:

```bash
cd ~/mentorlab-api-dotnet

dotnet run --project src/MentorLab.Api/MentorLab.Api.csproj --urls http://0.0.0.0:5080
```

Resultado esperado:

```text
Now listening on: http://0.0.0.0:5080
Application started.
```

## 4. Testar endpoints antes da apresentação

Terminal 2:

```bash
curl -s http://localhost:5080/api/status
curl -s http://localhost:5080/api/students
curl -s http://localhost:5080/api/learning-tracks
```

Resultado esperado:

```text
/api/status          -> status ok
/api/students        -> lista JSON
/api/learning-tracks -> lista JSON
```

## 5. Preparar comando de criação do aluno demo

```bash
curl -s -X POST http://localhost:5080/api/students \
  -H "Content-Type: application/json" \
  -d '{"fullName":"Aluno Demo SENAI","email":"aluno.demo.senai@example.com"}'
```

Se já existir e ocorrer duplicidade:

```bash
curl -s -X POST http://localhost:5080/api/students \
  -H "Content-Type: application/json" \
  -d '{"fullName":"Aluno Demo SENAI 2","email":"aluno.demo.senai2@example.com"}'
```

## 6. Arquivos para deixar abertos no VS Code

```text
README.md
src/MentorLab.Api/Program.cs
src/MentorLab.Api/Controllers/StudentsController.cs
src/MentorLab.Api/DTOs/Students/CreateStudentRequest.cs
src/MentorLab.Api/DTOs/Students/StudentResponse.cs
src/MentorLab.Api/Entities/Student.cs
src/MentorLab.Api/Services/Students/IStudentService.cs
src/MentorLab.Api/Services/Students/StudentService.cs
src/MentorLab.Api/Data/MentorLabDbContext.cs
tests/MentorLab.Api.Tests/Services/StudentServiceTests.cs
.github/workflows/dotnet-ci.yml
```

Tela inicial recomendada: `README.md`.

## 7. Navegador aberto

```text
1. Repositório GitHub mentorlab-api-dotnet
2. Aba Actions mostrando .NET CI verde
3. Swagger da API, se estiver usando
```

Swagger local provável:

```text
http://localhost:5080/swagger
```

## 8. Sequência operacional da demo

Durante a aula, executar somente o necessário:

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

```bash
dotnet test MentorLab.sln
```

Não fazer migration ao vivo. Migration ao vivo é igual atualizar firmware em produção: só parece boa ideia antes de começar.

## 9. Plano B

### API não sobe

Rodar:

```bash
dotnet test MentorLab.sln
```

Fala:

> Como o foco é demonstrar arquitetura e fluxo da API, sigo pela validação automatizada já versionada. Os testes cobrem services e endpoints principais, garantindo que o comportamento esperado da aplicação está íntegro.

### POST duplicado

Usar:

```text
aluno.demo.senai2@example.com
```

### Terminal poluído

```bash
clear
```

### Tempo apertado

Cortar detalhes de LearningTracks, Modules, GitHub Actions e Branch Protection. Manter obrigatoriamente:

```text
Controller
DTO
Service
DI
EF Core
Demo básica
```

## 10. Checklist físico e operacional

```text
[ ] Notebook carregado
[ ] Fonte do terminal aumentada
[ ] VS Code com zoom confortável
[ ] Internet funcionando
[ ] Projeto atualizado
[ ] API testada antes
[ ] GitHub Actions verde aberto
[ ] Comandos de curl prontos
[ ] Plano B validado
[ ] Celular no silencioso
[ ] Água por perto
```

## 11. Ordem mental da apresentação

```text
Problema
Arquitetura
Entity x DTO
EF Core
Service
Controller
DI
Demo
Testes/CI
Fechamento
```
