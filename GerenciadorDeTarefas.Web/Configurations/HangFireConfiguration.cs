using GerenciadorDeTarefas.Domain.Crons;
using GerenciadorDeTarefas.Web.Filters;
using Hangfire;
using Hangfire.MemoryStorage;

namespace GerenciadorDeTarefas.Web.Configurations;

public static class HangfireConfiguration
{
    public static void AddHangfireConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(options => { options.UseMemoryStorage(); });

        builder.Services.AddHangfireServer();
    }

    public static void UseHangfireConfiguration(this WebApplication app)
    {
        app.UseHangfireDashboard(options: new DashboardOptions
        {
            Authorization = [new HangfireAuthorizationFilter()],
            DashboardTitle = $"Gerenciamento de atividades - Hangfire"
        });

        RecurringJob.AddOrUpdate<ICronRemovedoraDeImagem>(
            "RemovedorDeImagem",
            cron => cron.ExecutarAsync(),
            "0 2 * * *"
        );
    }
}