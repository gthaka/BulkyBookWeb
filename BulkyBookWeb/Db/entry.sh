#!/bin/bash

set -e

# Run SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
echo "Waiting for SQL Server to start..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -Q "SELECT 1;" &> /dev/null
echo "SQL Server started successfully!"

# Run the setup script to initialize the database
echo "Initializing database..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i /usr/src/app/setup.sql
echo "Database initialized successfully!"

# Stop SQL Server
echo "Stopping SQL Server..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -Q "SHUTDOWN WITH NOWAIT;"
echo "SQL Server stopped successfully."

# Start SQL Server in the foreground
echo "Starting SQL Server..."
exec /opt/mssql/bin/sqlservr
