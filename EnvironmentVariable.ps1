$connectionString = $env:CONNECTIONSTRING
if ([string]::IsNullOrEmpty($connectionString)) { 
    $connectionString = "Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Initial Catalog=tempdb;"
}

$nameServer = $env:NAMESERVER
if ([string]::IsNullOrEmpty($nameServer)) { 
    $nameServer = "(localdb)\MSSQLLocalDB"
}

$sqlPrepareServer = "./tsqlt/PrepareServer.sql"

$sqlExample = "./tsqlt/Example.sql"

$sqlRunAll = "./tsqlt/RunAll.sql"

