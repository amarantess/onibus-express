# OniBus Express - Backend

API para busca, reserva, consulta e cancelamento de passagens rodoviarias.

Este repositório implementa o backend do desafio OniBus Express. O frontend não foi implementado nesta entrega.

## Tecnologias

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Docker e Docker Compose
- xUnit
- FluentValidation
- Swagger / Swashbuckle

## Arquitetura

O backend foi organizado em camadas:

- `OnibusExpress.Domain`: entidades, requests, responses, DTOs e interfaces de repositories.
- `OnibusExpress.Application`: use cases, validators e serviços de aplicação.
- `OnibusExpress.Infrastructure`: EF Core, repositories, unit of work, migrations, seed e exceptions customizadas.
- `OnibusExpress.Api`: controllers, middleware global de exceptions, Swagger e bootstrap HTTP.
- `OnibusExpress.Tests`: testes unitários e testes de integração leves com SQLite in-memory.

Estrutura principal:

```text
backend/
  src/
    OnibusExpress.Api/
    OnibusExpress.Application/
    OnibusExpress.Domain/
    OnibusExpress.Infrastructure/
  tests/
    OnibusExpress.Tests/
  Dockerfile
docker-compose.yml
```

## Como Rodar Com Docker

Pré-requisito:

- Docker Desktop

Na raiz do repositório, execute:

```powershell
docker-compose up --build
```

Esse comando sobe:

- API ASP.NET Core
- SQL Server
- migrations automaticamente no startup
- seed inicial de rotas e viagens

Acesse o Swagger:

```text
http://localhost:8080/swagger
```

Para parar os containers:

```powershell
docker-compose down
```

Para parar e remover também o volume do banco:

```powershell
docker-compose down -v
```

## Como Rodar Localmente Sem Docker

Pré-requisitos:

- .NET 10 SDK
- SQL Server local

Configure a connection string em:

```text
backend/src/OnibusExpress.Api/appsettings.Development.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=OnibusExpressDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Restaure as dependências:

```powershell
dotnet restore backend/OnibusExpress.slnx
```

Restaure a ferramenta local do EF:

```powershell
dotnet tool restore
```

Aplique as migrations via CLI:

```powershell
dotnet tool run dotnet-ef database update `
  --project backend/src/OnibusExpress.Infrastructure/OnibusExpress.Infrastructure.csproj `
  --startup-project backend/src/OnibusExpress.Api/OnibusExpress.Api.csproj
```

Alternativa pelo Package Manager Console do Visual Studio:

- Projeto padrão: `OnibusExpress.Infrastructure`
- Projeto de inicialização: `OnibusExpress.Api`
- Comando:

```powershell
Update-Database
```

Suba a API:

```powershell
dotnet run --project backend/src/OnibusExpress.Api/OnibusExpress.Api.csproj
```

Swagger local:

```text
http://localhost:5223/swagger
https://localhost:7180/swagger
```

## Endpoints Implementados

```http
GET    /rotas
GET    /viagens?origin=&destination=&travelDate=
GET    /viagens/{id}
POST   /reservas
GET    /reservas/{code}
DELETE /reservas/{code}
```

## Regras Implementadas

- CPF validado por formato e dígito verificador.
- Não permite reservar assento já ocupado.
- Não permite reservar viagem já realizada.
- Gera código de reserva único e legível, no formato `ABC-12345`.
- Permite cancelamento apenas até 2 horas antes da partida.

## Banco de Dados

O projeto usa EF Core Code First com SQL Server.

Inclui:

- migrations versionadas
- aplicação automática de migrations no startup
- seed inicial de rotas e viagens
- `IUnitOfWork` para persistência
- repositories sem `SaveChanges`

Os assentos são derivados das reservas existentes. Nesta versão não existe uma entidade `Seat`.

## Tratamento de Erros

Os controllers não usam `try/catch`.

Erros são tratados por um middleware global, que converte exceptions em uma resposta padronizada:

```json
{
  "errors": [
    "Mensagem de erro"
  ]
}
```

Status codes principais:

- `400 Bad Request`
- `404 Not Found`
- `409 Conflict`
- `500 Internal Server Error`

## Testes

Execute:

```powershell
dotnet test backend/OnibusExpress.slnx
```

A suíte cobre:

- validação de CPF
- geração de código legível
- criação de reserva
- assento ocupado
- viagem já realizada
- cancelamento permitido
- cancelamento fora do prazo
- reserva já cancelada
- colisão de código de reserva

Estratégia:

- testes unitários para serviços puros
- testes de integração de use cases com EF Core e SQLite in-memory
- sem dependência de SQL Server ou Docker nos testes

## Decisões de Arquitetura

- Separação em camadas seguindo Clean Architecture.
- Requests, responses, DTOs e interfaces de repository ficam em `Domain`.
- Use cases e validators ficam em `Application`.
- Implementações de repositories, EF Core, migrations, seed e exceptions ficam em `Infrastructure`.
- Controllers ficam simples e delegam regras para os use cases.
- Connection string do Docker é injetada via variável de ambiente `ConnectionStrings__DefaultConnection`.
- Migrations e seed rodam automaticamente quando a API sobe.
