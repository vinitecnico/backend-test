# backend-test run

- dotnet build

- dotnet run --project ./Web.Api/Web.Api.csproj

https://localhost:5001/swagger/index.html

- fazer o input dos dados acima, usando um arquivo no formato de log;
- chamar os recursos de API e combinar os dados para processamento.
POST: https://localhost:5001/api/GetAllMovements

- exibir o log de movimentações de forma ordenada
GET: https://localhost:5001/api/Extract/GetAllMovements

# dotnet run test project
dotnet test