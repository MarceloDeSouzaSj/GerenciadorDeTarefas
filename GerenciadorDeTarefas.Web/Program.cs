using GerenciadorDeTarefas.Domain.Infrastructure;
using GerenciadorDeTarefas.Infrastructure;
using GerenciadorDeTarefas.Domain;
using GerenciadorDeTarefas.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddDomainInjection();
builder.AddHangfireConfiguration();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseHangfireConfiguration();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tarefa}/{action=Index}/{id?}");

app.Run();
