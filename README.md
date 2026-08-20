# 🎮 GameStore - .NET 10 Clean Architecture

A professional **Game Store application** built with **ASP.NET Core .NET 10**, **Blazor**, **Entity Framework Core**, **SQL Server**, **ASP.NET Core Identity**, and **JWT Authentication**.

The project demonstrates how to build a modern, scalable, maintainable, and secure application using **Clean Architecture**, **Repository Pattern**, **Service Layer**, **DTOs**, **RESTful Web API**, and a **Blazor frontend**.

This project is also designed as a practical reference for developers migrating a legacy .NET application to **.NET 10 and Clean Architecture**.

---

## 🚀 Project Overview

GameStore is a full-stack .NET application that allows authenticated users to:

- Register a new account
- Login using username/email and password
- Authenticate using JWT tokens
- View available games
- Search games
- View game details
- Add new games
- Update existing games
- Delete games
- Work with paginated game listings
- Manage game information through a RESTful API

The application separates business logic, data access, authentication, domain entities, and presentation concerns into independent layers.

---

# 🏗️ Architecture

The application follows **Clean Architecture principles**.

```text
                        ┌───────────────────────┐
                        │    Blazor Client      │
                        │     Presentation      │
                        └───────────┬───────────┘
                                    │
                                    │ HTTP / JSON
                                    ▼
                        ┌───────────────────────┐
                        │     GameStore.Api     │
                        │    Web API Layer      │
                        │ Controllers / Auth    │
                        └───────────┬───────────┘
                                    │
                                    ▼
                  ┌─────────────────────────────────┐
                  │      GameStore.Application      │
                  │                                 │
                  │ DTOs                            │
                  │ Interfaces                      │
                  │ Services                        │
                  │ Business Logic                  │
                  └───────────────┬─────────────────┘
                                  │
                                  ▼
                  ┌─────────────────────────────────┐
                  │      GameStore.Infrastructure   │
                  │                                 │
                  │ EF Core                         │
                  │ DbContext                       │
                  │ Repositories                    │
                  │ Identity                        │
                  │ JWT Services                    │
                  └───────────────┬─────────────────┘
                                  │
                                  ▼
                        ┌───────────────────────┐
                        │      SQL Server      │
                        │       Database       │
                        └───────────────────────┘

                  ┌─────────────────────────────────┐
                  │        GameStore.Domain         │
                  │                                 │
                  │ Entities                        │
                  │ Domain Models                   │
                  │ Core Business Concepts          │
                  └─────────────────────────────────┘
