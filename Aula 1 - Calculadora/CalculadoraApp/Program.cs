// Programa principal que exibe um menu interativo no console para utilizar os
// métodos disponíveis da classe `Calculadora` (Somar, Subtrair, Multiplicar, Dividir).
// Este arquivo adota top-level statements (C# 9+) por simplicidade.

var calc = new Calculadora();

// Lê de forma segura um número de ponto flutuante do console.
// Repete a solicitação até que o usuário informe um valor válido.
static double ReadDouble(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (input is null)
        {
            // Se a entrada for nula (ex.: EOF), encerra o programa com código de erro.
            Console.WriteLine("Entrada nula. Encerrando.");
            Environment.Exit(1);
        }
        if (double.TryParse(input, out double value))
            return value;

        // Mensagem de erro e nova tentativa quando a conversão falha.
        Console.WriteLine("Entrada inválida. Digite um número válido.");
    }
}

// Loop principal do menu. Exibe opções, lê a escolha do usuário e executa
// a operação correspondente usando a instância `calc` da classe Calculadora.
while (true)
{
    Console.WriteLine();
    Console.WriteLine("--- Menu Calculadora ---");
    Console.WriteLine("1 - Somar");
    Console.WriteLine("2 - Subtrair");
    Console.WriteLine("3 - Multiplicar");
    Console.WriteLine("4 - Dividir");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");

    string? escolha = Console.ReadLine();

    // Validação básica da opção: vazio ou nulo é inválido.
    if (string.IsNullOrWhiteSpace(escolha))
    {
        Console.WriteLine("Opção inválida. Tente novamente.");
        continue;
    }

    // Caso escolha sair, encerra o loop e finaliza o programa.
    if (escolha == "0")
    {
        Console.WriteLine("Encerrando calculadora...");
        break;
    }

    // Converte a opção para inteiro e valida.
    if (!int.TryParse(escolha, out int opcao))
    {
        Console.WriteLine("Opção inválida. Digite o número da opção.");
        continue;
    }

    try
    {
        // Roteamento das opções para os métodos da Calculadora.
        switch (opcao)
        {
            case 1:
            {
                // Soma: solicita dois valores e exibe o resultado.
                double a = ReadDouble("Digite o primeiro número: ");
                double b = ReadDouble("Digite o segundo número: ");
                Console.WriteLine($"Resultado: {calc.Somar(a, b)}");
                break;
            }
            case 2:
            {
                // Subtração: solicita dois valores e exibe o resultado.
                double a = ReadDouble("Digite o primeiro número: ");
                double b = ReadDouble("Digite o segundo número: ");
                Console.WriteLine($"Resultado: {calc.Subtrair(a, b)}");
                break;
            }
            case 3:
            {
                // Multiplicação: solicita dois valores e exibe o resultado.
                double a = ReadDouble("Digite o primeiro número: ");
                double b = ReadDouble("Digite o segundo número: ");
                Console.WriteLine($"Resultado: {calc.Multiplicar(a, b)}");
                break;
            }
            case 4:
            {
                // Divisão: trata especificamente a exceção de divisão por zero.
                double a = ReadDouble("Digite o dividendo: ");
                double b = ReadDouble("Digite o divisor: ");
                try
                {
                    Console.WriteLine($"Resultado: {calc.Dividir(a, b)}");
                }
                catch (DivideByZeroException ex)
                {
                    // Mensagem amigável ao usuário quando o divisor é zero.
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                break;
            }
            default:
                // Opção não reconhecida pelo menu.
                Console.WriteLine("Opção inválida. Escolha uma opção válida.");
                break;
        }
    }
    catch (Exception ex)
    {
        // Tratamento genérico de exceções inesperadas para garantir que
        // o programa não termine de forma abrupta sem informar o usuário.
        Console.WriteLine($"Erro inesperado: {ex.Message}");
    }
}