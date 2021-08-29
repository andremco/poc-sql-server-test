. .\EnvironmentVariable.ps1

write-host 'AZ Id: ' + $env:AZSUBSCRIPTION

write-host 'ProjectName: ' + $env:PROJECTNAME

# Get ip of agent azure devops for allow in firewall sql server
$agentIp = (New-Object net.webclient).downloadstring("http://checkip.dyndns.com") -replace "[^\d\.]"

az sql server firewall-rule create -g $resGroup -s $nameSqlServerAZ -n "myrule$(versionPipe)" --start-ip-address $agentIp --end-ip-address $agentIp

write-host $agentip

# . .\RunTest.ps1