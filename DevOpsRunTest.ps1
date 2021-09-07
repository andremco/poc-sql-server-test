. .\EnvironmentVariable.ps1

# install localdb express
choco install sqllocaldb

sqllocaldb i

sqllocaldb start MSSQLLocalDB

. .\RunTest.ps1

# Publishing the test results
$sqlCommand = 'EXEC tSQLt.XmlResultFormatter'

$connection = new-object system.data.SqlClient.SQLConnection($connectionString)
$command = new-object system.data.sqlclient.sqlcommand($sqlCommand,$connection)
$connection.Open()

$adapter = New-Object System.Data.sqlclient.sqlDataAdapter $command
$dataset = New-Object System.Data.DataSet
$adapter.Fill($dataSet) | Out-Null

$connection.Close()
$dataSet.Tables[0].Rows[0].ItemArray[0] | Out-File "$(System.DefaultWorkingDirectory)/_publish-CI/drop/TEST-tempdb.xml"
