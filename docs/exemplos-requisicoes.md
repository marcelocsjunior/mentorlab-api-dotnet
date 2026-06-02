# Exemplos de requisições

Este documento registra exemplos planejados de uso da API.

Os endpoints podem ser ajustados durante a implementação.

---

## Criar aluno

```http
POST /api/students
Content-Type: application/json
```

```json
{
  "fullName": "Ana Silva",
  "email": "ana.silva@example.com"
}
```

Resposta esperada:

```http
201 Created
```

---

## Listar alunos

```http
GET /api/students
```

Resposta esperada:

```http
200 OK
```

---

## Criar trilha de aprendizado

```http
POST /api/tracks
Content-Type: application/json
```

```json
{
  "name": "Back-End .NET Básico",
  "description": "Trilha inicial para fundamentos de C#, API REST e banco de dados."
}
```

---

## Criar módulo

```http
POST /api/modules
Content-Type: application/json
```

```json
{
  "learningTrackId": 1,
  "title": "Fundamentos de API REST",
  "description": "Módulo introdutório sobre HTTP, endpoints, verbos e status codes.",
  "order": 1
}
```

---

## Criar exercício

```http
POST /api/exercises
Content-Type: application/json
```

```json
{
  "moduleId": 1,
  "title": "Criar endpoint de cadastro de alunos",
  "description": "Implementar POST /api/students usando DTO, service e persistência.",
  "difficulty": "Beginner"
}
```

---

## Registrar entrega

```http
POST /api/submissions
Content-Type: application/json
```

```json
{
  "studentId": 1,
  "exerciseId": 1,
  "repositoryUrl": "https://github.com/aluno/exercicio-api-students",
  "notes": "Entrega inicial com controller e DTO."
}
```

---

## Registrar feedback do mentor

```http
POST /api/feedbacks
Content-Type: application/json
```

```json
{
  "submissionId": 1,
  "score": 8,
  "comment": "Boa separação entre DTO e controller. Próximo passo: mover regra para service e adicionar validação."
}
```

---

## Dashboard resumido

```http
GET /api/dashboard/summary
```

Resposta planejada:

```json
{
  "activeStudents": 12,
  "learningTracks": 2,
  "pendingSubmissions": 5,
  "feedbacksGiven": 18
}
```
