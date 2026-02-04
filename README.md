# Sistema de Gerenciamento de Tarefas

## Descrição
Este é um sistema de gerenciamento de tarefas desenvolvido em **.NET 8.0**.  
A aplicação permite criar, visualizar, atualizar e excluir tarefas, gravando os dados em um **arquivo JSON**.  
O projeto também implementa filtros e ordenação na listagem de tarefas, facilitando a visualização e organização das mesmas.

---

## Funcionalidades

### CRUD de Tarefas
- **Criar:** Adição de novas tarefas com título, descrição e status.
- **Listar:** Visualização de todas as tarefas cadastradas.
- **Atualizar:** Edição de tarefas existentes (título, descrição e status).
- **Excluir:** Remoção de tarefas da lista.

### Filtros e Ordenação (Desejável implementado)
- Filtro por **título, descrição ou status**.
- Ordenação por **título, descrição, status ou data de criação**.
- Ordenação crescente ou decrescente.

### Persistência
- Todos os dados são armazenados em **arquivo JSON**.
- Operações de leitura e gravação garantem persistência entre execuções.

### Soft Delete (Lixeira) e Job de Limpeza 
- Ao excluir uma tarefa, ela é movida para a lixeira em vez de ser removida imediatamente.  
- A tela de Lixeira permite visualizar as tarefas excluídas e restaurá-las se necessário.  
- A exclusão definitiva pode ser feita manualmente ou através de um job de limpeza que remove os itens excluídos há mais de 30 dias.

---

## Tecnologias utilizadas
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- C#  
- Razor Pages / MVC
- Bootstrap 5 (para interface)  
- JSON para armazenamento de dados  

---

## Requisitos
- .NET SDK 8.0 ou superior  
- Navegador moderno para interface web  

---

## Como executar localmente

1. Clone o repositório:
git clone https://github.com/MarceloDeSouzaSj/GerenciadorDeTarefas.git

2. Restaure as dependências do .NET:
dotnet restore

3. Compile o projeto:
dotnet build

4. Execute a aplicação:
dotnet run --project GerenciadorDeTarefas.Web

5. Abra o navegador e acesse a URL informada no terminal, geralmente:
   
https://localhost:7170 ou http://localhost:5121

7. Pronto! A aplicação estará rodando localmente, permitindo:
- Criar novas tarefas  
- Editar tarefas existentes  
- Excluir tarefas  
- Listar, filtrar e ordenar tarefas  

> **Observação:** O arquivo JSON já vem criado no projeto e será usado automaticamente para armazenar e recuperar os dados.

## Hospedagem gratuita

O projeto também está hospedado em ambiente gratuito no **Render**.  
Você pode acessar a aplicação online pelo link:

[Gerenciador de Tarefas no Render](https://gerenciadordetarefas-9vhx.onrender.com/?Search=&Predicate=Titulo&Decrescente=false)

> **Importante:** Como o Render oferece hospedagem gratuita, o aplicativo pode entrar em **sleep mode** quando não houver acesso por algum tempo.  
> Ao acessar novamente, pode levar alguns segundos para “acordar” e carregar a aplicação, mas todas as funcionalidades permanecem disponíveis.  
> Quando isso acontece, os dados armazenados no arquivo JSON são **perdidos**.  
> Durante a execução ativa, o arquivo JSON armazena temporariamente as tarefas, mas não é persistente entre reinícios ou dormência do servidor.

