using ArchitectureTests.Base;
using NetArchTest.Rules;
using TestResult = NetArchTest.Rules.TestResult;

namespace ArchitectureTests.Presentation;

public sealed class PresentationTests : BaseTests
{
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
