using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Tests.Unitarios;

// Testes unitários para a classe DAL<Musica>
// Cada teste é independente e cobre um único método do DAL
[Trait("Categoria", "Unitário")]
public class MusicaDALTests
{
    private DbContextOptions<ScreenSoundContext> CreateOptions()
    {
        return new DbContextOptionsBuilder<ScreenSoundContext>()
            .UseLazyLoadingProxies()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    // Verifica se o método Adicionar insere corretamente uma música no contexto
    [Fact]
    [Trait("Categoria", "Unitário")]
    public void Adicionar_Musica_Deve_AdicionarEntidade()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);

        // preparar entidades relacionadas
        var artista = new Artista("Artist", "Bio") { FotoPerfil = "p.jpg" };
        var genero = new Genero { Nome = "Rock", Descricao = "Rock music" };
        context.Artistas.Add(artista);
        context.Generos.Add(genero);
        context.SaveChanges();

        var dal = new DAL<Musica>(context);
        var musica = new Musica("Song") { ArtistaId = artista.Id, GeneroId = genero.Id };
        dal.Adicionar(musica);

        context.Musicas.Should().ContainSingle(m => m.Nome == "Song");
    }

    // Verifica se Listar retorna todas as músicas
    [Fact]
    [Trait("Categoria", "Unitário")]
    public void Listar_Deve_RetornarTodasMusicas()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);

        context.Musicas.Add(new Musica("M1"));
        context.Musicas.Add(new Musica("M2"));
        context.SaveChanges();

        var dal = new DAL<Musica>(context);
        var lista = dal.Listar().ToList();

        lista.Should().HaveCount(2);
        lista.Select(m => m.Nome).Should().Contain(new[] { "M1", "M2" } as System.Collections.Generic.IEnumerable<string?>);
    }

    // Verifica se Listar com include carrega Artista e Genero relacionados
    [Fact]
    [Trait("Categoria", "Unitário")]
    public void ListarComInclude_Deve_CarregarArtistaEGenero()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);

        var artista = new Artista("A", "b") { FotoPerfil = "p.jpg" };
        var genero = new Genero { Nome = "G", Descricao = "d" };
        context.Artistas.Add(artista);
        context.Generos.Add(genero);
        context.SaveChanges();

        var musica = new Musica("Included") { ArtistaId = artista.Id, GeneroId = genero.Id };
        context.Musicas.Add(musica);
        context.SaveChanges();

        var dal = new DAL<Musica>(context);
        var lista = dal.Listar("Artista").ToList();

        lista.Should().ContainSingle().Which.Artista.Should().NotBeNull();
    }

    // Verifica se RecuperarPor encontra a música pela condição
    [Fact]
    [Trait("Categoria", "Unitário")]
    public void RecuperarPor_Deve_RetornarMusicaCorrespondente()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        context.Musicas.Add(new Musica("FindMe"));
        context.SaveChanges();

        var dal = new DAL<Musica>(context);
        var encontrado = dal.RecuperarPor(m => m.Nome == "FindMe");

        encontrado.Should().NotBeNull();
        encontrado!.Nome.Should().Be("FindMe");
    }

    // Verifica se Atualizar persiste alteração na música
    [Fact]
    [Trait("Categoria", "Unitário")]
    public void Atualizar_Deve_PersistirAlteracoes()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        var musica = new Musica("OldName");
        context.Musicas.Add(musica);
        context.SaveChanges();

        var dal = new DAL<Musica>(context);
        musica.Nome = "NewName";
        dal.Atualizar(musica);

        dal.RecuperarPor(m => m.Nome == "NewName").Should().NotBeNull();
    }

    // Verifica se Deletar remove a música do contexto
    [Fact]
    [Trait("Categoria", "Unitário")]
    public void Deletar_Deve_RemoverEntidade()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        var musica = new Musica("ToDelete");
        context.Musicas.Add(musica);
        context.SaveChanges();

        var dal = new DAL<Musica>(context);
        dal.Deletar(musica);

        dal.Listar().Should().BeEmpty();
    }
}
