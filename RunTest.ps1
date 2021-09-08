# set time
$sw = [Diagnostics.Stopwatch]::StartNew()


try {
    # PrepareServer.sql automatically enables CLR and installs a server certificate that allows the installation of the tSQLt CLR.
    Invoke-Sqlcmd -ServerInstance $nameServer -InputFile $sqlPrepareServer -Verbose -ErrorAction Stop

    # Execute the Example.sql file from the zip file to create an example database (tSQLt_Example) with tSQLt and test cases.
    Invoke-Sqlcmd -ServerInstance $nameServer -InputFile $sqlExample -Verbose -ErrorAction Stop    
}
catch {
    Write-Error "Something wrong in script TSQLt :("
    Write-Error $_
}

try 
{
    #  Execute the following script to run all the example tests
    Invoke-Sqlcmd -ServerInstance $nameServer -InputFile $sqlRunAll -Verbose -ErrorAction Stop 
}
catch
{
    Write-Error "Failed tests tsqlt :("
    Write-Error $_
}

. .\PublishTestsTSQLt.ps1
$sw.Stop()
write-host "Build time: " $sw.Elapsed.ToString()