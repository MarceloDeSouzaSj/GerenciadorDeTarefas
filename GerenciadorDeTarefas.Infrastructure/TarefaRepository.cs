using GerenciadorDeTarefas.Domain.Infrastructure;
using GerenciadorDeTarefas.Domain.Tarefas;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace GerenciadorDeTarefas.Infrastructure;

public class TarefaRepository : ITarefaRepository
{
    private readonly string _caminhoDoArquivo;

    public TarefaRepository(IHostEnvironment hostEnvironment)
    {
        _caminhoDoArquivo = Path.Combine(
       hostEnvironment.ContentRootPath,
       "Data/dados.json"
   );
    }

    public async Task<List<Tarefa>> BuscarTodos()
    {
        var json = await File.ReadAllTextAsync(_caminhoDoArquivo);
        return JsonSerializer.Deserialize<List<Tarefa>>(json) ?? new List<Tarefa>();
    }

    public async Task<Tarefa?> BuscarPorId(Guid id)
    {
        var tarefas = await BuscarTodos();

        return tarefas.FirstOrDefault(q => q.Id == id);
    }

    private async Task SalvarDados(List<Tarefa> tarefas)
    {
        var json = JsonSerializer.Serialize(tarefas);

        await File.WriteAllTextAsync(_caminhoDoArquivo, json);
    }

    public async Task Cadastrar(Tarefa tarefa)
    {
        var tarefas = await BuscarTodos();
        tarefas.Add(tarefa);

        await SalvarDados(tarefas);
    }

    public async Task Atualizar(Tarefa tarefaAtualizada)
    {
        var tarefas = await BuscarTodos();
        var indice = tarefas.FindIndex(t => t.Id == tarefaAtualizada.Id);
        tarefas[indice] = tarefaAtualizada;

        await SalvarDados(tarefas);
    }

    public async Task Remover(Guid id)
    {
        DateTime hoje = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        var tarefas = await BuscarTodos();
        var tarefa = tarefas.First(t => t.Id == id);
        tarefa.ExcluidoEm = hoje;

        await SalvarDados(tarefas);
    }

    public async Task Restaurar(Guid id)
    {
        var tarefas = await BuscarTodos();
        var tarefa = tarefas.First(t => t.Id == id);
        tarefa.ExcluidoEm = null;

        await SalvarDados(tarefas);
    }

    public async Task RemoverDefinitivamente(Guid id)
    {
        var tarefas = await BuscarTodos();
        var tarefa = tarefas.First(t => t.Id == id);
        tarefas.Remove(tarefa);

        await SalvarDados(tarefas);
    }

    public async Task RemoverDefinitivamenteEmLote(List<Guid> ids)
    {
        var tarefas = await BuscarTodos();
        tarefas.RemoveAll(t => ids.Contains(t.Id));

        await SalvarDados(tarefas);
    }
}
