using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Tests.Unitarios;

// Testes unitários para a classe DAL<Genero>
// Cada teste é independente e cobre um único método do DAL
[Trait("Categoria", "Unitário")]
public class GeneroDALTests
{
    private DbContextOptions<ScreenSoundContext> CreateOptions()
    {
        return new DbContextOptionsBuilder<ScreenSoundContext>()
            .UseLazyLoadingProxies()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    // Verifica se Adicionar insere corretamente um gênero
    [Fact]
    public void Adicionar_Genero_Deve_AdicionarEntidade()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        var dal = new DAL<Genero>(context);

        var genero = new Genero { Nome = "Pop", Descricao = "desc" };
        dal.Adicionar(genero);

        context.Generos.Should().ContainSingle(g => g.Nome == "Pop");
    }

    // Verifica se Listar retorna todos os gêneros
    [Fact]
    public void Listar_Deve_RetornarTodosGeneros()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        context.Generos.Add(new Genero { Nome = "G1" });
        context.Generos.Add(new Genero { Nome = "G2" });
        context.SaveChanges();

        var dal = new DAL<Genero>(context);
        var lista = dal.Listar().ToList();

        lista.Should().HaveCount(2);
        lista.Select(g => g.Nome).Should().Contain(new[] { "G1", "G2" } as System.Collections.Generic.IEnumerable<string?>);
    }

    // Verifica se Listar com include carrega as músicas relacionadas
    [Fact]
    public void ListarComInclude_Deve_CarregarMusicas()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);

        var genero = new Genero { Nome = "Pop", Descricao = "desc" };
        genero.Musicas = new System.Collections.Generic.List<Musica> { new Musica("Hit") };
        context.Generos.Add(genero);
        context.SaveChanges();

        var dal = new DAL<Genero>(context);
        var lista = dal.Listar("Musicas").ToList();

        lista.Should().ContainSingle();
        lista.First().Musicas.Should().ContainSingle(m => m.Nome == "Hit");
    }

    // Verifica se RecuperarPor encontra o gênero pela condição
    [Fact]
    public void RecuperarPor_Deve_RetornarGeneroCorrespondente()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        context.Generos.Add(new Genero { Nome = "Find" });
        context.SaveChanges();

        var dal = new DAL<Genero>(context);
        var encontrado = dal.RecuperarPor(g => g.Nome == "Find");

        encontrado.Should().NotBeNull();
        encontrado!.Nome.Should().Be("Find");
    }

    // Verifica se Atualizar persiste alteração no gênero
    [Fact]
    public void Atualizar_Deve_PersistirAlteracoes()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        var genero = new Genero { Nome = "Old" };
        context.Generos.Add(genero);
        context.SaveChanges();

        var dal = new DAL<Genero>(context);
        genero.Nome = "New";
        dal.Atualizar(genero);

        dal.RecuperarPor(g => g.Nome == "New").Should().NotBeNull();
    }

    // Verifica se Deletar remove o gênero do contexto
    [Fact]
    public void Deletar_Deve_RemoverEntidade()
    {
        var options = CreateOptions();
        using var context = new ScreenSoundContext(options);
        var genero = new Genero { Nome = "ToDelete" };
        context.Generos.Add(genero);
        context.SaveChanges();

        var dal = new DAL<Genero>(context);
        dal.Deletar(genero);

        dal.Listar().Should().BeEmpty();
    }
}
