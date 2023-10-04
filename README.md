# Fullstack Project

![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![SASS](https://img.shields.io/badge/SASS-v.4-hotpink)
![React](https://img.shields.io/badge/React-v.18-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-v.1.9-brown)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-drakblue)
![Vite](https://img.shields.io/badge/Vite-v.4-purple)
![ASP.NET](https://img.shields.io/badge/ASP.Net-v.4-purple)
![Microsoft azure](https://img.shields.io/badge/MicrosoftAzure--blue)

This project involves creating a Fullstack project with React and Redux on the frontend and ASP.NET Core 7 on the backend. The goal is to provide a seamless experience for users, along with robust management system for administrators.

- Frontend: SASS, TypeScript, React, Redux Toolkit
- Backend: ASP .NET Core, Entity Framework Core, PostgreSQL

## Table of Contents

1. [Features](###features)
   - Admin Functionalities
   - User Functionalities
2. [Getting Started](###Introduction)
   - Brief Project Description
   - How to clone, start the project locally
   - Link to live pages
3. [ERD Diagram](###ERD Diagram)
4. [Features Diagram](#Feature Diagram)
5. [Project Tree](###Project Tree)

#### Admin Functionalities

1. User Management: Admins should be able to view and delete users.
2. Product Management: Admins should be able to view, edit, delete and add new products.
3. Order Management: Admins should be able to view all orders

#### User Functionalities

1. User Management: Users should be able to view and edit only certain properties in their accounts. They also can unregister their own accounts.
2. Authentication and account registration with Google Oauth.
3. Order Management: Users should be able to view their order history, track the status of their orders, and potentially cancel orders within a certain timeframe.

### Introduction

This repository provides the codebase for a Library Management system - Designed and implemented by Ebizimoh Abodei

- To clone this project, run the commen $ `git clone https://github.com/EbizimohAbodei/LibraryManagement` in your termail.

#### Backend

- To run the backend server and visualize implementation, simple navigate into the backend folder then run `dotnet restore` to restore packages. Also, create an "appsettings.Development.json" file inside the Infrastructure layer - use the example file provided in the same folder as a template. Finally, run navigate to the webapi.infrastructure folder then run the command $ `dotnet watch` to run the backend project.

#### Frontend

- First, navigate to the frontend folder then restore all packages using the command `npm i` or `npm install` assuming you're using the NPM package registry. from the parent folder, run $ `npm run dev` to start the project.

- Link to frontpage: [Frontend Project](https://fs15-fullstack.vercel.app/)
- Backend [Api Webpage](https://libmgtsyst.azurewebsites.net/swagger/index.html) Endpoints:

### Project Tree

```
📦
├─ .github
│  └─ workflows
│     └─ backend-deployment.yaml
├─ .gitignore
├─ .vscode
│  ├─ extensions.json
│  ├─ launch.json
│  └─ settings.json
├─ Backend ERD Diagram.png
├─ Backend Features.png
├─ README.md
├─ backend
│  ├─ .editorconfig
│  ├─ WebApi.Business
│  │  ├─ WebApi.Business.csproj
│  │  └─ src
│  │     ├─ Abstractions
│  │     │  ├─ IAuthService.cs
│  │     │  ├─ IBaseService.cs
│  │     │  ├─ IBookService.cs
│  │     │  ├─ ILoanService.cs
│  │     │  ├─ IReviewService.cs
│  │     │  └─ IUserService.cs
│  │     ├─ Configuration
│  │     │  └─ JwtConfig.cs
│  │     ├─ Dto
│  │     │  ├─ BookDto.cs
│  │     │  ├─ BookTitleDto.cs
│  │     │  ├─ LoanViewDto.cs
│  │     │  ├─ ReviewDto.cs
│  │     │  └─ UserDto.cs
│  │     ├─ Services
│  │     │  ├─ ServicesImplementations
│  │     │  │  ├─ BaseService.cs
│  │     │  │  ├─ BookService.cs
│  │     │  │  ├─ LoanService.cs
│  │     │  │  ├─ ReviewService.cs
│  │     │  │  └─ UserService.cs
│  │     │  └─ Shared
│  │     │     └─ ServiceExceptions.cs
│  │     └─ Shared
│  │        ├─ AuthService.cs
│  │        ├─ CustomException.cs
│  │        ├─ PasswordService.cs
│  │        └─ ServiceException.cs
│  ├─ WebApi.Controller
│  │  ├─ WebApi.Controller.csproj
│  │  └─ src
│  │     └─ Controllers
│  │        ├─ AuthController.cs
│  │        ├─ BooksController.cs
│  │        ├─ CrudController.cs
│  │        ├─ LoansController.cs
│  │        ├─ ReviewsController.cs
│  │        └─ UserController.cs
│  ├─ WebApi.Domain
│  │  ├─ WebApi.Domain.csproj
│  │  └─ src
│  │     ├─ Abstractions
│  │     │  ├─ IBaseRepository.cs
│  │     │  ├─ IBookRepository.cs
│  │     │  ├─ ILoanRepository.cs
│  │     │  ├─ IReviewRepository.cs
│  │     │  └─ IUserRepo.cs
│  │     ├─ Entities
│  │     │  ├─ BaseEntity.cs
│  │     │  ├─ Book.cs
│  │     │  ├─ Loan.cs
│  │     │  ├─ Review.cs
│  │     │  ├─ TimeStamp.cs
│  │     │  └─ User.cs
│  │     ├─ Enums
│  │     │  ├─ Gender.cs
│  │     │  ├─ Genre.cs
│  │     │  ├─ LoanStatus.cs
│  │     │  └─ Role.cs
│  │     └─ Shared
│  │        └─ QueryOptions.cs
│  ├─ WebApi.Infrastructure
│  │  ├─ Program.cs
│  │  ├─ Properties
│  │  │  └─ launchSettings.json
│  │  ├─ WebApi.Infrastructure.csproj
│  │  ├─ appsettings.example.json
│  │  └─ src
│  │     ├─ Configuration
│  │     │  └─ MapperProfile.cs
│  │     ├─ Database
│  │     │  └─ DatabaseContext.cs
│  │     ├─ Interceptor
│  │     │  └─ TimeStampInterceptor.cs
│  │     ├─ Middleware
│  │     │  └─ ErrorHandlerMiddleware.cs
│  │     └─ Repositories
│  │        └─ Implementation
│  │           ├─ BaseRepository.cs
│  │           ├─ BookRepository.cs
│  │           ├─ LoanRepository.cs
│  │           ├─ ReviewRepository.cs
│  │           └─ UserRepo.cs
│  ├─ WebApi.Tests
│  │  ├─ Usings.cs
│  │  ├─ WebApi.Tests.csproj
│  │  └─ src
│  │     └─ Service.Tests
│  │        ├─ BookServiceTests.cs
│  │        ├─ LoanServiceTests.cs
│  │        ├─ ReviewServiceTests.cs
│  │        └─ UserServiceTests.cs
│  └─ WebApi.sln
└─ frontend
   ├─ README.md
   ├─ index.html
   ├─ package.json
   ├─ postcss.config.js
   ├─ public
   │  └─ vite.svg
   ├─ src
   │  ├─ App.css
   │  ├─ App.tsx
   │  ├─ assets
   │  │  └─ react.svg
   │  ├─ components
   │  │  ├─ CustomAuthorInput.tsx
   │  │  ├─ CustomInput.tsx
   │  │  ├─ DashNav.tsx
   │  │  ├─ Loader.tsx
   │  │  ├─ Navbar.tsx
   │  │  ├─ ProductModal.tsx
   │  │  └─ SideNav.tsx
   │  ├─ hooks
   │  │  └─ reduxHooks.ts
   │  ├─ index.css
   │  ├─ layout
   │  │  ├─ Dashboard.tsx
   │  │  └─ Root.tsx
   │  ├─ main.tsx
   │  ├─ pages
   │  │  ├─ Book.tsx
   │  │  ├─ Books.tsx
   │  │  ├─ CreateAdmin.tsx
   │  │  ├─ CreateBooks.tsx
   │  │  ├─ Home.tsx
   │  │  ├─ Login.tsx
   │  │  ├─ Overview.tsx
   │  │  ├─ Profile.tsx
   │  │  ├─ SignUp.tsx
   │  │  ├─ UpdateUser.tsx
   │  │  └─ Users.tsx
   │  ├─ redux
   │  │  ├─ reducers
   │  │  │  ├─ bookReducers.ts
   │  │  │  └─ userReducers.ts
   │  │  └─ store.ts
   │  ├─ utils
   │  │  ├─ functions.ts
   │  │  └─ types.ts
   │  └─ vite-env.d.ts
   ├─ tailwind.config.js
   ├─ tsconfig.json
   ├─ tsconfig.node.json
   ├─ types
   │  └─ types.d.ts
   └─ vite.config.ts
```
