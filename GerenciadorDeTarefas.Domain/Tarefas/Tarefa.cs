
namespace GerenciadorDeTarefas.Domain.Tarefas;

public class Tarefa
{
    public Guid Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public required string Status { get; set; }
    public required DateTime DataDeCriacao { get; set; }
}
