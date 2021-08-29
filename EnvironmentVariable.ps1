$connectionString = $env:CONNECTIONSTRING
if ([string]::IsNullOrEmpty($connectionString)) { throw("Empty var connectionString!") }

$resGroup = $env:RESOURCEGROUP
if ([string]::IsNullOrEmpty($resGroup)) { throw("Empty var resGroup!") }

$nameSqlServerAZ = $env:NAMESQLSERVERAZ
if ([string]::IsNullOrEmpty($nameSqlServerAZ)) { throw("Empty var nameSqlServerAZ!") }

# ${env:VERSION}

$versionPipe = $env:VERSION
if ([string]::IsNullOrEmpty($versionPipe)) { throw("Empty var versionPipe!") }