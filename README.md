# backend-test run

- dotnet build

- dotnet run --project ./Web.Api/Web.Api.csproj

https://localhost:5001/swagger/index.html

- Fazer o input dos dados acima, usando um arquivo no formato de log;
- Chamar os recursos de API e combinar os dados para processamento.
POST: https://localhost:5001/api/GetAllMovements

- Exibir o log de movimentações de forma ordenada
GET: https://localhost:5001/api/Extract/GetAllMovements

- Informar o total de gastos por categoria;
GET: https://localhost:5001/api/Extract/TotalByCategory

- informar qual categoria cliente gastou mais;
GET: https://localhost:5001/api/Extract/CustomerCategorySpentMore

- informar qual foi o mês que cliente mais gastou;
GET: https://localhost:5001/api/Extract/MonthCustomerCategorySpentMore

# dotnet run test project
dotnet test