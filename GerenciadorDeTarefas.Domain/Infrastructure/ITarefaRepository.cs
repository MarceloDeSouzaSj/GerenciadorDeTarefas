
using GerenciadorDeTarefas.Domain.Tarefas;

namespace GerenciadorDeTarefas.Domain.Infrastructure;

public interface ITarefaRepository
{
    Task<List<Tarefa>> BuscarTodos();
    Task<Tarefa?> BuscarPorId(Guid id);
    Task Cadastrar(Tarefa tarefa);
    Task Atualizar(Tarefa tarefa);
    Task Remover(Guid id);
    Task Restaurar(Guid id);
    Task RemoverDefinitivamente(Guid id);
    Task RemoverDefinitivamenteEmLote(List<Guid> ids);
}
