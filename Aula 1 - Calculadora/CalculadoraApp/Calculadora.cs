/// <summary>
/// Classe Calculadora fornece métodos para operações matemáticas básicas entre dois números.
/// </summary>
public class Calculadora
{
    /// <summary>
    /// Soma dois números.
    /// </summary>
    public double Somar(double a, double b)
    {
        return a + b;
    }

    /// <summary>
    /// Subtrai o segundo número do primeiro.
    /// </summary>
    public double Subtrair(double a, double b)
    {
        return a - b;
    }

    /// <summary>
    /// Multiplica dois números.
    /// </summary>
    public double Multiplicar(double a, double b)
    {
        return a * b;
    }

    /// <summary>
    /// Divide o primeiro número pelo segundo.
    /// </summary>
    /// <exception cref="DivideByZeroException">Lançada quando o divisor é zero.</exception>
    public double Dividir(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("O divisor não pode ser zero.");
        return a / b;
    }

    /// <summary>
    /// Calcula a potência de um número elevado a outro.
    /// </summary>
    public double Potencia(double baseNum, double expoente)
    {
        return Math.Pow(baseNum, expoente);
    }

    /// <summary>
    /// Calcula a raiz quadrada de um número.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Lançada quando o valor é negativo.</exception>
    public double RaizQuadrada(double valor)
    {
        if (valor < 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "Não é possível calcular a raiz quadrada de um número negativo.");
        return Math.Sqrt(valor);
    }

    /// <summary>
    /// Retorna o valor absoluto de um número.
    /// </summary>
    public double Absoluto(double valor)
    {
        return Math.Abs(valor);
    }

    /// <summary>
    /// Retorna o maior entre dois números.
    /// </summary>
    public double Maximo(double a, double b)
    {
        return Math.Max(a, b);
    }

    /// <summary>
    /// Retorna o menor entre dois números.
    /// </summary>
    public double Minimo(double a, double b)
    {
        return Math.Min(a, b);
    }

    /// <summary>
    /// Calcula o Máximo Divisor Comum (MDC) entre dois números inteiros.
    /// </summary>
    public int Mdc(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    /// <summary>
    /// Calcula o Mínimo Múltiplo Comum (MMC) entre dois números inteiros.
    /// </summary>
    public int Mmc(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        if (a == 0 || b == 0)
            return 0;
        return (a * b) / Mdc(a, b);
    }
}