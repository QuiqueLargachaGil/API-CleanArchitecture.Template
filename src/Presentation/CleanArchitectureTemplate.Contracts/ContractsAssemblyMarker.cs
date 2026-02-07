namespace CleanArchitectureTemplate.Contracts;

/***************************************************************************************
 * NOTE: Assembly marker class
 *
 * This class exists only to provide a stable reference to the Contracts assembly
 * for architecture tests.
 *
 * At template stage, the Presentation.Contracts project does not yet contain
 * any real domain-specific or API-related types that can be used to retrieve
 * its Assembly instance (via typeof(...).Assembly).
 *
 * This marker class allows:
 *   - Architecture tests to reference the Contracts assembly explicitly
 *   - Layer dependency rules to be validated from day one
 *
 * IMPORTANT:
 * Once the API development starts and real Contracts types are introduced
 * (e.g. request/response models, DTOs, versioned contracts, etc.),
 * this class can be safely removed.
 *
 * In that case:
 *   - Replace its usage in the ArchitectureTests BaseTest class
 *   - Use any real type from the Contracts project instead
 *
 * This class contains no logic by design.
 ***************************************************************************************/

public sealed class ContractsAssemblyMarker
{

}
