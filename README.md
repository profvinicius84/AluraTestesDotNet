# AluraTestesDotNet

Repositório de exemplo usado nas aulas de testes unitários em .NET.

Este projeto contém exercícios e projetos de demonstração para aprender a escrever testes automatizados em aplicações .NET.

## Principais conteúdos
- Exercícios por aula (por exemplo `Aula 1 - Calculadora`).
- Projetos de teste (ex.: `CalculadoraApp.Testes`).

## Pré-requisitos
- .NET SDK (recomenda-se .NET 6 ou superior) instalado. Verifique com:

    ```
    dotnet --version
    ```

## Como executar
1. Restaurar dependências:

    ```
    dotnet restore
    ```

2. Compilar o projeto:

    ```
    dotnet build
    ```

3. Executar os testes automatizados (no diretório raiz do repositório):

    ```
    dotnet test
    ```

    Para executar um projeto de testes específico, especifique o caminho do `.csproj`:

    ```
    dotnet test "Aula 1 - Calculadora/CalculadoraApp.Testes/CalculadoraApp.Testes.csproj"
    ```

## Abrir no editor
- Visual Studio: abra a pasta ou a solução.
- Visual Studio Code: abra a pasta do repositório e use a extensão C# para melhor experiência.

## Estrutura
- `Aula 1 - Calculadora/` — exemplos e testes relacionados à calculadora.
- `CalculadoraApp.Testes/` — projeto de testes da calculadora.

## Contribuições
Contribuições são bem-vindas. Abra issues ou pull requests com melhorias, correções de bugs ou novos exercícios.

## Licença
Coloque aqui o tipo de licença do projeto, se houver (por exemplo: MIT).
