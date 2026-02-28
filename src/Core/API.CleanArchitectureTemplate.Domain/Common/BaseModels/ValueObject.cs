// --------------------------------------------------------------------------------------------------
// <copyright file="ValueObject.cs" company="YourCompany">
// Copyright (c) Enrique Largacha Gil.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace API.CleanArchitectureTemplate.Domain.Common.BaseModels;

/// <summary>
/// Represents the base class for value objects in the domain model.
/// A Value Object is defined entirely by its values and has no conceptual
/// identity. Two value objects with the same values are considered equal,
/// and they are typically immutable to preserve consistency and
/// predictability within the domain.
/// </summary>
/// <remarks>
/// This base type provides a common abstraction for modeling concepts
/// that describe characteristics, measurements, or descriptive aspects
/// of the domain without independent identity.
/// </remarks>
public abstract class ValueObject
{
}
