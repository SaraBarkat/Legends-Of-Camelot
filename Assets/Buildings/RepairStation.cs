using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

   public interface IRepairStation : IAbsBuilding
    {
    
        /// Checks if the building can repair the given unit.
        /// <param name="u">The unit to check for repair.</param>
        /// <returns>True if the building can repair the unit, otherwise false.</returns>

        bool CanRepair(Unite u);


        /// Repairs the given unit.
        /// <param name="u">The unit to repair.</param>
        
        void Repair(Unite u);
    }
   /*   public static class RepairBuildingExtensions
    {
        public static void RepairDefault(this IRepairStation building, Unite unit)
        {
            if (building.CanRepair(unit))
            {
                if (building is PropertyManager ownableBuilding)
                {
                    Joueur p = ownableBuilding.Getproprietaire();

                    // the building repairs between 0 and 4 life points
                    // ce la signifie que lorsqu'une unité est réparée par le bâtiment, elle peut récupérer entre 0 et 20 points de vie à chaque réparation
                    // the building cannot add life such as the unit has more than 10 life points
                    // one life point costs 1/80 of the unit's cost
                    // so the final formula is the one below :
                    int pv = Math.Max(0, Math.Min(Math.Min(10 - unit.GetLife(), 4), 80 * p.GetFunds() / unit.GetCost()));
                    unit.AddLife(pv);
                    p.Spent(pv * unit.GetCost() / 80);
                }
                else
                {
                    unit.AddLife(4);
                }
               
            }
        }
    }*/
