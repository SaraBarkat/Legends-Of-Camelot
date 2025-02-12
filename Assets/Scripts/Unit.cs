using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Random = UnityEngine.Random;

public class Unit : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;
    public GameObject hpDizaine; // Reference to the tens place of HP
    public GameObject hpUnite; // Reference to the units place of HP

    public Sprite[] hpSprites; // Array of HP sprites (0-9)

    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private UnitTerrainBonus unitTerrainBonus;
    private int[,] baseAttackMatrix;

    public Tilemap plainTilemap; 
    public Tilemap mountainTilemap; 
    public Tilemap forestTilemap; 
    public Tilemap routeTilemap;
    public Tilemap riviereTilemap;
    private List<Vector3Int> attackableTiles = new List<Vector3Int>();
    public TileBase newTile;
    private Dictionary<Vector3Int, TileBase> originalTiles = new Dictionary<Vector3Int, TileBase>();
    public PlayerColor col;
    public bool cancapture;
    public CursorControl curseurRouge;
    public CursorControl curseurBleu;
    void Start()
    {
        currentHP = maxHP;
        unitTerrainBonus = ScriptableObject.CreateInstance<UnitTerrainBonus>();

        unitTerrainBonus.Initialize();
        InitializeBaseAttackMatrix(); 

        ChangeSprite(currentHP); // Call to update HP sprite at start
        AfficherHP(); // Call to display HP at start
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            // Check if the hit object has a Unit component
            Unit enemyUnit = enemyCollider.GetComponent<Unit>();
            if (enemyUnit != null && enemyUnit != this)
            {
                // Perform the attack
                string attackerType = gameObject.name;
                string defenderType = enemyUnit.gameObject.name;

                int attackerIndex = Array.IndexOf(unitNames, attackerType);
                int defenderIndex = Array.IndexOf(unitNames, defenderType);
                
                if (attackerIndex != -1 && defenderIndex != -1)
                {
                    int baseAttack = baseAttackMatrix[attackerIndex, defenderIndex];
                    enemyUnit.TakeDamage(baseAttack);
                    int selfDamage = TakeSelfDamage(baseAttack, currentHP, maxHP);
                    currentHP -= selfDamage;
                    if (currentHP <= 0)
                    {
                        PlayerColor gameObjectColor = enemyUnit.getPlayerColor();
                        if(gameObjectColor == PlayerColor.ROUGE) {
                            curseurRouge.deleteUnit(gameObject);
                        }
                        else if(gameObjectColor == PlayerColor.BLEU) {
                            curseurBleu.deleteUnit(gameObject);
                        }
                        else {
                            Debug.Log("Couleur indeterminee");
                        }
                        Destroy(gameObject);
                    }
                    ChangeSprite(currentHP); // Call to update HP sprite after attack
                }
                else
                {
                    Debug.Log("Invalid unit index.");
                }
            }
        }
    }

    public void ChangeSprite(int nombre)
    {
        if (hpDizaine != null && hpUnite != null && hpSprites.Length >= 10)
        {
            if (nombre < 10)
            {
                int index = Mathf.Clamp(nombre, 0, hpSprites.Length - 1);
                hpDizaine.GetComponent<SpriteRenderer>().sprite = hpSprites[0]; 
                hpUnite.GetComponent<SpriteRenderer>().sprite = hpSprites[index];   
            }
            else if (nombre >= 10 && nombre < 100)
            {
                int dizaine = Mathf.Clamp(nombre / 10, 0, hpSprites.Length - 1);
                int unite = Mathf.Clamp(nombre % 10, 0, hpSprites.Length - 1);
                hpDizaine.GetComponent<SpriteRenderer>().sprite = hpSprites[dizaine];
                hpUnite.GetComponent<SpriteRenderer>().sprite = hpSprites[unite];
            }
            else
            {
                Debug.LogError("Invalid number to display HP.");
            }
        }
        else
        {
            Debug.LogError("Missing HP game object or HP sprites in Unit script!");
        }
    }
    public PlayerColor getPlayerColor() {
        return col;
    }
    public void AfficherHP()
    {
        if (hpDizaine != null)
        {
            hpDizaine.SetActive(true);
        }
        else
        {
            Debug.LogError("Missing HP game object in Unit script!");
        }
    }

    public void TakeDamage(int baseAttack)
    {
        int luck = Random.Range(2, 4);
        float attackValue = baseAttack + luck;
        Vector3Int cellPosition = plainTilemap.WorldToCell(transform.position);
        float defenseValue = 100f - (unitTerrainBonus.GetTerrainBonus(gameObject.name, cellPosition, plainTilemap, mountainTilemap, forestTilemap, routeTilemap, riviereTilemap) * currentHP / 100f);
        
        float totalDamage = (currentHP / 10f) * attackValue * defenseValue / 100f;

        int damageTaken = Mathf.CeilToInt(totalDamage);
        currentHP -= damageTaken;

        if (currentHP <= 0)
        {
            if(col == PlayerColor.ROUGE) {
                curseurRouge.deleteUnit(gameObject);
            }
            else if(col == PlayerColor.BLEU) {
                curseurBleu.deleteUnit(gameObject);
            }
            else {
                Debug.Log("Couleur indeterminee");
            }
            Destroy(gameObject);
        }

        ChangeSprite(currentHP); // Call to update HP sprite at start
        AfficherHP(); // Call to display HP at start
    }
    
    private int TakeSelfDamage(int baseAttack, int currentHP, int maxHP)
    {
        if (maxHP == 0)
            return 0;
        
        float damagePercentage = (float)baseAttack / maxHP;
        int selfDamage = Mathf.CeilToInt(currentHP * damagePercentage);

        return selfDamage;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    string[] unitNames = { "CavalierR", "ChevalierR", "ArcherLourdeR", "InfanterieSimpleR", "InfanterieLourdeR", "InfanterieEpéisteR", "ArbaletrierR",
                           "CavalierB", "ChevalierB", "ArcherLourdeB", "InfanterieSimpleB", "InfanterieLourdeB", "InfanterieEpéisteB", "ArbaletrierB" };

    public void InitializeBaseAttackMatrix()
    {
        baseAttackMatrix = new int[,]
        {
            { 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }
        };
    }
    void CalculateAttackableTiles()
{
    ClasseTerrain terrain1, terrain2;
     // Effacer la liste des cases accessibles précédentes
    attackableTiles.Clear();
    // Obtenir la position actuelle de l'unité
    Vector3Int unitPosition = plainTilemap.WorldToCell(transform.position);

    // Initialiser une file pour le parcours en largeur
    Queue<Vector3Int> queue = new Queue<Vector3Int>();
    queue.Enqueue(unitPosition);

    // Tableau pour marquer les cases déjà visitées
    HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
    visited.Add(unitPosition);

    // Tableau pour stocker les directions possibles de déplacement
    Vector3Int[] directions = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };

    // Parcourir les cellules accessibles jusqu'à ce que la portée de mouvement soit atteinte
    int distance = 0;
    float attackRangeLocal;
    if(attackRange<1) {
        attackRangeLocal=1;
    }
    else{
        attackRangeLocal=attackRange;
    }
    while (queue.Count > 0 && distance <= attackRangeLocal)
    {
        // Nombre de cellules dans la file avant le prochain niveau
        int count = queue.Count;
        
        // Parcourir toutes les cellules du niveau actuel
        for (int i = 0; i < count; i++)
        {
            // Retirer le premier élément de la file
            Vector3Int currentCell = queue.Dequeue();
            terrain1=new ClasseTerrain(plainTilemap,mountainTilemap,forestTilemap,routeTilemap,riviereTilemap,currentCell);
            // Vérifier si la cellule courante n'est pas un terrain de type "rivière"
            if (terrain1.TerrainName != "Riviere")
            {
                attackableTiles.Add(currentCell);
                
                // Parcourir toutes les directions possibles
                foreach (Vector3Int direction in directions)
                {
                    // Calculer la position de la cellule adjacente dans cette direction
                    Vector3Int adjacentCell = currentCell + direction;
                    terrain2=new ClasseTerrain(plainTilemap,mountainTilemap,forestTilemap,routeTilemap,riviereTilemap,adjacentCell);
                    if(terrain1.TerrainName!="Forêt" && terrain1.TerrainName!="Montagne") {
                    // Vérifier si la cellule adjacente est dans la tilemap et n'a pas été visitée
                    if (plainTilemap.HasTile(adjacentCell) && !visited.Contains(adjacentCell))
                    {
                        // Vérifier si la cellule adjacente contient une rivière
                        if (terrain2.TerrainName != "Riviere")
                        {

                            // Ajouter la cellule adjacente à la file pour exploration future
                            queue.Enqueue(adjacentCell);
                            visited.Add(adjacentCell);
                            
                        }
                    }
                    }
                }
            }
        }

        // Augmenter la distance parcourue après avoir exploré un niveau
        distance++;
    }
}

void HighlightAttackRange()
{   
    ClasseTerrain terrain;
    foreach (Vector3Int cellPosition in attackableTiles)
    {
    
       plainTilemap.SetTile(cellPosition, newTile);
       terrain = new ClasseTerrain(plainTilemap,mountainTilemap,forestTilemap,routeTilemap,riviereTilemap,cellPosition);
       if(terrain.TerrainName=="Route") routeTilemap.SetTile(cellPosition, newTile);
       if(terrain.TerrainName=="Riviere") riviereTilemap.SetTile(cellPosition, newTile);
      

    }
} 

void StoreOriginalTiles()
{
    ClasseTerrain terrain;
    // Parcourir toutes les cellules de chaque Tilemap et enregistrer les tiles d'origine
    foreach (Vector3Int cellPosition in attackableTiles)
    {
        terrain = new ClasseTerrain(plainTilemap,mountainTilemap,forestTilemap,routeTilemap,riviereTilemap,cellPosition);
        
        if (terrain.TerrainName=="Route")
        {
            originalTiles[cellPosition] = routeTilemap.GetTile(cellPosition);
        }
        else if (terrain.TerrainName=="Riviere")
        {
            originalTiles[cellPosition] = riviereTilemap.GetTile(cellPosition);
        }
        else
        {
            originalTiles[cellPosition] = plainTilemap.GetTile(cellPosition);
        }
    }
}

public void RestoreOriginalTiles()
{
    ClasseTerrain terrain;
    // Parcourir la structure de données et restaurer les tiles d'origine sur les tilemaps appropriées
    foreach (var entry in originalTiles)
    {
        Vector3Int cellPosition = entry.Key;
        TileBase originalTile = entry.Value;
        terrain= new ClasseTerrain(plainTilemap,mountainTilemap,forestTilemap,routeTilemap,riviereTilemap,cellPosition);
        if (terrain.TerrainName=="Route")
        {
            routeTilemap.SetTile(cellPosition, originalTile);
        }
        else if (terrain.TerrainName=="Riviere")
        {
            riviereTilemap.SetTile(cellPosition, originalTile);
        }
        else
        {
            plainTilemap.SetTile(cellPosition, originalTile);
        }
    }

    // Effacer la structure de données après avoir restauré les tiles d'origine
    originalTiles.Clear();
}

public void afficherAttackRange() {
    CalculateAttackableTiles();
    if(attackableTiles.Count!=0) {
        StoreOriginalTiles();
        HighlightAttackRange();
    }
}
}
