using FluentAssertions;

namespace CalculadoraApp.Testes;

/// <summary>
/// Testes unitários para a classe Calculadora. Cada método verifica um cenário de sucesso
/// ou de tratamento de erro (exceções) para garantir resiliência.
/// </summary>
public class CalculadoraTest
{
    /// <summary>
    /// Testa se o método Somar retorna o valor correto para dois números positivos.
    /// Cenário: sucesso.
    /// </summary>
    [Fact]
    public void TesteSoma_DeveRetonarValorCorreto()
    {
        // Arrange
        var calculadora = new Calculadora();
        double a = 5;
        double b = 3;
        double resultadoEsperado = 8;
        // Act
        double resultado = calculadora.Somar(a, b);
        // Assert
        resultado.Should().Be(resultadoEsperado);
    }

    /// <summary>
    /// Testa se o método Subtrair retorna o valor correto para dois números positivos.
    /// Cenário: sucesso.
    /// </summary>
    [Fact]
    public void TesteSubtracao_DeveRetonarValorCorreto()
    {
        // Arrange
        var calculadora = new Calculadora();
        double a = 5;
        double b = 3;
        double resultadoEsperado = 2;
        // Act
        double resultado = calculadora.Subtrair(a, b);
        // Assert
        Assert.Equal(resultadoEsperado, resultado);
    }

    /// <summary>
    /// Testa se o método Multiplicar retorna o valor correto para dois números positivos.
    /// Cenário: sucesso.
    /// </summary>
    [Fact]
    public void TesteMultiplicacao_DeveRetonarValorCorreto()
    {
        // Arrange
        var calculadora = new Calculadora();
        double a = 5;
        double b = 3;
        double resultadoEsperado = 15;
        // Act
        double resultado = calculadora.Multiplicar(a, b);
        // Assert
        Assert.Equal(resultadoEsperado, resultado);
    }

    /// <summary>
    /// Testa se o método Dividir retorna o valor correto para divisão exata.
    /// Cenário: sucesso.
    /// </summary>
    [Fact]
    public void TesteDivisao_DeveRetonarValorCorreto()
    {
        // Arrange
        var calculadora = new Calculadora();
        double a = 6;
        double b = 3;
        double resultadoEsperado = 2;
        // Act
        double resultado = calculadora.Dividir(a, b);
        // Assert
        Assert.Equal(resultadoEsperado, resultado);
    }
}