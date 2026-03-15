// --------------------------------------------------------------------------------------------------
// <copyright file="ArchitectureConstants.cs" company="YourCompany">
// Copyright (c) Enrique Largacha Gil.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectureTests.TestUtils;

/// <summary>
/// Provides shared constants used by architecture tests.
///
/// This class centralizes configuration values that define architectural
/// rules for the solution, such as forbidden dependencies between layers.
/// </summary>
public static class ArchitectureConstants
{
	/// <summary>
	/// List of assemblies that the Domain layer is not allowed to depend on.
	///
	/// The Domain layer should remain free of infrastructure,
	/// persistence, or external framework concerns.
	/// </summary>
	public static readonly string[] DomainForbiddenDependencies = new[] { "Microsoft.EntityFrameworkCore", "System.Data" };

	// Additional shared architecture configuration can be added here in the future
}
