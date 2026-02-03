using GerenciadorDeTarefas.Domain.Tarefas;
using GerenciadorDeTarefas.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefas.Web.Controllers;

public class TarefaController : Controller
{
    private readonly ITarefaService _tarefaService;

    public TarefaController(ITarefaService tarefaService) => _tarefaService = tarefaService;

    public async Task<IActionResult> Index(TarefaFilter filter)
    {
        var tarefas = await _tarefaService.Listar();

        tarefas = FiltrarTarefas(filter, tarefas);

        ViewBag.Tarefas = tarefas;
        return View(filter);
    }

    public async Task<IActionResult> CreateAsync(Guid id)
    {
        var tarefa = await _tarefaService.BuscarPorId(id);
        return View(tarefa);
    }

    private List<Tarefa> FiltrarTarefas(TarefaFilter filter, List<Tarefa> tarefas)
    {
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            tarefas = tarefas
                .Where(q => q.Titulo.Contains(filter.Search, StringComparison.OrdinalIgnoreCase) ||
                            q.Descricao.Contains(filter.Search, StringComparison.OrdinalIgnoreCase) ||
                            q.Status.Contains(filter.Search, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        switch (filter.Predicate)
        {
            case "Titulo":
                tarefas = filter.Decrescente
                    ? tarefas.OrderByDescending(q => q.Titulo).ToList()
                    : tarefas.OrderBy(q => q.Titulo).ToList();
                break;
            case "Descricao":
                tarefas = filter.Decrescente
                    ? tarefas.OrderByDescending(q => q.Descricao).ToList()
                    : tarefas.OrderBy(q => q.Descricao).ToList();
                break;
            case "DataDeCriacao":
                tarefas = filter.Decrescente
                    ? tarefas.OrderByDescending(q => q.DataDeCriacao).ToList()
                    : tarefas.OrderBy(q => q.DataDeCriacao).ToList();
                break;
            case "Status":
                tarefas = filter.Decrescente
                    ? tarefas.OrderByDescending(q => q.Status).ToList()
                    : tarefas.OrderBy(q => q.Status).ToList();
                break;
        }

        return tarefas;
    }

}
