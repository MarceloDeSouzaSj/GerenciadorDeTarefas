using GerenciadorDeTarefas.Domain.Infrastructure;
using GerenciadorDeTarefas.Domain.ViewModels;

namespace GerenciadorDeTarefas.Domain.Tarefas;

public interface ITarefaService
{
    Task Salvar(TarefaViewModel request);
    Task<TarefaViewModel?> BuscarPorId(Guid id);
    Task Remover(Guid id);
    Task<List<Tarefa>> Listar();
}

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaService(ITarefaRepository tarefaRepository) => _tarefaRepository = tarefaRepository;

    public Task Salvar(TarefaViewModel request)
    {
        DateTime hoje = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        var tarefa = new Tarefa
        {
            Id = request.Id ?? Guid.NewGuid(),
            Titulo = request.Titulo,
            Descricao = request.Descricao,
            Status = request.Status,
            DataDeCriacao = request.DataDeCriacao.HasValue ? request.DataDeCriacao.Value : hoje
        };

        if(!request.Id.HasValue)
            return _tarefaRepository.Cadastrar(tarefa);
        else
            return _tarefaRepository.Atualizar(tarefa);
    }

    public async Task<TarefaViewModel?> BuscarPorId(Guid id)
    {
        var tarefa = await _tarefaRepository.BuscarPorId(id);
        if (tarefa == null)
            return null;

        var tarefaResponse = new TarefaViewModel
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            Status = tarefa.Status,
            DataDeCriacao = tarefa.DataDeCriacao,
        };

        return tarefaResponse;
    }

    public async Task Remover(Guid id) => await _tarefaRepository.Remover(id);

    public async Task<List<Tarefa>> Listar() => await _tarefaRepository.BuscarTodos();
}
