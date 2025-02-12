using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisableMovement : MonoBehaviour
{
    public GameObject CanvasTerrain;
    public GameObject CanvasCapture;
    public GameObject CanvasAttack;
    public GameObject cursorRouge;
    public GameObject cursorBleu;
    public Tilemap plainTilemap;
    public Tilemap batimentTilemap;
    private PlayerMovement playermovementselectionne;
    private Unit unit;
    public Collider2D[] hitEnemies = new Collider2D[0];
    public bool containsBatiment(Vector3Int cellPosition) {
        if(batimentTilemap!=null && batimentTilemap.HasTile(cellPosition)) {
            return true;
        }
        else return false;
    }
    public void DisableMove() {
        playermovementselectionne =null;
        unit=null;
        bool cancapture = false;
        GameObject[] unitObjects = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unitObject in unitObjects ) {
        if(unitObject!=null) {
            PlayerMovement unitScript = unitObject.GetComponent<PlayerMovement>();
            PlayerColor cursorColor=unitScript.color;
            if(unitScript!=null && unitScript.enabled==true) {
                unit = unitObject.GetComponent<Unit>();  
                cancapture=unit.cancapture; 
                float range=unit.attackRange;
                hitEnemies = Physics2D.OverlapCircleAll(unitObject.transform.position, range, unit.enemyLayers);
                playermovementselectionne = unitScript;
                unitScript.enabled=false;
                Debug.Log("Script desactive"); 
                Vector3Int cellPosition = plainTilemap.WorldToCell(unitObject.transform.position);
                if(containsBatiment(cellPosition) && cancapture==true) {
                CanvasCapture.SetActive(true);
                }
                else if(hitEnemies!=null && hitEnemies.Length>0) {
                CanvasAttack.SetActive(true);
                }
                else {CanvasTerrain.SetActive(true);
                if(cursorColor==PlayerColor.ROUGE) {cursorRouge.GetComponent<Renderer>().enabled=true;}
                else {cursorBleu.GetComponent<Renderer>().enabled=true;}
            }
            }
            else{
                Debug.Log("Le gameobject ne contient pas de PlayerMovement");
            }
            
        }
        else {
            Debug.Log("Object not found");
        }
        }
    }
    public PlayerMovement getpms(){
        return playermovementselectionne;
    }
    public Unit getunit(){
        return unit;
    }
}
