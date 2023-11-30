set -e

sleepTime=20s
echo "Waiting for $sleepTime to let the db warm up"
sleep $sleepTime

echo "Inserting base data"
/opt/mssql-tools/bin/sqlcmd -S database -U sa -P $MSSQL_SA_PASSWORD -d master -i /tools/database-dummy-data.entrypoint.sql -e

