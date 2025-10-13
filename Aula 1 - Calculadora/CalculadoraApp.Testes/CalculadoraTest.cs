using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraApp.Testes
{
    public class CalculadoraTest
    {
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

            Assert.Same(resultadoEsperado, resultado);
        }

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
}
