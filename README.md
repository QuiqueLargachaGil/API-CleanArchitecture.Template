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
docs<br>
├── Template Bootstrap Script<br>
src<br>
├── Core<br>
│   ├── Application<br>
│&emsp;&emsp;└── DependencyInjections<br>
│   └── Domain<br>
│&emsp;&emsp;└── Common<br>
│&emsp;&emsp;&emsp;&emsp;└── BaseModels<br>
│&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;├── AggregateRoot<br>
│&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;├── Entity<br>
│&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;└── ValueObject<br>
├── Infrastructure<br>
│   └── Infrastructure<br>
│&emsp;&emsp;└── DependencyInjections<br>
├── Presentation<br>
│   └── API<br>
│&emsp;&emsp;└── Program<br>
│   └── Contracts<br>
tests<br>
├── ArchitectureTests<br>
├── IntegrationTests<br>
├── SubcutaneousTests<br>
│── UnitTests<br>
│&emsp;&emsp;├── Application.UnitTests<br>
│&emsp;&emsp;└── Domain.UnitTests<br>
utils<br>
├── bootstrap.py<br>
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

> This template includes a bootstrap script that automates the initialization process and ensures consistent naming across the solution.

### Option 1 (Recommended): GitHub Template
1. Mark this repository as a **Template repository**
2. Create a new repository using this template
3. Clone the new repository locally
4. Run the bootstrap script:
```bash
python utils/bootstrap.py
```
5. Provide the required inputs:
   - Project name
   - Author name
   - Company name
   - The script will automatically:
      - Rename solution and project files
      - Update namespaces and internal references
      - Configure StyleCop (author, company, license)
      - Synchronize file headers
      - Regenerate the README
6. Start implementing your business logic

> Documentation:<br>
> Detailed documentation about the script is available at: `docs/bootstrap.md`

### Option 2: Manual Clone
1. Clone the repository
2. Run the bootstrap script following the related previous steps.
3. Push to a new repository

### Alternative Approach: Manual Setup
If you prefer not to use the script:
1. Clone the repository
2. Manually rename:
   - Solution file (.slnx)
   - Projects
   - Namespaces
3. Update StyleCop configuration and file headers
4. Adjust README and project metadata

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