using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f; // Vitesse de déplacement par case
    public float tileSize = 1f; // Taille d'une case
    public Rigidbody2D rb;
    Vector2 targetPosition; // Position cible
    Vector2 initialPosition;
    public Tilemap plainTilemap; // Tilemap pour les plaines
    public Tilemap mountainTilemap; // Tilemap pour les montagnes
    public Tilemap forestTilemap; // Tilemap pour les forêts
    public Tilemap routeTilemap;
    public Tilemap riviereTilemap;
    public Tilemap villageTilemap;
    public TileBase newTile;
    public int movementRange;
    private int nbpas;
    public GameObject CanvasTerrain;
    public GameObject CanvasBatiment;
    public bool isMoved;
    // Cases accessibles pour le déplacement
    private List<Vector3Int> accessibleTiles = new List<Vector3Int>();
    private Dictionary<Vector3Int, TileBase> originalTiles = new Dictionary<Vector3Int, TileBase>();
    public PlayerColor color;
    void Start()
    {
        if(movementRange==3) {
        initialPosition=rb.position;
        nbpas=0;
        }
        targetPosition = initialPosition;
    }
    void OnEnable()
{
    RecalculateMovementRange();

    HighlightMovementRange();
}
void RecalculateMovementRange()
{
        targetPosition = rb.position;
        CalculateAccessibleTiles();
        StoreOriginalTiles();
    }

public void Restart()
    {
        nbpas = 0;
        // Ramène l'unité à sa position initiale
        rb.position=initialPosition;
        // Actions à effectuer lors du redémarrage du mouvement
        RecalculateMovementRange();
        HighlightMovementRange();
    }
    void Update()
    {
        
        // Gestion des touches du clavier
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2.down);
        }
        
        
    }

 /*public void MoveUnit(Vector3Int cellPosition)
{
    // Obtenir le nom du terrain sur lequel se trouve l'unité
    string terrainName = GetTerrainName(cellPosition);

    if (!string.IsNullOrEmpty(terrainName))
    {
        Debug.Log("Unité déplacée sur la cellule : " + cellPosition + ", Terrain : " + terrainName);
        // Vous pouvez également utiliser le terrainName comme vous le souhaitez
         if(containsUnit(cellPosition)) Debug.Log("Contient une unite !");
            else Debug.Log("Ne contient pas d unite !");
            if(containsBuilding(cellPosition)) Debug.Log("Contient un batiment !");
            else Debug.Log("Ne contient pas de batiment !");
    }
    else
    {
        Debug.LogWarning("Position invalide !");
    }
}
*/

  /*public string GetTerrainName(Vector3Int cellPosition)
{
    if (mountainTilemap != null && mountainTilemap.HasTile(cellPosition))
    {
        return "Montagne";
    }
   
    else if (forestTilemap != null && forestTilemap.HasTile(cellPosition))
    {
        return "Forêt";
    }
     else if (routeTilemap != null && routeTilemap.HasTile(cellPosition))
    {
        return "Route";
    }
    else if (riviereTilemap != null && riviereTilemap.HasTile(cellPosition)) {
        return "Riviere";
    }
     else if (plainTilemap != null && plainTilemap.HasTile(cellPosition))
    {
        return "Plaine";
    }
    
    else
    {
        return "Inconnu";
    }
}*/


    void Move(Vector2 direction)
    {
        // Calculer la nouvelle position cible en fonction de la direction
        Vector3Int cellPosition = plainTilemap.WorldToCell(targetPosition + direction * tileSize);
        if (plainTilemap.HasTile(cellPosition))
        {
            if (!containsUnit(cellPosition)&& accessibleTiles.Contains(cellPosition)&& nbpas<movementRange)
        {
            // Aucune unité à la nouvelle position, vous pouvez effectuer le mouvement
            targetPosition += direction * tileSize;
            nbpas++;
            //Vector3Int cellPosition = plainTilemap.WorldToCell(targetPosition + direction * tileSize);
           // if(containsVillage(cellPosition)) {
          //  CanvasTerrain.SetActive(false);
           // CanvasBatiment.SetActive(true);
        //}
            // Appeler la méthode MoveUnit ici
           // MoveUnit(cellPosition);
        }
        else
        {
            // Il y a une unité à la nouvelle position, ne rien faire ou gérer le comportement selon votre logique
            Debug.Log("Une unité est présente à la nouvelle position. Le déplacement est bloqué.");
        }
        }
        else
        {
            // Pas de tile à la nouvelle position, ne rien faire ou gérer le comportement selon votre logique
        }
    }

    void FixedUpdate()
    {
        // Déplacer l'objet vers la position cible
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
        
        
    }

    public bool containsUnit(Vector3Int cellPosition) {
        RaycastHit2D hit = Physics2D.Raycast(plainTilemap.GetCellCenterWorld(cellPosition), Vector2.down, 0.1f);
        if(hit.collider!=null) {
            if(hit.collider.CompareTag("Unit") ) {
                return true;
            }
            else return false;
        }
        else return false;
    }

    public bool containsBuilding(Vector3Int cellPosition) {
        RaycastHit2D hit = Physics2D.Raycast(plainTilemap.GetCellCenterWorld(cellPosition), Vector2.down, 0.1f);
        if(hit.collider!=null) {
            if(hit.collider.CompareTag("Building") ) {
                return true;
            }
            else return false;
        }
        else return false;
    }



    void CalculateAccessibleTiles()
{
    ClasseTerrain terrain1, terrain2;
     // Effacer la liste des cases accessibles précédentes
    accessibleTiles.Clear();
    // Obtenir la position actuelle de l'unité
    Vector3Int unitPosition = plainTilemap.WorldToCell(targetPosition);

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
    while (queue.Count > 0 && distance <= movementRange)
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
                accessibleTiles.Add(currentCell);
                
                // Parcourir toutes les directions possibles
                foreach (Vector3Int direction in directions)
                {
                    // Calculer la position de la cellule adjacente dans cette direction
                    Vector3Int adjacentCell = currentCell + direction;
                    terrain2=new ClasseTerrain(plainTilemap,mountainTilemap,forestTilemap,routeTilemap,riviereTilemap,adjacentCell);
                    if(terrain1.TerrainName!="Forêt" && terrain1.TerrainName!="Montagne" || containsUnit(currentCell)) {
                    // Vérifier si la cellule adjacente est dans la tilemap et n'a pas été visitée
                    if (plainTilemap.HasTile(adjacentCell) && !visited.Contains(adjacentCell))
                    {
                        // Vérifier si la cellule adjacente contient une rivière
                        if (terrain2.TerrainName != "Riviere" && !containsUnit(adjacentCell))
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



   void HighlightMovementRange()
{   
    Color myColor = new Color32(0x49, 0x49, 0x49, 0x49);
    ClasseTerrain terrain;
    foreach (Vector3Int cellPosition in accessibleTiles)
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
    foreach (Vector3Int cellPosition in accessibleTiles)
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


// À appeler lorsque le script est terminé ou que vous souhaitez restaurer les tiles d'origine
void OnDisable()
{
    initialPosition=rb.position;
    movementRange=3;
    nbpas=0;
    isMoved=true;
    // Restaurer les tiles d'origine
    RestoreOriginalTiles();
   
}

}

