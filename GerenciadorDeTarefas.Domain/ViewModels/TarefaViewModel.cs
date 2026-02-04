
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeTarefas.Domain.ViewModels;

public class TarefaViewModel
{
    public Guid? Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public required string Status { get; set; }
    public DateTime? DataDeCriacao { get; set; }
    public DateTime? ExcluidoEm { get; set; }
}
