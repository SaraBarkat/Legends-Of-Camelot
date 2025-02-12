using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.IO;
[Serializable]
public class ClasseTerrain 
{
    private string terrainName;
    private int defense;
    public Tilemap plainTilemap; 
    public Tilemap mountainTilemap; 
    public Tilemap forestTilemap; 
    public Tilemap routeTilemap;
    public Tilemap riviereTilemap;
    public ClasseTerrain(Tilemap plainTilemap, Tilemap mountainTilemap, Tilemap forestTilemap, Tilemap routeTilemap, Tilemap riviereTilemap, Vector3Int cellPosition) 
    {
        SetTerrainValues(plainTilemap, mountainTilemap, forestTilemap, routeTilemap, riviereTilemap, cellPosition);
    }

    private void SetTerrainValues(Tilemap plainTilemap, Tilemap mountainTilemap, Tilemap forestTilemap, Tilemap routeTilemap, Tilemap riviereTilemap, Vector3Int cellPosition) 
    {
        if (mountainTilemap != null && mountainTilemap.HasTile(cellPosition))
        {
            terrainName = "Montagne";
            defense = 4;
        }
        else if (forestTilemap != null && forestTilemap.HasTile(cellPosition))
        {
            terrainName = "Foret";
            defense = 3;
        }
        else if (routeTilemap != null && routeTilemap.HasTile(cellPosition))
        {
            terrainName = "Route";
            defense = 0;
        }
        else if (riviereTilemap != null && riviereTilemap.HasTile(cellPosition)) 
        {
            terrainName = "Riviere";
            defense = 0;
        }
        else if (plainTilemap != null && plainTilemap.HasTile(cellPosition))
        {
            terrainName = "Plaine";
            defense = 1;
        }
        else
        {
            terrainName = "Inconnu";
            defense = 0;
        }
    }
    public string TerrainName
{
    get { return terrainName; }
}

public int Defense
{
    get { return defense; }
}
}
