using Moq;
using System.Text;
using System.Net.Http.Json;
using ScreenSound.Shared.Modelos.Requests;
using ScreenSound.Shared.Modelos.Response;

namespace ScreenSound.Tests.Integracao
{
    [Trait("Categoria", "Integração")]
    public  class ArtistasAPITest
    {
        private static HttpClient CreateClient(Func<HttpRequestMessage, HttpResponseMessage> handler, Uri? baseAddress = null)
        {
            var messageHandler = new StubHttpMessageHandler(handler);
            var client = new HttpClient(messageHandler);
            if (baseAddress != null) 
                client.BaseAddress = baseAddress;
            return client;
        }

        // Teste do endpoint GET /artistas (lista)
        // Arrange: simula resposta JSON com lista de artistas
        // Act: chama ArtistasAPI.GetArtistasAsync
        // Assert: valida se a lista retornada corresponde à simulada
        [Fact]
        [Trait("Categoria", "Integração")]
        public async Task GetArtistasAsync_DeveChamarEndpointECapturarLista()
        {
            // Arrange
            var artistasEsperados = new List<ArtistaResponse>
            {
                new ArtistaResponse(1, "Artista 1", "", null),
                new ArtistaResponse(2, "Artista 2", "", null)
            };

            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Get && request.RequestUri!.PathAndQuery == "/artistas")
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(artistasEsperados);
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

            var api = new ScreenSound.WebAssembly.Services.ArtistasAPI(factory.Object);
            
            // Act
            var artistasObtidos = await api.GetArtistasAsync();
            // Assert
            Assert.NotNull(artistasObtidos);
            
            Assert.Equal(artistasEsperados.Count, artistasObtidos.Count);
            for (int i = 0; i < artistasEsperados.Count; i++)
            {
                Assert.Equal(artistasEsperados[i].Id, artistasObtidos.ElementAt(i).Id);
                Assert.Equal(artistasEsperados[i].Nome, artistasObtidos.ElementAt(i).Nome);                
            }
        }

        // Teste do endpoint POST /artistas (criação)
        // Arrange: captura o corpo da requisição enviada ao endpoint
        // Act: chama ArtistasAPI.AddArtistaAsync
        // Assert: valida se o corpo recebido corresponde ao objeto enviado
        [Fact]
        [Trait("Category", "Integração")]
        public async Task AddArtistaAsync_Deve_ChamarEndpointPostEEnviarCorpo()
        {
            // Arrange
            ArtistaRequest? received = null;
            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Post && request.RequestUri!.PathAndQuery == "/artistas")
                {
                    var content = request.Content?.ReadAsStringAsync().Result ?? string.Empty;
                    var opts = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    received = System.Text.Json.JsonSerializer.Deserialize<ArtistaRequest>(content, opts);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(f => f.CreateClient("API")).Returns(httpClient);

            var api = new ScreenSound.WebAssembly.Services.ArtistasAPI(factory.Object);

            var request = new ArtistaRequest("Novo", "bio", "base64");

            // Act
            await api.AddArtistaAsync(request);

            // Assert
            Assert.NotNull(received);
            Assert.Equal(request.Nome, received!.Nome);
            Assert.Equal(request.Bio, received.Bio);
            Assert.Equal(request.FotoPerfil, received.FotoPerfil);
        }

        // Teste do endpoint GET /artistas/{nome} (recuperar por nome)
        // Arrange: simula retorno de um artista específico
        // Act: faz GET direto via HttpClient para endpoint por nome
        // Assert: valida se o conteúdo retornado corresponde ao artista simulado
        [Fact]
        [Trait("Category", "Integração")]
        public async Task GetArtistaPorNome_Deve_RetornarArtista()
        {
            // Arrange
            var artista = new ArtistaResponse(10, "NomeX", "bio", null);
            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Get && request.RequestUri!.PathAndQuery == "/artistas/NomeX")
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(artista);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));

            // Act
            var res = await httpClient.GetAsync("http://localhost/artistas/NomeX");
            var content = await res.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, res.StatusCode);
            var obj = System.Text.Json.JsonSerializer.Deserialize<ArtistaResponse>(content);
            Assert.NotNull(obj);
            Assert.Equal(artista.Id, obj!.Id);
            Assert.Equal(artista.Nome, obj.Nome);
        }

        // Teste do endpoint PUT /artistas (atualização)
        // Arrange: captura o PUT enviado e retorna OK
        // Act: envia PUT via HttpClient
        // Assert: verifica que corpo enviado contém as alterações
        [Fact]
        [Trait("Category", "Integração")]
        public async Task PutArtista_Deve_EnviarCorpoEReceberOk()
        {
            // Arrange
            ScreenSound.Shared.Modelos.Requests.ArtistaRequestEdit? received = null;
            var handler = new Func<HttpRequestMessage, HttpResponseMessage>(request =>
            {
                if (request.Method == HttpMethod.Put && request.RequestUri!.PathAndQuery == "/artistas")
                {
                    var content = request.Content?.ReadAsStringAsync().Result ?? string.Empty;
                    var opts = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    received = System.Text.Json.JsonSerializer.Deserialize<ScreenSound.Shared.Modelos.Requests.ArtistaRequestEdit>(content, opts);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            });

            var httpClient = CreateClient(handler, new Uri("http://localhost/"));

            var edit = new ScreenSound.Shared.Modelos.Requests.ArtistaRequestEdit(5, "NomeEdit", "bioedit", null);

            // Act
            var res = await httpClient.PutAsJsonAsync("/artistas", edit);

    sealed class StubHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _handler;
        public StubHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> handler) => _handler = handler;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
          => Task.FromResult(_handler(request));
    }
}
