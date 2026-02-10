// --------------------------------------------------------------------------------------------------
// <copyright file="DomainTests.cs" company="YourCompany">
// Copyright (c) Enrique Largacha Gil.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using System.Reflection;
using ArchitectureTests.Base;
using ArchitectureTests.TestUtils;
using NetArchTest.Rules;
using TestResult = NetArchTest.Rules.TestResult;

namespace ArchitectureTests.Domain;

/// <summary>
/// Architecture tests for the Domain layer.
/// </summary>
public class DomainTests : BaseTests
{
	private const string AggregateRoot = "AggregateRoot";
	private const string Entity = "Entity";
	private const string ValueObject = "ValueObject";

	/// <summary>
	/// Ensures that all concrete classes in the Domain layer are sealed.
	/// Abstract base classes (e.g. AggregateRoot, Entity, ValueObject) are excluded
	/// as they are designed for inheritance.
	/// </summary>
	/// <remarks>
	/// Sealing domain classes prevents unintended inheritance, helps preserve
	/// domain invariants, and enforces explicit modeling decisions.
	/// </remarks>
	[Fact]
	public void DomainLayer_ShouldContainOnly_SealedConcreteClasses()
	{
		TestResult result = Types
			.InAssembly(DomainAssembly)
			.That()
			.AreClasses()
			.And()
			.AreNotAbstract()
			.Should()
			.BeSealed()
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	/// <summary>
	/// Ensures that all base types in the Domain layer are declared as abstract.
	/// </summary>
	[Fact]
	public void DomainBaseTypes_Should_BeAbstract()
	{
		TestResult result = Types
			.InAssembly(DomainAssembly)
			.That()
			.HaveNameEndingWith(AggregateRoot)
			.Or()
			.HaveNameEndingWith(Entity)
			.Or()
			.HaveNameEndingWith(ValueObject)
			.Should()
			.BeAbstract()
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	/// <summary>
	/// Verifies that the Domain assembly does not depend on typical
	/// persistence-related libraries such as Entity Framework Core or
	/// System.Data.
	/// </summary>
	/// <remarks>
	/// The domain layer should remain technology-agnostic and free of
	/// infrastructure concerns. This test prevents accidental references
	/// to common persistence assemblies. Adjust <c>ArchitectureConstants</c>
	/// if additional assemblies should be included or excluded.
	/// </remarks>
	[Fact]
	public void Domain_ShouldNotDependOn_PersistenceLibraries()
	{
		TestResult result = Types
			.InAssembly(DomainAssembly)
			.ShouldNot()
			.HaveDependencyOnAny(ArchitectureConstants.DomainForbiddenDependencies)
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	/// <summary>
	/// Verifies that public class types in the Domain assembly do not expose
	/// public fields.
	/// </summary>
	/// <remarks>
	/// Public mutable fields break encapsulation and domain invariants. Prefer
	/// properties with controlled accessors (get-only, private setter, or
	/// explicit methods) and use readonly/const for publicly visible constants.
	/// This test inspects public and static fields and will fail if any are
	/// detected.
	/// </remarks>
	[Fact]
	public void Domain_ShouldNotExposePublicFields()
	{
		var publicTypes = DomainAssembly.GetTypes().Where(t => t.IsClass && t.IsPublic);

		foreach (var type in publicTypes)
		{
			var publicFields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			Assert.Empty(publicFields);
		}
	}
}
