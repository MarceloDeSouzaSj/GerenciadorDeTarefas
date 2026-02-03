using GerenciadorDeTarefas.Domain.Infrastructure;
using GerenciadorDeTarefas.Domain.Tarefas;
using GerenciadorDeTarefas.Domain.ViewModels;
using NSubstitute;

namespace GerenciamentoDeTarefas.Test;

public class TarefaServiceTest
{
    private readonly ITarefaService _tarefaService;
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaServiceTest()
    {
        _tarefaRepository = Substitute.For<ITarefaRepository>();

        _tarefaService = new TarefaService(_tarefaRepository);
    }

    [Fact]
    public async Task SalvarNovaTarefaAsync_DeveCadastrar()
    {
        var tarefaViewModel = new TarefaViewModel
        {
            Titulo = "Nova Tarefa",
            Descricao = "Descrição da nova tarefa",
            Status = "Pendente"
        };

        await _tarefaService.Salvar(tarefaViewModel);

        await _tarefaRepository.Received(1)
            .Cadastrar(Arg.Is<Tarefa>(t =>
                t.Id != Guid.Empty &&
                t.Titulo == tarefaViewModel.Titulo &&
                t.Status == tarefaViewModel.Status &&
                t.Descricao == tarefaViewModel.Descricao
            ));

        await _tarefaRepository.DidNotReceive()
            .Atualizar(Arg.Any<Tarefa>());
    }

    [Fact]
    public async Task SalvarTarefaExistenteAsync_DeveAtualizar()
    {
        var id = Guid.NewGuid();

        var tarefaViewModel = new TarefaViewModel
        {
            Id = id,
            Titulo = "Tarefa Atualizada",
            Descricao = "Nova descrição",
            Status = "Concluida"
        };

        await _tarefaService.Salvar(tarefaViewModel);

        await _tarefaRepository.Received(1)
            .Atualizar(Arg.Is<Tarefa>(t =>
                t.Id == id &&
                t.Titulo == tarefaViewModel.Titulo
            ));

        await _tarefaRepository.DidNotReceive()
            .Cadastrar(Arg.Any<Tarefa>());
    }

    [Fact]
    public async Task BuscarPorIdAsync_QuandoNaoExiste_DeveRetornarNull()
    {
        var id = Guid.NewGuid();
        _tarefaRepository.BuscarPorId(id).Returns((Tarefa?)null);

        var resultado = await _tarefaService.BuscarPorId(id);

        Assert.Null(resultado);
    }


    [Fact]
    public async Task ListarAsync_DeveRetornarLista()
    {
        var tarefas = new List<Tarefa>
        {
            new Tarefa { Id = Guid.NewGuid(), Titulo = "Tarefa1", DataDeCriacao = DateTime.Now, Descricao = "Descrição1", Status = "Status1" },
            new Tarefa { Id = Guid.NewGuid(), Titulo = "Tarefa2", DataDeCriacao = DateTime.Now, Descricao = "Descrição2", Status = "Status2" }
        };

        _tarefaRepository.BuscarTodos().Returns(tarefas);

        var resultado = await _tarefaService.Listar();

        Assert.Equal(2, resultado.Count);
    }
}

