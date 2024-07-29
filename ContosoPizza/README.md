# install
- install dotnet SDK 8.0, 
- dotnet tool install --global dotnet-ef
- postgres version 13
# database
- Database=Test
- Username=postgres
- Password=mysecretpassword
- run lệnh Migrations để khởi tạo database
   + dotnet ef database update

# run command line
dotnet run

# include
- WebAPI
- JWT(UseAuthentication,UseAuthorization,AuthorizationMiddleware)
- Identity
- Migrations:
   + dotnet ef migrations add MYEntityUpdateForAge
   + dotnet ef database update
- 

