using FluentAssertions;

namespace CalculadoraApp.Testes
{
    /// <summary>
    /// Testes unitários para a classe Calculadora, cobrindo casos de sucesso, erro e resiliência.
    /// </summary>
    public class CalculadoraTest
    {
        /// <summary>
        /// Testa se o método Somar retorna o valor correto para dois números positivos.
        /// </summary>
        [Fact]
        public void TesteSoma_DeveRetornarValorCorreto()
        {
            var calculadora = new Calculadora();
            calculadora.Somar(5, 3).Should().Be(8);
        }

        /// <summary>
        /// Testa se o método Subtrair retorna o valor correto para dois números positivos.
        /// </summary>
        [Fact]
        public void TesteSubtracao_DeveRetornarValorCorreto()
        {
            var calculadora = new Calculadora();
            calculadora.Subtrair(5, 3).Should().Be(2);
        }

        /// <summary>
        /// Testa se o método Multiplicar retorna o valor correto para dois números positivos.
        /// </summary>
        [Fact]
        public void TesteMultiplicacao_DeveRetornarValorCorreto()
        {
            var calculadora = new Calculadora();
            calculadora.Multiplicar(5, 3).Should().Be(15);
        }

        /// <summary>
        /// Testa se o método Dividir retorna o valor correto para divisão exata.
        /// </summary>
        [Fact]
        public void TesteDivisao_DeveRetornarValorCorreto()
        {
            var calculadora = new Calculadora();
            calculadora.Dividir(6, 3).Should().Be(2);
        }

        /// <summary>
        /// Testa se o método Dividir lança exceção ao tentar dividir por zero.
        /// </summary>
        [Fact]
        public void TesteDivisaoPorZero_DeveLancarExcecao()
        {
            var calculadora = new Calculadora();
            Action acao = () => calculadora.Dividir(5, 0);
            acao.Should().Throw<DivideByZeroException>();
        }

        /// <summary>
        /// Testa se o método Potencia retorna o valor correto para base e expoente positivos.
        /// </summary>
        [Fact]
        public void TestePotencia_DeveRetornarValorCorreto()
        {
            var calculadora = new Calculadora();
            calculadora.Potencia(2, 3).Should().Be(8);
        }

        /// <summary>
        /// Testa se o método RaizQuadrada retorna o valor correto para número positivo.
        /// </summary>
        [Fact]
        public void TesteRaizQuadrada_DeveRetornarValorCorreto()
        {
            var calculadora = new Calculadora();
            calculadora.RaizQuadrada(9).Should().Be(3);
        }

        /// <summary>
        /// Testa se o método RaizQuadrada lança exceção ao tentar calcular raiz de número negativo.
        /// </summary>
        [Fact]
        public void TesteRaizQuadradaNegativa_DeveLancarExcecao()
        {
            var calculadora = new Calculadora();
            Action acao = () => calculadora.RaizQuadrada(-4);
            acao.Should().Throw<ArgumentOutOfRangeException>();
        }

        /// <summary>
        /// Testa se o método Absoluto retorna o valor absoluto de um número negativo.
        /// </summary>
        [Fact]
        public void TesteAbsoluto_DeveRetornarValorCorreto()
        {
            var calculadora = new Calculadora();
            calculadora.Absoluto(-5).Should().Be(5);
        }

        /// <summary>
        /// Testa se o método Maximo retorna o maior valor entre dois números.
        /// </summary>
        [Fact]
        public void TesteMaximo_DeveRetornarMaiorValor()
        {
            var calculadora = new Calculadora();
            calculadora.Maximo(5, 8).Should().Be(8);
        }

        /// <summary>
        /// Testa se o método Minimo retorna o menor valor entre dois números.
        /// </summary>
        [Fact]
        public void TesteMinimo_DeveRetornarMenorValor()
        {
            var calculadora = new Calculadora();
            calculadora.Minimo(5, 8).Should().Be(5);
        }

        /// <summary>
        /// Testa se o método Mmc retorna o mínimo múltiplo comum correto entre dois inteiros.
        /// </summary>
        [Fact]
        public void TesteMmc_DeveRetornarValorCorreto()
        {
            var calculadora = new Calculadora();
            calculadora.Mmc(6, 8).Should().Be(24);
        }

        /// <summary>
        /// Testa se o método Mmc retorna zero quando um dos valores é zero.
        /// </summary>
        [Fact]
        public void TesteMmcComZero_DeveRetornarZero()
        {
            var calculadora = new Calculadora();
            calculadora.Mmc(0, 8).Should().Be(0);
        }

        /// <summary>
        /// Testa se o método Mdc retorna o máximo divisor comum correto entre dois inteiros.
        /// </summary>
        [Fact]
        public void TesteMdc_DeveRetornarValorCorreto()
        {
            var calculadora = new Calculadora();
            calculadora.Mdc(6, 8).Should().Be(2);
        }

        /// <summary>
        /// Testa se o método Mdc retorna o outro número quando um dos valores é zero.
        /// </summary>
        [Fact]
        public void TesteMdcComZero_DeveRetornarOutroNumero()
        {
            var calculadora = new Calculadora();
            calculadora.Mdc(0, 8).Should().Be(8);
        }

        /// <summary>
        /// Testa se o método Mdc retorna valor positivo quando um dos valores é negativo.
        /// </summary>
        [Fact]
        public void TesteMdcComNegativo_DeveRetornarValorPositivo()
        {
            var calculadora = new Calculadora();
            calculadora.Mdc(-6, 8).Should().Be(2);
        }

        /// <summary>
        /// Testa se o método Dividir lança exceção ao dividir double.MaxValue por zero.
        /// </summary>
        [Fact]
        public void TesteDivisaoPorZeroComValorMaximo_DeveLancarExcecao()
        {
            var calculadora = new Calculadora();
            Action acao = () => calculadora.Dividir(double.MaxValue, 0);
            acao.Should().Throw<DivideByZeroException>();
        }

        /// <summary>
        /// Testa se o método RaizQuadrada lança exceção ao tentar calcular raiz de double.MinValue.
        /// </summary>
        [Fact]
        public void TesteRaizQuadradaDeMinValue_DeveLancarExcecao()
        {
            var calculadora = new Calculadora();
            Action acao = () => calculadora.RaizQuadrada(double.MinValue);
            acao.Should().Throw<ArgumentOutOfRangeException>();
        }

        /// <summary>
        /// Testa se o método Potencia retorna infinito ao elevar double.MaxValue a 2.
        /// </summary>
        [Fact]
        public void TestePotenciaComOverflow_DeveRetornarInfinito()
        {
            var calculadora = new Calculadora();
            var resultado = calculadora.Potencia(double.MaxValue, 2);
            double.IsInfinity(resultado).Should().BeTrue();
        }

        /// <summary>
        /// Testa se o método Mmc retorna zero quando ambos os valores são zero.
        /// </summary>
        [Fact]
        public void TesteMmcComAmbosZero_DeveRetornarZero()
        {
            var calculadora = new Calculadora();
            calculadora.Mmc(0, 0).Should().Be(0);
        }

        /// <summary>
        /// Testa se o método Mdc retorna zero quando ambos os valores são zero.
        /// </summary>
        [Fact]
        public void TesteMdcComAmbosZero_DeveRetornarZero()
        {
            var calculadora = new Calculadora();
            calculadora.Mdc(0, 0).Should().Be(0);
        }

        /// <summary>
        /// Testa se o método Minimo e Maximo lidam com valores extremos corretamente.
        /// </summary>
        [Fact]
        public void TesteMaximoMinimoComExtremos()
        {
            var calculadora = new Calculadora();
            calculadora.Maximo(double.MaxValue, double.MinValue).Should().Be(double.MaxValue);
            calculadora.Minimo(double.MaxValue, double.MinValue).Should().Be(double.MinValue);
        }
    }
}