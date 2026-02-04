
using GerenciadorDeTarefas.Domain.Tarefas;
using GerenciadorDeTarefas.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefas.Web.Api;

[Route("api/[controller]")]
[ApiController]
public class TarefasController : ControllerBase
{
    private readonly ITarefaService _tarefaService;

    public TarefasController(ITarefaService tarefaService) => _tarefaService = tarefaService;

    [HttpPost]
    public async Task<IActionResult> Salvar([FromForm] TarefaViewModel request)
    {
        await _tarefaService.Salvar(request);

        return Redirect("/Tarefa/Index");
    }

    [HttpPost, Route("Remover/{id:guid}")]  
    public async Task<IActionResult> Remover(Guid id)
    {
        await _tarefaService.Remover(id);
        
        return Redirect("/Tarefa/Index");
    }

    [HttpPost, Route("Restaurar/{id:guid}")]  
    public async Task<IActionResult> Restaurar(Guid id)
    {
        await _tarefaService.Restaurar(id);
        
        return Redirect("/Tarefa/Lixeira");
    }

    [HttpPost, Route("RemoverDefinitivamente/{id:guid}")]  
    public async Task<IActionResult> RemoverDefinitivamente(Guid id)
    {
        await _tarefaService.RemoverDefinitivamente(id);
        
        return Redirect("/Tarefa/Lixeira");
    }
}
