using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUnitsMovement : MonoBehaviour
{
    public GameObject cursorRouge;
    public GameObject cursorBleu;
    public void resetUnits() {
        PlayerColor cursorColor;
        if(cursorRouge.activeSelf) cursorColor=PlayerColor.ROUGE;
        else cursorColor=PlayerColor.BLEU;
        GameObject[] unitObjects = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unitObject in unitObjects ) {
        if(unitObject!=null) {
            PlayerMovement unitScript = unitObject.GetComponent<PlayerMovement>();
            if(unitScript.color==cursorColor && unitScript.isMoved==true) {
                unitScript.isMoved=false;
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
}
