// --------------------------------------------------------------------------------------------------
// <copyright file="PresentationTests.cs" company="YourCompany">
// Copyright (c) Enrique Largacha Gil.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using ArchitectureTests.Base;
using NetArchTest.Rules;
using TestResult = NetArchTest.Rules.TestResult;

namespace ArchitectureTests.Presentation;

/// <summary>
/// Architecture tests for the Presentation (API) layer.
/// </summary>
public sealed class PresentationTests : BaseTests
{
	/// <summary>
	/// Ensures that controllers (types with names ending in 'Controller')
	/// do not depend on Domain or Infrastructure assemblies.
	/// </summary>
	[Fact]
	public void Controllers_ShouldNotDependOn_InfrastructureOrDomain()
	{
		TestResult result = Types
			.InAssembly(ApiAssembly)
			.That()
			.HaveNameEndingWith("Controller")
			.ShouldNot()
			.HaveDependencyOnAny(
				InfrastructureAssembly.GetName().Name,
				DomainAssembly.GetName().Name)
			.GetResult();

		Assert.True(result.IsSuccessful);
	}
}
