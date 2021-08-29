# set variables
$connectionString = "Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Initial Catalog=tempdb;"

$sqlFile = "./database/tempdb.sql"

. .\RunTest.ps1

$agentIp = (New-Object net.webclient).downloadstring("http://checkip.dyndns.com") -replace "[^\d\.]"

write-host $agentip

# az sql server firewall-rule create -g mygroup -s myserver -n myrule --start-ip-address 1.2.3.4 --end-ip-address 5.6.7.8

