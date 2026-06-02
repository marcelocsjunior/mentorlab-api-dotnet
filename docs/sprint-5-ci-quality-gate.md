# Sprint 5 — CI Quality Gate

## Objetivo

A Sprint 5 adiciona um pipeline de CI com GitHub Actions para validar automaticamente o MentorLab API .NET.

O objetivo é garantir que todo push e todo Pull Request para `main` execute restore, build e testes antes de qualquer merge.

## Por que CI depois dos testes

A Sprint 4 criou a suíte automatizada com xUnit, `WebApplicationFactory` e SQLite em memória.

A Sprint 5 transforma essa suíte em uma barreira de qualidade executada no GitHub. Assim, os testes deixam de depender apenas da execução manual local e passam a proteger a branch principal.

## Workflow

Arquivo criado:

```text
.github/workflows/dotnet-ci.yml
```

Runner:

```text
ubuntu-latest
```

Actions usadas:

```text
actions/checkout@v6
actions/setup-dotnet@v5
```

Versão do .NET:

```text
8.0.x
```

## Gatilhos

O workflow roda em:

```yaml
push:
  branches:
    - main
pull_request:
  branches:
    - main
```

Isso cobre alterações enviadas diretamente para `main` e Pull Requests que pretendem entrar em `main`.

## Comandos executados

```bash
dotnet restore MentorLab.sln
dotnet build MentorLab.sln --configuration Release --no-restore
dotnet test MentorLab.sln --configuration Release --no-build --verbosity normal
```

O build e os testes rodam em `Release` para aproximar a validação do ambiente de entrega.

## O que o quality gate valida

- Restauração de dependências NuGet.
- Compilação da solution completa.
- Execução da suíte automatizada.
- Regressão dos services.
- Regressão dos endpoints principais.
- Compatibilidade do projeto de testes com ambiente Linux do GitHub Actions.

## Impacto na governança

Com CI, a branch `main` passa a ter uma validação objetiva antes do merge.

Isso melhora a rastreabilidade técnica do projeto, reduz risco de regressão e cria um padrão profissional de entrega: cada PR precisa provar que compila e que os testes continuam passando.

## Validação local

Comandos executados localmente:

```bash
dotnet restore MentorLab.sln
dotnet build MentorLab.sln
dotnet test MentorLab.sln
```

Resultado esperado:

```text
Restore OK
Build OK
Test OK
22 testes executados
0 falhas
```

## Narrativa para banca técnica

“Nesta sprint eu transformei os testes automatizados em um quality gate de CI. A Sprint 4 já garantia testes locais com xUnit, WebApplicationFactory e SQLite em memória. Agora, com GitHub Actions, todo Pull Request para `main` executa restore, build e test automaticamente. Isso aproxima o projeto de uma prática real de engenharia de software, em que a branch principal só deve receber código que compile e preserve a suíte de regressão.”

Para demonstrar, mostre o arquivo `.github/workflows/dotnet-ci.yml`, explique os gatilhos `push` e `pull_request`, abra a aba Actions do GitHub e destaque o resultado do workflow associado ao PR.
