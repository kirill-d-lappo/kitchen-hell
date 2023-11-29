set -e

sleepTime=10s
echo "Waiting for $sleepTime to let the db warm up"
sleep $sleepTime

echo "Waiting for MSSQL port is accessible"
/tools/wait-for-it.sh database:1433 -t 30

echo "Setting up initial user and empty Vopty database"
/opt/mssql-tools/bin/sqlcmd -S database -U sa -P "$MSSQL_SA_PASSWORD" -d master -i /tools/init.sql -e

echo "Database Gates are opened"
