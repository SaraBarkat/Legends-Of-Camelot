using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class UnitTerrainBonus : ScriptableObject
{
    public Dictionary<string, int[]> unitTerrainBonuses = new Dictionary<string, int[]>();

    // Méthode pour initialiser les valeurs des bonus de terrain
    public void Initialize()
    {
        // Ajouter les unités avec leurs bonus de terrain associés
        unitTerrainBonuses.Add("CavalierB", new int[] { 10, 10, -10, 15, -15 });
        unitTerrainBonuses.Add("CavalierR", new int[] { 10, 10, -10, 15, -15 });
        unitTerrainBonuses.Add("InfanterieLourdeB", new int[] { 10, -15, -15, 10, -10 });
        unitTerrainBonuses.Add("InfanterieLourdeR", new int[] { 10, -15, -15, 10, -10 });
        unitTerrainBonuses.Add("InfanterieEpéisteB", new int[] { 5, 5, -10, 10, -10 });
        unitTerrainBonuses.Add("InfanterieEpéisteR", new int[] { 5, 5, -10, 10, -10 });
        unitTerrainBonuses.Add("ChevalierB", new int[] { 5, 10, -10, 15, -15 });
        unitTerrainBonuses.Add("ChevalierR", new int[] { 5, 10, -10, 15, -15 });
        unitTerrainBonuses.Add("ArbalétrierB", new int[] { 5, 5, -10, 20, -20 });
        unitTerrainBonuses.Add("ArbalétrierR", new int[] { 5, 5, -10, 20, -20 });
        unitTerrainBonuses.Add("ArcherLourdeB", new int[] { 5, 5, -10, 20, -20 });
        unitTerrainBonuses.Add("ArcherLourdeR", new int[] { 5, 5, -10, 20, -20 });
        unitTerrainBonuses.Add("InfanterieSimpleB", new int[] { 15, -10, -15, 10, -10 });
        unitTerrainBonuses.Add("InfanterieSimpleR", new int[] { 15, -10, -15, 10, -10 });
    }

    // Méthode pour obtenir le bonus de terrain pour une unité et une position de cellule donnée
    public int GetTerrainBonus(string unitName, Vector3Int cellPosition, Tilemap plainTilemap, Tilemap mountainTilemap, Tilemap forestTilemap, Tilemap routeTilemap, Tilemap riviereTilemap)
    {
        // Créez une instance de ClasseTerrain pour obtenir le type de terrain
        ClasseTerrain terrain = new ClasseTerrain(plainTilemap, mountainTilemap, forestTilemap, routeTilemap, riviereTilemap, cellPosition);
        
        // Obtenez le type de terrain
        string terrainType = terrain.TerrainName;

        // Obtenez le bonus correspondant
        return GetBonusForTerrain(unitName, terrainType);
    }

    // Méthode pour obtenir le bonus de terrain pour une unité et un type de terrain donnés
    private int GetBonusForTerrain(string unitName, string terrainType)
    {
        int bonus = 0;

        // Vérifiez d'abord si le dictionnaire contient l'unité
        if (unitTerrainBonuses.ContainsKey(unitName))
        {
            // Récupérez les bonus associés à cette unité
            int[] terrainBonuses = unitTerrainBonuses[unitName];

            // Obtenez l'index correspondant au type de terrain dans l'ordre : plaine, montagne, forêt, route, rivière
            int terrainIndex = GetTerrainIndex(terrainType);

            // Assurez-vous que l'index est valide
            if (terrainIndex >= 0 && terrainIndex < terrainBonuses.Length)
            {
                // Récupérez le bonus pour ce type de terrain
                bonus = terrainBonuses[terrainIndex];
            }
            else
            {
                Debug.LogError("Index de terrain invalide : " + terrainIndex);
            }
        }
        else
        {
            Debug.LogError("Unité inconnue : " + unitName);
        }

        return bonus;
    }

    // Méthode pour obtenir l'index correspondant au type de terrain
    private int GetTerrainIndex(string terrainType)
    {
        int index = -1;

        switch (terrainType)
        {
            case "Plaine":
                index = 0;
                break;
            case "Montagne":
                index = 1;
                break;
            case "Forêt":
                index = 2;
                break;
            case "Route":
                index = 3;
                break;
            case "Rivière":
                index = 4;
                break;
        }

        return index;
    }
}
