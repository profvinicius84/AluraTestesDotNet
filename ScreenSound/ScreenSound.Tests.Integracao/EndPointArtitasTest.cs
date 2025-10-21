using FluentAssertions;

namespace ScreenSound.Tests.Integracao
{
    public class EndPointArtitasTest
    {
        [Fact]
        public async Task GetArtistas_Deve_Retornar_Ok_Mesmo_Sem_Dados()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:5241/artistas");

            response.EnsureSuccessStatusCode();

            var conteudo = await response.Content.ReadAsStringAsync();

            conteudo.Should().NotBeNull();
        }
    }
}
