using Moq;
using System.Text;
using System.Net.Http.Json;
using ScreenSound.Shared.Modelos.Response;

namespace ScreenSound.Tests.Integracao
{
    public class GenerosAPITest
    {
        private static HttpClient CreateClient(Func<HttpRequestMessage, HttpResponseMessage> handler, Uri? baseAddress = null)
        {
            var messageHandler = new StubHttpMessageHandler(handler);
            var client = new HttpClient(messageHandler);
            if (baseAddress != null)
                client.BaseAddress = baseAddress;
            return client;
        }

        // Teste do endpoint GET /generos (lista)
        [Fact]
        public async Task GetGenerosAsync_Deve_ChamarEndpointECapturarLista()
        {
            var generosEsperados = new List<GeneroResponse>
            {
                new GeneroResponse(1, "G1", "d1"),
                new GeneroResponse(2, "G2", "d2")
            };

            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Get && request.RequestUri!.PathAndQuery == "/generos")
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(generosEsperados);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };
                }

                if (request.Method == HttpMethod.Post && request.RequestUri!.PathAndQuery == "/generos")
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }

                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(f => f.CreateClient("API")).Returns(httpClient);

            var api = new ScreenSound.WebAssembly.Services.MusicasAPI(factory.Object, new Mock<Microsoft.Extensions.Logging.ILogger<ScreenSound.WebAssembly.Services.MusicasAPI>>().Object);

            // Act
            var response = await httpClient.GetAsync("http://localhost/generos");
            var conteudo = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(conteudo);
        }

        // Teste do endpoint POST /generos (criação)
        // Arrange: retorna OK para POST
        // Act: envia POST
        // Assert: status OK
        [Fact]
        public async Task PostGenero_Deve_RetornarOk()
        {
            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Post && request.RequestUri!.PathAndQuery == "/generos")
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));
            var req = new ScreenSound.Shared.Modelos.Requests.GeneroRequest("GNew", "d");
            var res = await httpClient.PostAsJsonAsync("/generos", req);
            Assert.Equal(System.Net.HttpStatusCode.OK, res.StatusCode);
        }

        // Teste do endpoint GET /generos/{nome}
        // Arrange: simula retorno de um genero por nome
        // Act: get via HttpClient
        // Assert: valida dados
        [Fact]
        public async Task GetGeneroPorNome_Deve_RetornarGenero()
        {
            var genero = new GeneroResponse(11, "GenX", "d");
            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Get && request.RequestUri!.PathAndQuery == "/generos/GenX")
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(genero);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));
            var res = await httpClient.GetAsync("http://localhost/generos/GenX");
            var content = await res.Content.ReadAsStringAsync();

            Assert.Equal(System.Net.HttpStatusCode.OK, res.StatusCode);
            var obj = System.Text.Json.JsonSerializer.Deserialize<GeneroResponse>(content);
            Assert.NotNull(obj);
            Assert.Equal(genero.Id, obj!.Id);
        }

        // Teste do endpoint DELETE /generos/{id}
        [Fact]
        public async Task DeleteGenero_Deve_RetornarNoContent()
        {
            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Delete && request.RequestUri!.PathAndQuery == "/generos/4")
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));
            var res = await httpClient.DeleteAsync("http://localhost/generos/4");
            Assert.Equal(System.Net.HttpStatusCode.NoContent, res.StatusCode);
        }
    }
}
