using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAttack : MonoBehaviour
{
    public DisableMovement script; 
    private Collider2D[] enemies = new Collider2D[0];
    public void attackUnit() {
        PlayerMovement unitScript = script.getpms();
        Debug.Log("Player movement obtenu");
        Unit unite=script.getunit(); 
        Debug.Log("Unit obtenu");
        if(unitScript!=null) {
            Debug.Log("Unite detectee");
            Vector3 unitPosition = unitScript.rb.transform.position;
            Debug.Log("Position de l'unité active : "+unitPosition);
            if(unite != null) {
                enemies=script.hitEnemies;
                unite.Attack(); 
            }
            else{
                Debug.Log("Script null");
            }

        }
    }
}
