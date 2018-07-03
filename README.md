# ASP.NET-Education-System Project

- ASP.NET MVC CORE 2
- Microsoft SQL Server
- Entity Framework Core
- AutoMapper
- Razor Engine
-- Bootstrap
** Extensions for:
   -- Service layer
   -- Identities role
   -- DB Migration
   
The project is a Learning/Education system that has three main roles (administrator, trainer and author) 
and is divided into several areas.
Administrator's role uses a CRUD on the users, as well as creating different events.
The Trainer role enables course management, issuance of PDF certificates, 
assessment of students and info for enrolled students in courses. .
Author role allows publish of blog articles that can be commented by users logged in.
Each logged-in student selects a role in his profile, without the administrator 
and can uploading a .zip file with max size 2MB.
The main page shows the active courses, and the users can also review the archives.
The Project has a search engine for blog articles, courses and students.
The project is based on the OOP principles and uses Dependancy Injection.
The Project is divided into several layers that are Web, Service, Data/DB, Constants, Infrastructure/Extensions
