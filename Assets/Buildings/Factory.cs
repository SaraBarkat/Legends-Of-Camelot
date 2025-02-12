using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactoryBuilding : IAbsBuilding
{
    /// <summary>
    /// Gets the set of unit types that can be created by the building.
    /// </summary>
    /// <returns>The set of unit types that can be created by the building.</returns>

        HashSet<TypeUnite> GetUnitList();

    /// <summary>
    /// Creates a unit of the specified type.
    /// </summary>
    /// <param name="unitType">The type of unit to be created.</param>
    /// <returns>True if the unit was successfully created, otherwise false.</returns>
    bool Create(TypeUnite unitType);
}
