/// <summary>
/// Classe Calculadora fornece m�todos para opera��es matem�ticas b�sicas entre dois n�meros.
/// </summary>
public class Calculadora
{
    /// <summary>
    /// Soma dois n�meros.
    /// </summary>
    public double Somar(double a, double b)
    {
        return a + b;
    }

    /// <summary>
    /// Subtrai o segundo n�mero do primeiro.
    /// </summary>
    public double Subtrair(double a, double b)
    {
        return a - b;
    }

    /// <summary>
    /// Multiplica dois n�meros.
    /// </summary>
    public double Multiplicar(double a, double b)
    {
        return a * b;
    }

    /// <summary>
    /// Divide o primeiro n�mero pelo segundo.
    /// </summary>
    /// <exception cref="DivideByZeroException">Lan�ada quando o divisor � zero.</exception>
    public double Dividir(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("O divisor n�o pode ser zero.");
        return a / b;
    }

    /// <summary>
    /// Calcula a pot�ncia de um n�mero elevado a outro.
    /// </summary>
    public double Potencia(double baseNum, double expoente)
    {
        return Math.Pow(baseNum, expoente);
    }

    /// <summary>
    /// Calcula a raiz quadrada de um n�mero.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Lan�ada quando o valor � negativo.</exception>
    public double RaizQuadrada(double valor)
    {
        if (valor < 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "N�o � poss�vel calcular a raiz quadrada de um n�mero negativo.");
        return Math.Sqrt(valor);
    }

    /// <summary>
    /// Retorna o valor absoluto de um n�mero.
    /// </summary>
    public double Absoluto(double valor)
    {
        return Math.Abs(valor);
    }

    /// <summary>
    /// Retorna o maior entre dois n�meros.
    /// </summary>
    public double Maximo(double a, double b)
    {
        return Math.Max(a, b);
    }

    /// <summary>
    /// Retorna o menor entre dois n�meros.
    /// </summary>
    public double Minimo(double a, double b)
    {
        return Math.Min(a, b);
    }

    /// <summary>
    /// Calcula o M�ximo Divisor Comum (MDC) entre dois n�meros inteiros.
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
    /// Calcula o M�nimo M�ltiplo Comum (MMC) entre dois n�meros inteiros.
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