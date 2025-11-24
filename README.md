# 📡 Web API -- Sistema de Streaming Multimídia

Este projeto implementa a **Web API oficial** do sistema de streaming
multimídia desenvolvido para o **PIM VIII -- UNIP**, responsável por
gerenciar usuários, playlists, conteúdos, criadores e itens de playlist.

A API foi construída com **ASP.NET Core**, **Entity Framework Core** e
segue princípios de arquitetura limpa e boas práticas de
desenvolvimento.

## 🚀 Tecnologias Utilizadas

-   .NET 9 / ASP.NET Core
-   Entity Framework Core
-   SQL Server
-   Swagger / OpenAPI
-   Dependency Injection nativo do ASP.NET Core


### Domain

Entidades do diagrama de classes: - Usuário - Playlist - Conteúdo -
Criador - ItemPlaylist

### Infrastructure

-   EF Core
-   Mapeamentos
-   PlaylistRepository (CRUD)

### WebApi

-   Endpoints REST
-   Swagger
-   Autenticação/autorização

## 🗃 Banco de Dados

Acesso via Entity Framework Core.

### Migrações

    dotnet ef migrations add InitialCreate
    dotnet ef database update

## ✔️ Objetivo no PIM

Atende às disciplinas: - Programação Orientada a Objetos II -
Desenvolvimento de Software para a Internet

Inclui CRUD, Repository Pattern, EF Core, Swagger.

## 📚 Fontes Oficiais

-   ASP.NET Core Web API:
    https://learn.microsoft.com/aspnet/core/web-api/
-   Entity Framework Core: https://learn.microsoft.com/ef/core/
-   Security: https://learn.microsoft.com/aspnet/core/security/
-   Swagger:
    https://learn.microsoft.com/aspnet/core/tutorials/getting-started-with-swashbuckle
