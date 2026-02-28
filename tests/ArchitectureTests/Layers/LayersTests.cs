// --------------------------------------------------------------------------------------------------
// <copyright file="LayersTests.cs" company="YourCompany">
// Copyright (c) Enrique Largacha Gil.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using ArchitectureTests.Base;
using NetArchTest.Rules;
using TestResult = NetArchTest.Rules.TestResult;

namespace ArchitectureTests.Layers;

/// <summary>
/// Contains architecture tests that validate dependency rules
/// between the different layers of the system.
/// </summary>
public sealed class LayersTests : BaseTests
{
	/// <summary>
	/// Verifies that the Domain layer does not depend on any other layer.
	/// </summary>
	[Fact]
	public void DomainLayer_Should_NotHaveAnyDependencies()
	{
		TestResult result = Types
			.InAssembly(DomainAssembly)
			.ShouldNot()
			.HaveDependencyOnAny(
				ApplicationAssembly.GetName().Name,
				InfrastructureAssembly.GetName().Name,
				ApiAssembly.GetName().Name,
				ContractsAssembly.GetName().Name)
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	/// <summary>
	/// Verifies that the Application layer does not depend on
	/// the Infrastructure or Presentation layers.
	/// </summary>
	[Fact]
	public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayerOrPresentationLayer()
	{
		TestResult result = Types
			.InAssembly(ApplicationAssembly)
			.ShouldNot()
			.HaveDependencyOnAny(
				InfrastructureAssembly.GetName().Name,
				ApiAssembly.GetName().Name,
				ContractsAssembly.GetName().Name)
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	/// <summary>
	/// Verifies that the Infrastructure layer does not depend on
	/// the Presentation layer.
	/// </summary>
	[Fact]
	public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
	{
		TestResult result = Types
			.InAssembly(InfrastructureAssembly)
			.ShouldNot()
			.HaveDependencyOnAny(
				ApiAssembly.GetName().Name,
				ContractsAssembly.GetName().Name)
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	/// <summary>
	/// Verifies that the Contracts assembly is fully isolated
	/// and does not depend on any other layer.
	/// </summary>
	[Fact]
	public void Contracts_Should_NotHaveAnyDependencies()
	{
		TestResult result = Types
			.InAssembly(DomainAssembly)
			.ShouldNot()
			.HaveDependencyOnAny(
				DomainAssembly.GetName().Name,
				ApplicationAssembly.GetName().Name,
				InfrastructureAssembly.GetName().Name,
				ApiAssembly.GetName().Name)
			.GetResult();

		Assert.True(result.IsSuccessful);
	}
}
