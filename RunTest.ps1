. .\Functions.ps1

$sqlConnection = New-Object System.Data.SqlClient.SqlConnection
$sqlConnection.ConnectionString = $connectionString
$sqlConnection.Open()

$queryCreateContextForTest = [IO.File]::ReadAllText($sqlFile)

# set time
$sw = [Diagnostics.Stopwatch]::StartNew()

# Execute creation context for test purpose!!
$sqlcmd = ExecuteTSQL -sqlConnection $sqlConnection -sql $queryCreateContextForTest
$adp = New-Object System.Data.SqlClient.SqlDataAdapter $sqlcmd
$data = New-Object System.Data.DataSet
$adp.Fill($data) | Out-Null

# Query in table customer  
$sqlcmd = ExecuteTSQL -sqlConnection $sqlConnection -sql "SELECT id, customer_name FROM [tempdb].[dbo].[customer]"
$adp = New-Object System.Data.SqlClient.SqlDataAdapter $sqlcmd
$data = New-Object System.Data.DataSet
$adp.Fill($data) | Out-Null

$emptyFailure = $false

foreach ($row in $data.Tables[0].Rows)
{ 
    # $row[0] -> id     $row[1] -> customer_name 
    # column id
    if($row[0] -eq 5){
        $emptyFailure = $true
        break
    }
}

if($emptyFailure -eq $true){
    throw ("Exec: something failed :( ")
}

$sqlConnection.Close()
$sqlConnection.Dispose()
$sw.Stop()

write-host "Build time: " $sw.Elapsed.ToString()
write-host
$data.Tables