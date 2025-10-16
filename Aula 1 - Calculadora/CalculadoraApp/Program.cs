using System;


// Instancia a calculadora
Calculadora calc = new Calculadora();
while (true)
{
    // Exibe o menu de opções
    Console.WriteLine("\n--- Menu Calculadora ---");
    Console.WriteLine("1. Somar");
    Console.WriteLine("2. Subtrair");
    Console.WriteLine("3. Multiplicar");
    Console.WriteLine("4. Dividir");
    Console.WriteLine("5. Potência");
    Console.WriteLine("6. Raiz Quadrada");
    Console.WriteLine("7. Absoluto");
    Console.WriteLine("8. Máximo");
    Console.WriteLine("9. Mínimo");
    Console.WriteLine("10. MMC");
    Console.WriteLine("11. MDC");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");
    string opcao = Console.ReadLine();
    if (opcao == "0") break;
    try
    {
        // Executa a operação escolhida pelo usuário
        switch (opcao)
        {
            case "1":
                // Soma dois números
                Console.Write("Digite o primeiro número: ");
                double a1 = double.Parse(Console.ReadLine());
                Console.Write("Digite o segundo número: ");
                double b1 = double.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.Somar(a1, b1)}");
                break;
            case "2":
                // Subtrai dois números
                Console.Write("Digite o primeiro número: ");
                double a2 = double.Parse(Console.ReadLine());
                Console.Write("Digite o segundo número: ");
                double b2 = double.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.Subtrair(a2, b2)}");
                break;
            case "3":
                // Multiplica dois números
                Console.Write("Digite o primeiro número: ");
                double a3 = double.Parse(Console.ReadLine());
                Console.Write("Digite o segundo número: ");
                double b3 = double.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.Multiplicar(a3, b3)}");
                break;
            case "4":
                // Divide dois números
                Console.Write("Digite o primeiro número: ");
                double a4 = double.Parse(Console.ReadLine());
                Console.Write("Digite o segundo número: ");
                double b4 = double.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.Dividir(a4, b4)}");
                break;
            case "5":
                // Calcula potência
                Console.Write("Digite a base: ");
                double baseNum = double.Parse(Console.ReadLine());
                Console.Write("Digite o expoente: ");
                double expoente = double.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.Potencia(baseNum, expoente)}");
                break;
            case "6":
                // Calcula raiz quadrada
                Console.Write("Digite o número: ");
                double valorRaiz = double.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.RaizQuadrada(valorRaiz)}");
                break;
            case "7":
                // Calcula valor absoluto
                Console.Write("Digite o número: ");
                double valorAbs = double.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.Absoluto(valorAbs)}");
                break;
            case "8":
                // Calcula o máximo entre dois números
                Console.Write("Digite o primeiro número: ");
                double a8 = double.Parse(Console.ReadLine());
                Console.Write("Digite o segundo número: ");
                double b8 = double.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.Maximo(a8, b8)}");
                break;
            case "9":
                // Calcula o mínimo entre dois números
                Console.Write("Digite o primeiro número: ");
                double a9 = double.Parse(Console.ReadLine());
                Console.Write("Digite o segundo número: ");
                double b9 = double.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.Minimo(a9, b9)}");
                break;
            case "10":
                // Calcula o MMC entre dois números inteiros
                Console.Write("Digite o primeiro número inteiro: ");
                int a10 = int.Parse(Console.ReadLine());
                Console.Write("Digite o segundo número inteiro: ");
                int b10 = int.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.Mmc(a10, b10)}");
                break;
            case "11":
                // Calcula o MDC entre dois números inteiros
                Console.Write("Digite o primeiro número inteiro: ");
                int a11 = int.Parse(Console.ReadLine());
                Console.Write("Digite o segundo número inteiro: ");
                int b11 = int.Parse(Console.ReadLine());
                Console.WriteLine($"Resultado: {calc.Mdc(a11, b11)}");
                break;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
    }
    catch (Exception ex)
    {
        // Exibe mensagem de erro caso ocorra exceção
        Console.WriteLine($"Erro: {ex.Message}");
    }
}
// Mensagem de encerramento
Console.WriteLine("Encerrando calculadora...");