# backend-test run

- Para rodar o projeto rode os seguintes comandos:

dotnet build

dotnet run --project ./Web.Api/Web.Api.csproj

# Swagger para documentar as rotas do projeto

  https://localhost:5001/swagger/index.html
  
# Descrição do que cada uma é responsável por retornar

- Fazer o input dos dados acima, usando um arquivo no formato de log;
- Chamar os recursos de API e combinar os dados para processamento.

POST: https://localhost:5001/api/Extract/UploadFileDbLog 

obs: aquivo de exemplo para upload na solution db.log
selecione no postman na aba body format-data e passando o campo file.

- Exibir o log de movimentações de forma ordenada

GET: https://localhost:5001/api/Extract/GetAllMovements

- Informar o total de gastos por categoria;

GET: https://localhost:5001/api/Extract/TotalByCategory

- Informar qual categoria cliente gastou mais;

GET: https://localhost:5001/api/Extract/CustomerCategorySpentMore

- Informar qual foi o mês que cliente mais gastou;

GET: https://localhost:5001/api/Extract/MonthCustomerCategorySpentMore

- Quanto de dinheiro o cliente gastou;

GET: https://localhost:5001/api/Extract/MoneyCustomerSpent

- Quanto de dinheiro o cliente recebeu;

GET: https://localhost:5001/api/Extract/MoneyCustomerReceived

- Saldo total de movimentações do cliente.

GET: https://localhost:5001/api/Extract/TotalMovementCustomer

# Como executar o projeto de teste

dotnet test

dotnet test Web.Api.UnitTest/Web.Api.UnitTest.csproj /p:CollectCoverage=true
