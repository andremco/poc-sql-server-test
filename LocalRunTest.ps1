# set variables
$connectionString = "Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Initial Catalog=tempdb;"

$sqlFile = "./database/tempdb.sql"

. .\RunTest.ps1

$agentIp = (New-Object net.webclient).downloadstring("http://checkip.dyndns.com") -replace "[^\d\.]"

write-host $agentip

