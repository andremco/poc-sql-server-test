$connectionString = $env:CONNECTIONSTRING
if ([string]::IsNullOrEmpty($connectionString)) { throw("Empty var connectionString!") }