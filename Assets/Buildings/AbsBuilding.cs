using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 public interface IAbsBuilding
    {
        /// <summary>
        /// Gets the name of the building.
        /// </summary>
        /// <returns>The name of the building.</returns>
        string GetName();
        /// <summary>
        /// Gets the terrain on which the building is located.
        /// </summary>
        /// <returns>The terrain on which the building is located.</returns>
       // Terrain GetTerrain();
        int GetX();

        int GetY();

        /// <summary>
        /// Gets the location of the building.
        /// </summary>
        /// <returns>The location of the building as a vector .</returns>
        Vector2 GetLocation();

        /// <summary>
        /// Sets the location of the building.
        /// </summary>
        /// <param name="location">The new location of the building.</param>
        void SetLocation(Vector2 location);
        
    }