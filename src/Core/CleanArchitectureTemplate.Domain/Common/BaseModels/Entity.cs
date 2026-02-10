// --------------------------------------------------------------------------------------------------
// <copyright file="Entity.cs" company="YourCompany">
// Copyright (c) Enrique Largacha Gil.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace CleanArchitectureTemplate.Domain.Common.BaseModels;

/// <summary>
/// Represents the base class for entities in the domain model.
/// An Entity is defined by its identity rather than its attributes.
/// Even if its properties change over time, its identity remains stable,
/// allowing it to be consistently tracked throughout the lifecycle of
/// the domain model.
/// </summary>
/// <remarks>
/// This base type provides a common abstraction for all domain entities
/// and serves as a foundation for enforcing identity-based equality
/// and domain consistency rules.
/// </remarks>
public abstract class Entity
{
}
