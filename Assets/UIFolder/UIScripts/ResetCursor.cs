using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCursor : MonoBehaviour
{
    public DisableMovement script;
    public GameObject cursorRouge;
    public GameObject cursorBleu;
    public void resetCursor() {
        PlayerMovement playerMovement=script.getpms();
        if(playerMovement.color==PlayerColor.ROUGE) {
            cursorRouge.GetComponent<Renderer>().enabled=true;
        }
        else{
            cursorBleu.GetComponent<Renderer>().enabled=true;
        }
    }
}
