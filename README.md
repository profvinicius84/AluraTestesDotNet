# AluraTestesDotNet

Repositório de exemplo usado nas aulas de testes unitários em .NET.

Este projeto contém exercícios e projetos de demonstração para aprender a escrever testes automatizados em aplicações .NET.

## Principais conteúdos
- Exercícios por aula (por exemplo `Aula 1 - Calculadora`).
- Projetos de teste (ex.: `CalculadoraApp.Testes`).

## Pré-requisitos
- .NET SDK (recomenda-se .NET 6 ou superior). Verifique com:

```
dotnet --version
```

Instale o SDK a partir de https://dotnet.microsoft.com/ se necessário.

## Como executar
1. Restaurar dependências:

```
dotnet restore
```

2. Compilar o(s) projeto(s):

```
dotnet build
```

3. Executar os testes automatizados (no diretório raiz do repositório):

```
dotnet test
```

Para executar um projeto de testes específico, especifique o caminho do `.csproj` ou da solução `.sln`:

```
# executar um projeto de testes
dotnet test "Aula 1 - Calculadora/CalculadoraApp.Testes/CalculadoraApp.Testes.csproj"

# executar todos os testes em uma solução específica
dotnet test "screensound/Screensound.sln"
```

Caso a solução `screensound` exista em outro local, substitua o caminho acima pelo caminho correto até o arquivo `.sln` ou execute o `dotnet test` diretamente dentro do diretório da solução.

## Abrir no editor
- Visual Studio: abra a solução (`.sln`) ou a pasta do projeto.
- Visual Studio Code: abra a pasta do repositório e use a extensão C# (Omnisharp) para melhor experiência.

## Estrutura
- `Aula 1 - Calculadora/` — exemplos e testes relacionados à calculadora.
- `CalculadoraApp.Testes/` — projeto de testes da calculadora.

> Observação: nomes de diretórios podem variar dependendo das aulas. Navegue pela raiz do repositório para ver todos os subprojetos e exercícios.

## Solução `screensound`
Se este repositório contiver a solução `screensound`, ela geralmente estará em uma pasta chamada `screensound` ou similar. Abra a solução no Visual Studio ou execute os testes específicos com `dotnet test` apontando para o `.sln`/`.csproj` correspondente.

## Contato
Para dúvidas sobre o conteúdo das aulas, acesse o nosso forum ou canal no discord.
