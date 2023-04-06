docker build -t skysample:latest  -f Web.API\Dockerfile .

docker run -d -t -i -p 32000:80 -e ASPNETCORE_ENVIRONMENT="Development" -e DB_SAMPLE_MSQL="server=sample-db-hml.cdz1u8qrhkma.us-east-1.rds.amazonaws.com;database=sample_db;user=admin;password=Vini#0102Mi"  public.ecr.aws/p3q4d0z4/sampleapi:50

pause