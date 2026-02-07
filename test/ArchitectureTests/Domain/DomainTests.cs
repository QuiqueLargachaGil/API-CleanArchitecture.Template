using System.Linq;
using System.Reflection;
using ArchitectureTests.Base;
using ArchitectureTests.TestUtils;
using NetArchTest.Rules;

namespace ArchitectureTests.Domain;

public class DomainTests : BaseTests
{
	private const string AggregateRoot = "AggregateRoot";
	private const string Entity = "Entity";
	private const string ValueObject = "ValueObject";

	[Fact]
	public void DomainLayer_ShouldContainOnly_SealedConcreteClasses()
	{
		var result = Types
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

	[Fact]
	public void DomainBaseTypes_Should_BeAbstract()
	{
		var result = Types
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

	[Fact]
	public void Domain_ShouldNotDependOn_PersistenceLibraries()
	{
		var result = Types
			.InAssembly(DomainAssembly)
			.ShouldNot()
			.HaveDependencyOnAny(ArchitectureConstants.DomainForbiddenDependencies)
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

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
