# Sprint 6 — Branch Protection e Required CI Checks

## Objetivo

A Sprint 6 transforma o workflow `.NET CI` em um quality gate real para a branch `main`.

O objetivo é proteger a branch principal para que mudanças só entrem por Pull Request e apenas depois que o check `Restore, build and test` passar.

## Por que CI sem branch protection não é suficiente

A Sprint 5 criou o workflow de CI com GitHub Actions.

Sem branch protection, o workflow roda, mas ainda é possível fazer merge mesmo com falha, fazer push direto para `main`, forçar histórico com force push ou deletar a branch principal.

Com branch protection, o CI passa a ser uma regra de governança. O status check deixa de ser apenas informativo e vira uma condição obrigatória antes do merge.

## Proteção da branch main

A branch `main` deve ter uma regra de proteção com o padrão:

```text
main
```

Configuração esperada:

- exigir Pull Request antes do merge
- exigir status checks antes do merge
- exigir que a branch esteja atualizada antes do merge, se disponível
- bloquear force push
- bloquear deleção da `main`
- não permitir bypass das regras quando fizer sentido para demonstração

## Required status checks

O workflow `.NET CI` possui o job:

```text
Restore, build and test
```

Esse check deve ser selecionado como required status check.

Com isso, Pull Requests para `main` só podem ser mergeados quando restore, build e test passam no GitHub Actions.

## Bloqueio de force push

Force push reescreve histórico remoto.

Em uma branch principal, isso pode apagar commits, quebrar rastreabilidade e dificultar auditoria. Por isso, a regra da `main` deve bloquear force pushes.

## Bloqueio de deleção da main

A branch `main` representa a linha principal do projeto.

Bloquear deleção evita remoções acidentais da branch que concentra releases, tags e Pull Requests.

## Fluxo esperado de PR

1. Criar uma branch a partir de `main`.
2. Implementar a mudança.
3. Rodar validações locais:

```bash
dotnet restore MentorLab.sln
dotnet build MentorLab.sln
dotnet test MentorLab.sln
```

4. Abrir Pull Request para `main`.
5. Aguardar o GitHub Actions executar o workflow `.NET CI`.
6. Confirmar que o check `Restore, build and test` passou.
7. Revisar e aprovar o PR.
8. Fazer merge apenas após o required check estar verde.

## Configuração automática

Foi feita verificação local com GitHub CLI:

```bash
gh auth status
```

Resultado:

```text
Token local do gh inválido.
```

Por isso, a branch protection não foi aplicada automaticamente neste ambiente. O procedimento manual abaixo deve ser usado no GitHub.

## Procedimento manual no GitHub UI

Acesse:

```text
Repository -> Settings -> Branches -> Add branch protection rule
```

Branch name pattern:

```text
main
```

Ativar:

- Require a pull request before merging
- Require status checks to pass before merging
- Require branches to be up to date before merging, se disponível
- Selecionar o check `Restore, build and test`
- Do not allow bypassing the above settings, se fizer sentido para demonstração
- Block force pushes
- Block deletions

Salvar a regra.

## Como validar se a regra está ativa

Abra um Pull Request para `main`.

Antes do workflow finalizar, o PR deve indicar que o merge está bloqueado por required checks pendentes.

Depois que o workflow `.NET CI` passar, o check `Restore, build and test` deve aparecer como sucesso.

Para uma validação adicional, confirme em:

```text
Repository -> Settings -> Branches
```

A regra para `main` deve aparecer listada como branch protection rule ativa.

## Impacto na governança

Com branch protection, o MentorLab API passa a ter um fluxo mais próximo de um projeto profissional:

- mudanças entram por PR
- CI precisa passar antes do merge
- a branch principal fica protegida contra alterações diretas perigosas
- histórico e rastreabilidade ficam preservados
- a suíte de testes passa a proteger a evolução do projeto

## Narrativa para banca técnica

“Nesta sprint eu completei o ciclo de qualidade do repositório. Primeiro criamos testes automatizados, depois configuramos o CI para executar restore, build e test no GitHub Actions. Agora, com branch protection, esse CI vira uma regra obrigatória para a branch main. Isso impede merge sem validação, bloqueia force push e protege a branch principal contra deleção. A ideia é mostrar não só código funcionando, mas também governança técnica de entrega.”

Para demonstrar, mostre o workflow `.NET CI`, abra a regra de branch protection em `Settings -> Branches` e explique que o PR só pode ser mergeado quando o check `Restore, build and test` estiver aprovado.
