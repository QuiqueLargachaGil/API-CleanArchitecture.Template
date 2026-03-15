// --------------------------------------------------------------------------------------------------
// <copyright file="AggregateRoot.cs" company="YourCompany">
// Copyright (c) Enrique Largacha Gil.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace API.CleanArchitectureTemplate.Domain.Common.BaseModels;

/// <summary>
/// Represents the base class for aggregate roots in the domain model.
/// An Aggregate Root is the entry point to an aggregate and is responsible
/// for enforcing consistency boundaries and invariants across the
/// associated entities and value objects.
/// </summary>
/// <remarks>
/// All access to an aggregate should occur through its root, ensuring
/// that the domain model remains consistent and aligned with business
/// rules.
/// </remarks>
public abstract class AggregateRoot
{
}
