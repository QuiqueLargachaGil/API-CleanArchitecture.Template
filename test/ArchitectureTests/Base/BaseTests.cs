using CleanArchitectureTemplate.Application;
using CleanArchitectureTemplate.Contracts;
using CleanArchitectureTemplate.Domain.Common.BaseModels;
using System.Reflection;
using InfrastructureDependencyInjection = CleanArchitectureTemplate.Infrastructure.DependencyInjection;

namespace ArchitectureTests.Base;

public abstract class BaseTests
{
	protected static readonly Assembly ApplicationAssembly = typeof(DependencyInjection).Assembly;
	protected static readonly Assembly DomainAssembly = typeof(AggregateRoot).Assembly;
	protected static readonly Assembly InfrastructureAssembly = typeof(InfrastructureDependencyInjection).Assembly;
	protected static readonly Assembly ApiAssembly = typeof(Program).Assembly;
	protected static readonly Assembly ContractsAssembly = typeof(ContractsAssemblyMarker).Assembly;
}
