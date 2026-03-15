// --------------------------------------------------------------------------------------------------
// <copyright file="BaseTests.cs" company="YourCompany">
// Copyright (c) Enrique Largacha Gil.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Reflection;
using API.CleanArchitectureTemplate.Application;
using API.CleanArchitectureTemplate.Contracts;
using API.CleanArchitectureTemplate.Domain.Common.BaseModels;
using InfrastructureDependencyInjection = API.CleanArchitectureTemplate.Infrastructure.DependencyInjection;

namespace ArchitectureTests.Base;

/// <summary>
/// Base class for architecture tests.
/// Provides access to the assemblies that represent each layer of the Clean Architecture solution.
/// </summary>
public abstract class BaseTests
{
	/// <summary>
	/// Gets the Application layer assembly.
	/// </summary>
	protected static readonly Assembly ApplicationAssembly = typeof(DependencyInjection).Assembly;

	/// <summary>
	/// Gets the Domain layer assembly.
	/// </summary>
	protected static readonly Assembly DomainAssembly = typeof(AggregateRoot).Assembly;

	/// <summary>
	/// Gets the Infrastructure layer assembly.
	/// </summary>
	protected static readonly Assembly InfrastructureAssembly = typeof(InfrastructureDependencyInjection).Assembly;

	/// <summary>
	/// Gets the API (Presentation) layer assembly.
	/// </summary>
	protected static readonly Assembly ApiAssembly = typeof(Program).Assembly;

	/// <summary>
	/// Gets the Contracts assembly.
	/// </summary>
	/// <remarks>
	/// This assembly is intentionally isolated and must not have dependencies.
	/// </remarks>
	protected static readonly Assembly ContractsAssembly = typeof(ContractsAssemblyMarker).Assembly;
}
