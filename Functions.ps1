function ExecuteTSQL {
    param (
        [System.Data.SqlClient.SqlConnection] $sqlConnection,
        [string]$sql
    )

    $sqlcmd = $sqlConnection.CreateCommand()
    $sqlcmd.Connection = $sqlConnection

    LogQuery -sql $sql

    $sqlcmd.CommandText = $sql

    return $sqlcmd
}

function LogQuery {

    param(
        [string]$sql
    )

    write-host $sql
}
