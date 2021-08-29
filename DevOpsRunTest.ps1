. .\EnvironmentVariable.ps1

write-host 'AZ Id: ' + $env:AZSUBSCRIPTION

write-host 'ProjectName: ' + $env:PROJECTNAME

$agentIp = (New-Object net.webclient).downloadstring("http://checkip.dyndns.com") -replace "[^\d\.]"

write-host $agentip

# . .\RunTest.ps1