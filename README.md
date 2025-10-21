# AluraTestesDotNet

Reposit�rio de exemplo usado nas aulas de testes unit�rios em .NET.

Este projeto cont�m exerc�cios e projetos de demonstra��o para aprender a escrever testes automatizados em aplica��es .NET.

## Principais conte�dos
- Exerc�cios por aula (por exemplo `Aula 1 - Calculadora`).
- Projetos de teste (ex.: `CalculadoraApp.Testes`).

## Pr�-requisitos
- .NET SDK (recomenda-se .NET 6 ou superior) instalado. Verifique com:

    ```
    dotnet --version
    ```

## Como executar
1. Restaurar depend�ncias:

    ```
    dotnet restore
    ```

2. Compilar o projeto:

    ```
    dotnet build
    ```

3. Executar os testes automatizados (no diret�rio raiz do reposit�rio):

    ```
    dotnet test
    ```

    Para executar um projeto de testes espec�fico, especifique o caminho do `.csproj`:

    ```
    dotnet test "Aula 1 - Calculadora/CalculadoraApp.Testes/CalculadoraApp.Testes.csproj"
    ```

## Abrir no editor
- Visual Studio: abra a pasta ou a solu��o.
- Visual Studio Code: abra a pasta do reposit�rio e use a extens�o C# para melhor experi�ncia.

## Estrutura
- `Aula 1 - Calculadora/` � exemplos e testes relacionados � calculadora.
- `CalculadoraApp.Testes/` � projeto de testes da calculadora.

## Contribui��es
Contribui��es s�o bem-vindas. Abra issues ou pull requests com melhorias, corre��es de bugs ou novos exerc�cios.

## Licen�a
Coloque aqui o tipo de licen�a do projeto, se houver (por exemplo: MIT).
