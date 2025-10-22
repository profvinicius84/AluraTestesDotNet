using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Tests.Unitarios;

// Testes unitários para a classe DAL<Artista>
// Cada teste é independente e cobre um único método do DAL
[Trait("Categoria", "Unitário")]
public class ArtistaDALTests
{
    private DbContextOptions<ScreenSoundContext> CreateOptions()
    {
        return new DbContextOptionsBuilder<ScreenSoundContext>()
            .UseLazyLoadingProxies()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    // Verifica se o método Adicionar insere corretamente um artista no contexto
    [Fact]
    public void Adicionar_Artista_Deve_AdicionarEntidade()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        var dal = new DAL<Artista>(context);

        var artista = new Artista("Nirvana", "Grunge band") { FotoPerfil = "perfil.jpg" };
        dal.Adicionar(artista);

        context.Artistas.Should().ContainSingle(a => a.Nome == "Nirvana");
    }

    // Verifica se o método Listar retorna todos os artistas registrados
    [Fact]
    public void Listar_Deve_RetornarTodosArtistas()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        context.Artistas.Add(new Artista("A1", "b1") { FotoPerfil = "p1.jpg" });
        context.Artistas.Add(new Artista("A2", "b2") { FotoPerfil = "p2.jpg" });
        context.SaveChanges();

        var dal = new DAL<Artista>(context);
        var lista = dal.Listar().ToList();

        lista.Should().HaveCount(2);
        lista.Select(a => a.Nome).Should().Contain(new[] { "A1", "A2" } as System.Collections.Generic.IEnumerable<string?>);
    }

    // Verifica se Listar com include carrega a coleção de músicas associadas
    [Fact]
    public void ListarComInclude_Deve_CarregarMusicas()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);

        var artista = new Artista("Nirvana", "Bio") { FotoPerfil = "p.jpg" };
        artista.AdicionarMusica(new Musica("Song1"));
        context.Artistas.Add(artista);
        context.SaveChanges();

        var dal = new DAL<Artista>(context);
        var listaIncluida = dal.Listar("Musicas").ToList();

        listaIncluida.Should().ContainSingle();
        listaIncluida.First().Musicas.Should().ContainSingle(m => m.Nome == "Song1");
    }

    // Verifica se RecuperarPor encontra o artista esperado pela condição
    [Fact]
    public void RecuperarPor_Deve_RetornarArtistaCorrespondente()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        context.Artistas.Add(new Artista("Match", "bio") { FotoPerfil = "p.jpg" });
        context.SaveChanges();

        var dal = new DAL<Artista>(context);
        var encontrado = dal.RecuperarPor(a => a.Nome == "Match");

        encontrado.Should().NotBeNull();
        encontrado!.Nome.Should().Be("Match");
    }

    // Verifica se o método Atualizar persiste as alterações realizadas na entidade
    [Fact]
    public void Atualizar_Deve_PersistirAlteracoes()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        var artista = new Artista("Old", "bio") { FotoPerfil = "p.jpg" };
        context.Artistas.Add(artista);
        context.SaveChanges();

        var dal = new DAL<Artista>(context);
        artista.Nome = "New";
        dal.Atualizar(artista);

        dal.RecuperarPor(a => a.Nome == "New").Should().NotBeNull();
    }

    // Verifica se o método Deletar remove a entidade do contexto
    [Fact]
    public void Deletar_Deve_RemoverEntidade()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        var artista = new Artista("ToDelete", "bio") { FotoPerfil = "p.jpg" };
        context.Artistas.Add(artista);
        context.SaveChanges();

        var dal = new DAL<Artista>(context);
        dal.Deletar(artista);

        dal.Listar().Should().BeEmpty();
    }
}
