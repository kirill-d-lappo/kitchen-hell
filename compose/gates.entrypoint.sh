set -e

sleepTime=30s
echo "Waiting for $sleepTime to let the db warm up"
echo "Database starts to listen to port immediately; however, it may take time to init SQL Server"

portWait=30
echo "Waiting for MSSQL port is accessible for $portWait seconds"
/tools/wait-for-it.sh database:1433 -t $portWait

sleep $sleepTime

echo "Setting up initial user and empty kh database"
/opt/mssql-tools/bin/sqlcmd -S database -U sa -P $MSSQL_SA_PASSWORD -d master -i /tools/init.sql -e

echo "Database Gates are opened"
