# set time
$sw = [Diagnostics.Stopwatch]::StartNew()

# PrepareServer.sql automatically enables CLR and installs a server certificate that allows the installation of the tSQLt CLR.
Invoke-Sqlcmd -ServerInstance $nameServer -InputFile $sqlPrepareServer -Verbose

# Execute the Example.sql file from the zip file to create an example database (tSQLt_Example) with tSQLt and test cases.
Invoke-Sqlcmd -ServerInstance $nameServer -InputFile $sqlExample -Verbose

#  Execute the following script to run all the example tests
Invoke-Sqlcmd -ServerInstance $nameServer -InputFile $sqlRunAll -Verbose

$sw.Stop()
write-host "Build time: " $sw.Elapsed.ToString()