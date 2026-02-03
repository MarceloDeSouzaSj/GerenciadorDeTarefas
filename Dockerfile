# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia arquivos da solution e dos projetos para dentro do container
COPY *.sln ./
RUN ls -l /src
COPY GerenciadorDeTarefas.Web/*.csproj ./GerenciadorDeTarefas.Web/
COPY GerenciadorDeTarefas.Infrastructure/*.csproj ./GerenciadorDeTarefas.Infrastructure/
COPY GerenciadorDeTarefas.Domain/*.csproj ./GerenciadorDeTarefas.Domain/

RUN dotnet restore GerenciadorDeTarefas.Web/GerenciadorDeTarefas.Web.csproj

COPY . .

RUN dotnet publish GerenciadorDeTarefas.Web/GerenciadorDeTarefas.Web.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GerenciadorDeTarefas.Web.dll"]