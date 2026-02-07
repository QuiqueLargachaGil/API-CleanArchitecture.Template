using ArchitectureTests.Base;
using NetArchTest.Rules;

namespace ArchitectureTests.Layers;

public sealed class LayersTests : BaseTests
{
	[Fact]
	public void DomainLayer_Should_NotHaveAnyDependencies()
	{
		var result = Types
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

	[Fact]
	public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayerOrPresentationLayer()
	{
		var result = Types
			.InAssembly(ApplicationAssembly)
			.ShouldNot()
			.HaveDependencyOnAny(
				InfrastructureAssembly.GetName().Name,
				ApiAssembly.GetName().Name,
				ContractsAssembly.GetName().Name)
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
	{
		var result = Types
			.InAssembly(InfrastructureAssembly)
			.ShouldNot()
			.HaveDependencyOnAny(
				ApiAssembly.GetName().Name,
				ContractsAssembly.GetName().Name)
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void Contracts_Should_NotHaveAnyDependencies()
	{
		var result = Types
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
