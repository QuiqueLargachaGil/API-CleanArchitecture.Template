# .NET API Clean Architecture Template

[![PR Validation](https://github.com/QuiqueLargachaGil/API-CleanArchitecture.Template/actions/workflows/pr-validation.yml/badge.svg?branch=master)](https://github.com/QuiqueLargachaGil/API-CleanArchitecture.Template/actions/workflows/pr-validation.yml)
![License](https://img.shields.io/github/license/QuiqueLargachaGil/API-CleanArchitecture.Template)
![.NET](https://img.shields.io/badge/.NET-10-blue)
![Template](https://img.shields.io/badge/template-clean--architecture-blue)
![Last Commit](https://img.shields.io/github/last-commit/QuiqueLargachaGil/API-CleanArchitecture.Template)

This repository provides a **starter solution template** for building REST APIs in **.NET**, following **Clean Architecture** and **Domain-Driven Design (DDD)** principles.

The goal of this template is to offer a **clean, consistent and scalable project structure** that can be reused as the foundation for future API projects.

No business logic is included. This repository only contains:
- Project structure
- References between layers
- Basic configuration
- Test project scaffolding

>### ⚠️ Disclaimer<br>
>This repository is primarily intended as a learning project.<br>
>The template has been mainly created  to deepen practical understanding of:
>- Clean Architecture
>- Domain-Driven Design (DDD)
>- Testing strategies in .NET APIs
>
>While the structure and decisions are based on well-known principles and best practices,
>this template should not be considered a definitive or enterprise-approved standard.<br>
>Any feedback, comments, or suggestions for improvement are very welcome and highly appreciated.

## 📁 Solution Structure
src<br>
├── Core<br>
│   ├── Application<br>
│   └── Domain<br>
│&emsp;&emsp;└── Common<br>
│&emsp;&emsp;&emsp;&emsp;└── BaseModels<br>
│&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;├── AggregateRoot<br>
│&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;├── Entity<br>
│&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;└── ValueObject<br>
├── Infrastructure<br>
│   └── Infrastructure<br>
├── Presentation<br>
│   ├── API<br>
│   └── Contracts<br>
tests<br>
├── AcceptanceTests<br>
├── ArchitectureTests<br>
├── EndToEndTests<br>
├── FunctionalTests<br>
├── IntegrationTests<br>
└── UnitTests<br>
.editorconfig<br>
.gitignore<br>
Solution.sln<br>
Directory.Build.props<br>
LICENSE<br>
README.md<br>
stylecop.json<br>

## 🧱 Layer Responsibilities

### Core / Domain
- Domain entities
- Value Objects
- Aggregates
- Domain events
- Domain interfaces
- No dependencies on other layers

### Core / Application
- Use cases / application services
- DTOs (internal)
- Interfaces (repositories, services)
- Business rules orchestration
- Depends only on `Domain`

### Infrastructure
- Persistence implementations
- External services
- Identity, messaging, file system, etc.
- Implements interfaces defined in `Application`
- Depends on `Application` and `Domain`

### Presentation / API
- ASP.NET Core Web API
- Controllers / Minimal APIs
- Dependency Injection configuration
- Depends on `Application` and `Contracts`

### Presentation / Contracts
- Public request/response contracts
- API models shared with clients
- Versionable and decoupled from domain

## 🧪 Testing Strategy

### Architecture Tests
Ensures architectural rules are respected:
- Domain has no external dependencies
- Application does not depend on Infrastructure
- Presentation does not violate layer boundaries

### Unit Tests
- Domain: pure business rules
- Application: use cases and orchestration
- Infrastructure: isolated logic when applicable

### Integration Tests
- API end-to-end tests
- Real DI container
- HTTP pipeline validation

## 🚀 How to Use This Template

### Option 1 (Recommended): GitHub Template
1. Mark this repository as a **Template repository**
2. Create a new repository using this template
3. Clone the new repository locally
4. Rename:
   - Solution file (`.sln`)
   - API project
   - Root namespaces
5. Start implementing your business logic

### Option 2: Manual Clone
1. Clone the repository
2. Rename solution, projects and namespaces
3. Push to a new repository

## 🎯 Purpose

This template is intended to:
- Reduce setup time for new API projects
- Enforce architectural consistency
- Serve as a learning and reference project
- Scale from small to large applications

## 📌 Notes

- No business logic is included by design
- No concrete implementations are provided
- This is an evolving template and can be extended as needed

## 📝 License

This project is licensed under the MIT License.
If you find it useful, a mention or star is always appreciated 🙂

Happy coding! 🚀