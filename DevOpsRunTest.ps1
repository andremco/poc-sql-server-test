. .\EnvironmentVariable.ps1

# install localdb express
choco install sqllocaldb

sqllocaldb i

sqllocaldb start MSSQLLocalDB

. .\RunTest.ps1
