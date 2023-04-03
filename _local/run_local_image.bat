docker build -t skysample:latest  -f Web.API\Dockerfile .

docker run -d -t -i -p 32000:80 -e ASPNETCORE_ENVIRONMENT="Development" -e SAMPLEDB_DATABASE_USER="system" -e SAMPLEDB_DATABASE_PASSWORD="oracle" -e SAMPLEDB_DATABASE_CONNECTION="User Id={0};Password={1}; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.10.9)(PORT=49161))(CONNECT_DATA=(SERVICE_NAME=xe)(SERVER=dedicated)));"  skysample:latest

pause