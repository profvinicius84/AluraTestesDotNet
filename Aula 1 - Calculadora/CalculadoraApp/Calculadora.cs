using System;

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
}
