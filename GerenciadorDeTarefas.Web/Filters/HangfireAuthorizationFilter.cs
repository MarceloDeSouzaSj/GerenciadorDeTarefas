using Hangfire.Dashboard;

namespace GerenciadorDeTarefas.Web.Filters;

public sealed class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => true;
}