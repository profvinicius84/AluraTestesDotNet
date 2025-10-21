using System;

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
}
