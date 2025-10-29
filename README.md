# ProvaDev

API para gerenciamento de Clientes, Produtos e Pedidos.

## Tecnologias

- .NET 8
- Entity Framework Core (InMemory Database)
- MediatR
- FluentValidation
- AutoMapper
- Swagger/OpenAPI

## Estrutura do Projeto

```
ProvaDev/
├── ProvaDev.Domain          # Entidades, validações e interfaces
├── ProvaDev.Application     # Casos de uso (CQRS com MediatR)
├── ProvaDev.Infrastructure  # Implementação de repositórios e contexto
├── ProvaDev.WebApi          # Controllers e configuração da API
└── ProvaDev.Domain.Tests    # Testes unitários do domínio
```

A aplicação usa Clean Architecture com separação por camadas. O domínio é isolado e as validações são feitas tanto na camada de aplicação (FluentValidation) quanto no domínio.

## Como Rodar

1. Clone o repositório
2. Navegue até a pasta do projeto Web:
   ```bash
   cd ProvaDev.WebApi
   ```
3. Execute:
   ```bash
   dotnet run
   ```
4. Acesse o Swagger em:
   - http://localhost:5293/swagger

## Observações

- O banco de dados é **InMemory** e é populado automaticamente com alguns dados iniciais ao iniciar a aplicação
- Os dados são perdidos quando a aplicação é reiniciada
- Validações de telefone aceitam apenas números (10 ou 11 dígitos)

## Endpoints Principais

- **Customers**: `/api/customers`
- **Products**: `/api/products`
- **Orders**: `/api/orders`

Use o Swagger para explorar e testar todos os endpoints disponíveis.
