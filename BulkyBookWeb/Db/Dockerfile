FROM mcr.microsoft.com/mssql/server:2022-latest

WORKDIR /usr/src/app
COPY setup.sql setup.sql
COPY entry.sh entry.sh

ENTRYPOINT ["/usr/src/app/entry.sh"]

# Set the command to start SQL Server
CMD ["/opt/mssql/bin/sqlservr"]

