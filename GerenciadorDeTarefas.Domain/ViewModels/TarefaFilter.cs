

namespace GerenciadorDeTarefas.Domain.ViewModels;

public sealed record TarefaFilter
{
    public string Search { get; set; }
    public string Predicate { get; set; } = "Titulo";
    public bool Decrescente { get; set; }
}
