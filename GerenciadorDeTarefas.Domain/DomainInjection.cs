using GerenciadorDeTarefas.Domain.Infrastructure;
using GerenciadorDeTarefas.Domain.Tarefas;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorDeTarefas.Domain;

public static class DomainInjection
{
    private static IServiceCollection _service = null!;

    public static void AddDomainInjection(this IServiceCollection service)
    {
        _service = service;

        _service.AddTransient<ITarefaService, TarefaService>();
    }

}
