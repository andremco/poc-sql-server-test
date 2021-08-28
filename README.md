# poc-sql-server-test
Project that connects with sql server and execute scripts for test purpose

# Requirements

## Chocolatey (optional)
Link - https://chocolatey.org/install

## sqllocaldb
If you install choco, run this code below with privileges admin:

```bash
 choco install sqllocaldb
```

or follow steps in link below:

https://docs.microsoft.com/pt-br/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15

## run sqllocaldb

```bash
  sqllocaldb i
```

the result for the previous code should return -> MSSQLLocalDB, and next, type in:

```bash
  sqllocaldb start MSSQLLocalDB
```