# AluraTestesDotNet

Reposit�rio de exemplo usado nas aulas de testes unit�rios em .NET.

Este projeto cont�m exerc�cios e projetos de demonstra��o para aprender a escrever testes automatizados em aplica��es .NET.

## Principais conte�dos
- Exerc�cios por aula (por exemplo `Aula 1 - Calculadora`).
- Projetos de teste (ex.: `CalculadoraApp.Testes`).

## Pr�-requisitos
- .NET SDK (recomenda-se .NET 6 ou superior). Verifique com:

```
dotnet --version
```

Instale o SDK a partir de https://dotnet.microsoft.com/ se necess�rio.

## Como executar
1. Restaurar depend�ncias:

```
dotnet restore
```

2. Compilar o(s) projeto(s):

```
dotnet build
```

3. Executar os testes automatizados (no diret�rio raiz do reposit�rio):

```
dotnet test
```

Para executar um projeto de testes espec�fico, especifique o caminho do `.csproj` ou da solu��o `.sln`:

```
# executar um projeto de testes
dotnet test "Aula 1 - Calculadora/CalculadoraApp.Testes/CalculadoraApp.Testes.csproj"

# executar todos os testes em uma solu��o espec�fica
dotnet test "screensound/Screensound.sln"
```

Caso a solu��o `screensound` exista em outro local, substitua o caminho acima pelo caminho correto at� o arquivo `.sln` ou execute o `dotnet test` diretamente dentro do diret�rio da solu��o.

## Abrir no editor
- Visual Studio: abra a solu��o (`.sln`) ou a pasta do projeto.
- Visual Studio Code: abra a pasta do reposit�rio e use a extens�o C# (Omnisharp) para melhor experi�ncia.

## Estrutura
- `Aula 1 - Calculadora/` � exemplos e testes relacionados � calculadora.
- `CalculadoraApp.Testes/` � projeto de testes da calculadora.

> Observa��o: nomes de diret�rios podem variar dependendo das aulas. Navegue pela raiz do reposit�rio para ver todos os subprojetos e exerc�cios.

## Solu��o `screensound`
Se este reposit�rio contiver a solu��o `screensound`, ela geralmente estar� em uma pasta chamada `screensound` ou similar. Abra a solu��o no Visual Studio ou execute os testes espec�ficos com `dotnet test` apontando para o `.sln`/`.csproj` correspondente.

## Contato
Para d�vidas sobre o conte�do das aulas, acesse o nosso forum ou canal no discord.
