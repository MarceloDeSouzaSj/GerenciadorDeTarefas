using GerenciadorDeTarefas.Domain.Infrastructure;

namespace GerenciadorDeTarefas.Domain.Crons;
public interface ICronRemovedoraDeImagem 
{
    Task ExecutarAsync();
}
public class CronRemovedoraDeImagem : ICronRemovedoraDeImagem
{
    private readonly ITarefaRepository _tarefaRepository;

    public CronRemovedoraDeImagem(ITarefaRepository tarefaRepository) => _tarefaRepository = tarefaRepository;

    public async Task ExecutarAsync()
    {
        DateTime hoje = DateTime.UtcNow;
        var tarefas = await _tarefaRepository.BuscarTodos();

        var tarefasParaRemover = tarefas
            .Where(t => t.ExcluidoEm.HasValue && (hoje - t.ExcluidoEm.Value).TotalDays >= 30)
            .Select(q => q.Id)
            .ToList();

        await _tarefaRepository.RemoverDefinitivamenteEmLote(tarefasParaRemover);
    }
}
