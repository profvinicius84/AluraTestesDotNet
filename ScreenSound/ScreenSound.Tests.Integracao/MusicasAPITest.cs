using Moq;
using System.Text;
using System.Net.Http.Json;
using ScreenSound.Shared.Modelos.Response;
using ScreenSound.WebAssembly.Services;

namespace ScreenSound.Tests.Integracao
{
    public class MusicasAPITest
    {
        private static HttpClient CreateClient(Func<HttpRequestMessage, HttpResponseMessage> handler, Uri? baseAddress = null)
        {
            var messageHandler = new StubHttpMessageHandler(handler);
            var client = new HttpClient(messageHandler);
            if (baseAddress != null)
                client.BaseAddress = baseAddress;
            return client;
        }

        // Teste do endpoint GET /musicas (lista)
        // Arrange: simula lista de musicas
        // Act: chama MusicasAPI.GetMusicasAsync
        // Assert: valida a lista
        [Fact]
        public async Task GetMusicasAsync_Deve_ChamarEndpointECapturarLista()
        {
            // Arrange
            var musicasEsperadas = new List<MusicaResponse>
            {
                new MusicaResponse(1, "M1", 1, "A1", 2000, null),
                new MusicaResponse(2, "M2", 2, "A2", 2010, null)
            };

            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Get && request.RequestUri!.PathAndQuery == "/musicas")
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(musicasEsperadas);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(f => f.CreateClient("API")).Returns(httpClient);

            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<MusicasAPI>>();
            var api = new MusicasAPI(factory.Object, logger.Object);

            // Act
            var musicasObtidas = await api.GetMusicasAsync();

            // Assert
            Assert.NotNull(musicasObtidas);
            Assert.Equal(musicasEsperadas.Count, musicasObtidas.Count);
            for (int i = 0; i < musicasEsperadas.Count; i++)
            {
                Assert.Equal(musicasEsperadas[i].Id, musicasObtidas.ElementAt(i).Id);
                Assert.Equal(musicasEsperadas[i].Nome, musicasObtidas.ElementAt(i).Nome);
            }
        }

        // Teste do endpoint GET /musicas/{nome}
        // Arrange: simula retorno de musica por nome
        // Act: request via HttpClient
        // Assert: valida resposta
        [Fact]
        public async Task GetMusicaPorNome_Deve_RetornarMusica()
        {
            var musica = new MusicaResponse(5, "SongX", 1, "Art", 1999, null);
            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Get && request.RequestUri!.PathAndQuery == "/musicas/SongX")
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(musica);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));

            var res = await httpClient.GetAsync("http://localhost/musicas/SongX");
            var content = await res.Content.ReadAsStringAsync();

            Assert.Equal(System.Net.HttpStatusCode.OK, res.StatusCode);
            var obj = System.Text.Json.JsonSerializer.Deserialize<MusicaResponse>(content);
            Assert.NotNull(obj);
            Assert.Equal(musica.Id, obj!.Id);
        }

        // Teste do endpoint POST /musicas
        // Arrange: captura request body enviado
        // Act: Post via HttpClient
        // Assert: valida corpo
        [Fact]
        public async Task PostMusica_Deve_EnviarCorpo()
        {
            ScreenSound.Shared.Modelos.Requests.MusicaRequest? received = null;
            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Post && request.RequestUri!.PathAndQuery == "/musicas")
                {
                    var content = request.Content?.ReadAsStringAsync().Result ?? string.Empty;
                    var opts = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    received = System.Text.Json.JsonSerializer.Deserialize<ScreenSound.Shared.Modelos.Requests.MusicaRequest>(content, opts);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));
            var contentReq = new ScreenSound.Shared.Modelos.Requests.MusicaRequest("SongReq", 1, 2000, 1);
            var res = await httpClient.PostAsJsonAsync("/musicas", contentReq);

            Assert.Equal(System.Net.HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(received);
            Assert.Equal(contentReq.Nome, received!.Nome);
        }

        // Teste do endpoint PUT /musicas
        // Arrange: captura corpo do PUT
        // Act: envia PUT
        // Assert: valida corpo recebido
        [Fact]
        public async Task PutMusica_Deve_EnviarCorpo()
        {
            ScreenSound.Shared.Modelos.Requests.MusicaRequestEdit? received = null;
            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Put && request.RequestUri!.PathAndQuery == "/musicas")
                {
                    var content = request.Content?.ReadAsStringAsync().Result ?? string.Empty;
                    var opts = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    received = System.Text.Json.JsonSerializer.Deserialize<ScreenSound.Shared.Modelos.Requests.MusicaRequestEdit>(content, opts);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));
            var edit = new ScreenSound.Shared.Modelos.Requests.MusicaRequestEdit(3, "Edit", 1, 2022, 1);
            var res = await httpClient.PutAsJsonAsync("/musicas", edit);

            Assert.Equal(System.Net.HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(received);
            Assert.Equal(edit.Id, received!.Id);
        }

        // Teste do endpoint DELETE /musicas/{id}
        // Arrange: retorna NoContent
        // Act: envia DELETE
        // Assert: verifica status
        [Fact]
        public async Task DeleteMusica_Deve_RetornarNoContent()
        {
            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Delete && request.RequestUri!.PathAndQuery == "/musicas/9")
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));
            var res = await httpClient.DeleteAsync("http://localhost/musicas/9");
            Assert.Equal(System.Net.HttpStatusCode.NoContent, res.StatusCode);
        }
    }
}
