# install
- install dotnet SDK 8.0, 
- dotnet tool install --global dotnet-ef
- postgres version 13 trở lên
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
- Serilog: //cấu hình file log
- swagger

# account login 
- username: admin@example.com
- password: CustomPassword123!

# steps to run(command line)
B0:  Tạo Database với thông số sau: (Database=Test, Username=postgres, Password=mysecretpassword)
B1:  Chạy lệnh khởi tạo database: dotnet ef database update 
B2:  Lệnh chạy ứng dụng: dotnet run

