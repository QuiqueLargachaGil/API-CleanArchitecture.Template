using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectureTests.TestUtils;

public static class ArchitectureConstants
{
	public static readonly string[] DomainForbiddenDependencies = new[] { "Microsoft.EntityFrameworkCore", "System.Data" };

	// Additional shared architecture configuration can be added here in the future
}
