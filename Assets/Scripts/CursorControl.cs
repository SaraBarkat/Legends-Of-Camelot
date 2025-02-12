using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using TMPro;
using MonBatiment;

public class CursorControl : MonoBehaviour
{
    public float moveSpeed = 6f; // Vitesse de déplacement par case
    public float tileSize = 1f; // Taille d'une case
    public Rigidbody2D rb;
    Vector3 cursorPosition; // Position du curseur en Vector3
 
    Vector2 targetPosition; // Position cible
    public Tilemap plainTilemap; // Tilemap pour les plaines
    public Tilemap mountainTilemap; // Tilemap pour les montagnes
    public Tilemap forestTilemap; // Tilemap pour les forêts
    public Tilemap routeTilemap;
    public Tilemap riviereTilemap;
    public Tilemap villageTilemap;
    public Terrain ter;
    private ClasseTerrain terrain;
    public GameObject CanvasDeplacement;
    public GameObject CanvasTerrain;
    public GameObject CanvasUnite;
    public GameObject CanvasBatiment;
    public GameObject CanvasMenu;
    public GameObject creationunite;

    public TextMeshProUGUI terrainText;
    public TextMeshProUGUI terrainDefense;
    public PlayerColor cursorColor;
    public List<Vector3Int> adjacentTiles = new List<Vector3Int>();
    private Caserne caserne;
    public List<GameObject> unites;
    public RessourcesText ressourcesText;


    public void DetailsTerrains() {
        Vector3Int cellPosition = plainTilemap.WorldToCell(rb.position);        
        ClasseTerrain terrain = new ClasseTerrain(plainTilemap,mountainTilemap,forestTilemap,routeTilemap,riviereTilemap,cellPosition);
        if (terrainText != null)
        {
            
            // Modifier le texte du TextMeshProUGUI
            terrainText.text = terrain.TerrainName;
        }
        else
        {
            // Afficher un message d'erreur si le composant n'a pas été trouvé
            Debug.Log("Le composant TextMeshProUGUI avec le nom 'TypeName' n'a pas été trouvé.");
        }
        if(terrainDefense!=null) {
            terrainDefense.text=terrain.Defense.ToString();
        }
    }
    

       void Start()
    {
        targetPosition = rb.position; // Initialiser la position cible
        cursorPosition = transform.position; // Initialiser la position du curseur
        //Vector3Int cellPosition = plainTilemap.WorldToCell(rb.position);        
        //terrain = new ClasseTerrain(plainTilemap,mountainTilemap,forestTilemap,routeTilemap,riviereTilemap,cellPosition);
        DetailsTerrains();
        CanvasTerrain.SetActive(true);
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
    public bool unitesIsEmpty() {
        return unites.Count==0;
    }
    void Move(Vector2 direction)
    {
        // Calculer la nouvelle position cible en fonction de la direction
       Vector3Int cellPosition = plainTilemap.WorldToCell(targetPosition + direction * tileSize);
        if (plainTilemap.HasTile(cellPosition))
        { 
            targetPosition += direction * tileSize;
        }
      cursorPosition = transform.position; 
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

    public void deleteUnit(GameObject gm) {
        
        // Si l'objet existe dans la liste, le supprimer
        if (unites.Contains(gm))
        {
            unites.Remove(gm);
        }

    }
    public bool containsVillage(Vector3Int cellPosition) {
        if(villageTilemap!=null && villageTilemap.HasTile(cellPosition)) {
            return true;
        }
        else return false;
    }
   Batiment GetBatimentAtPosition(Vector3 position)
    {
        // Trouve tous les objets de type Village dans la scène
        Batiment[] batiments = FindObjectsOfType<Batiment>();

        // Parcours tous les villages pour trouver celui à la position spécifiée
        foreach (Batiment batiment in batiments)
        {
            // Récupère la position du village 
            Debug.Log("Le batiment est : " + batiment.transform.position); 
            // Spécifiez un intervalle pour traiter l'incertitude
            float intervalle = 0.5f; // Par exemple, 0.5 unité

            // Vérifie si la distance entre les deux positions est inférieure à l'intervalle
            if (Vector3.Distance(position, batiment.transform.position) <= intervalle)
            {
                // Retourne le village trouvé
                return batiment;
            }
        }

        // Aucun village trouvé à cette position, retourne null
        return null;
    }

    IEnumerator ShowAttackRangeForDuration(float duration, Vector3Int cellPosition)
{
    // Afficher la portée d'attaque
    if (containsUnit(cellPosition))
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero, Mathf.Infinity);
        // Récupérer le script attaché à l'objet détecté
        Unit unitScript = hit.collider.gameObject.GetComponent<Unit>();
        unitScript.afficherAttackRange();
    }

    // Attendre pendant la durée spécifiée
    yield return new WaitForSeconds(duration);

    // Restaurer les tiles originales
    // Insérez ici l'appel à votre fonction pour restaurer les tiles originales
    // Par exemple : restoreOriginalTiles();

    // Masquer la portée d'attaque (ou effectuer d'autres actions à la fin de la temporisation si nécessaire)
    // Par exemple, pour masquer la portée d'attaque :
    if (containsUnit(cellPosition))
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero, Mathf.Infinity);
        // Récupérer le script attaché à l'objet détecté
        Unit unitScript = hit.collider.gameObject.GetComponent<Unit>();
        unitScript.RestoreOriginalTiles();
    }
}

    void FixedUpdate()
    {
        // Déplacer l'objet vers la position cible
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
        DetailsTerrains();
        Vector3Int cellPosition = plainTilemap.WorldToCell(rb.position);
        if (Input.GetKeyDown(KeyCode.E)) {
            //CanvasTerrain.SetActive(true);
        cursorPosition = transform.position;
        Debug.Log("position du curseur : " + cursorPosition);
         if(containsUnit(cellPosition))
            {
                Debug.Log("unite detectee " );
                RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero, Mathf.Infinity);            
                // Récupérer le script attaché à l'objet détecté
                PlayerMovement unitScript = hit.collider.gameObject.GetComponent<PlayerMovement>();
                if (unitScript != null && unitScript.color==cursorColor && unitScript.isMoved==false)
                {
                    CanvasTerrain.SetActive(false);
                    // Activer le script attaché à l'objet détecté
                    unitScript.enabled = true;
                    rb.GetComponent<Renderer>().enabled=false;
                    CanvasDeplacement.SetActive(true);
                }
                
                
            
            }
            else
            {
               
                //Vector3 position = new Vector3(cellPosition.x,cellPosition.y, cellPosition.z);
                Batiment batiment=GetBatimentAtPosition(cursorPosition);
                if (batiment != null){
                    Debug.Log("Batiment detectee ! " ); 
                    // Vérifie si c'est une caserne
                    caserne = batiment.GetComponent<Caserne>();
                    if (caserne != null)
                    {
                        if (caserne.col==cursorColor){
                        Debug.Log("Caserne detectee ! " );
                        ressourcesText.setCol(caserne.getCurrColor());
                        creationunite.SetActive(true);   
                        }
                        
                    }

                }
                else {
                CanvasMenu.SetActive(true);
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.R)){
            StartCoroutine(ShowAttackRangeForDuration(5f,cellPosition));
        }
        
        /*else if (Input.GetKeyDown(KeyCode.R))
    {

        Vector3Int cellPosition = plainTilemap.WorldToCell(rb.position);
        
         if(containsUnit(cellPosition))
            {
                
                RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero, Mathf.Infinity);
        
            
                // Récupérer le script attaché à l'objet détecté
                MonoBehaviour unitScript = hit.collider.gameObject.GetComponent<MonoBehaviour>();
                if (unitScript != null && unitScript.enabled==true)
                {
                    // Activer le script attaché à l'objet détecté
                    unitScript.enabled = false;
                }
            
            
            }
            else
            {
                Debug.Log("Raycast did not hit anything.");
            }
        
    }*/
    }
   /* void verifyAdjacentTiles() {
        adjacentTiles.Clear();
        Vactor3Int unitPosition = plainTilemap.WorldToCell(rb.position);
        Vector3Int[] directions = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };
        foreach(Vector3Int direction in directions) {
            Vector3Int adjacentCell = unitPosition + direction;
            if(containsUnit(adjacentCell)) {
                adjacentTiles.add(adjacentCell);
            }
        }

    }*/
    public Caserne getcaserne(){
        return caserne;
    }
    public PlayerColor GetPlayerColor(){
        return cursorColor;
    }
}
