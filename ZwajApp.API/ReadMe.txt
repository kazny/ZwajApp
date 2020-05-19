to create new project 
    1- create folder and cd to it 
    2- run the following (dotnet new webapi)
To run the project type in terminal
    >dotnet watch run

arrang code ctrl+A => ctrl+K+F

Ctrl+Shift+P type nuget

>dotnet add package Microsoft.EntityFrameworkCore

https://sqlitebrowser.org

>dotnet ef migrations add CreateUserMigration
tnet ef database update

add 
>dotnet add package System.IdentityModel.Tokens.Jwt --version 6.5.1

commet
ctrl+/

SPA:
to add form in http name as #FornName ="ngForm" add the FormModule in appModule #ngModules

npm install alertifyjs --save
add these files in style.css
    @import "~alertifyjs/build/css/themes/bootstrap.min.css";
    @import "~alertifyjs/build/css/alertify.rtl.min.css";

add angular.json
 "scripts": [
              "node_modules/alertifyjs/build/alertify.min.js"
            ]
to use this libarary we should create service (alertify.service.ts)
add to app.module.ts in provider section

add Jwt
npm install @auth0/angular-jwt

add ngx bootstrap

npm install ngx-bootstrap --save