using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject canvasDeplacement;
    public GameObject cursorRouge;
    public GameObject cursorBleu;
    private PlayerColor getActiveCursor() {
        if (cursorRouge.activeSelf) return PlayerColor.ROUGE;
        else return PlayerColor.BLEU;
    }
    public void Update() {
        if(!canvasDeplacement.activeSelf){
        PlayerColor cursorColor = getActiveCursor();
        Vector2 cursorPosition;
        if (cursorColor == PlayerColor.ROUGE){
            cursorPosition = cursorRouge.transform.position;
        }
        else 
        {
            cursorPosition = cursorBleu.transform.position;
        }

            if (cursorPosition.x > canvas1.transform.position.x && cursorPosition.y < canvas1.transform.position.y)
            {
                canvas1.SetActive(false);
                canvas2.SetActive(true);
            }
            else if(cursorPosition.x< canvas2.transform.position.x && cursorPosition.y < canvas2.transform.position.y) {
                canvas2.SetActive(false);
                canvas1.SetActive(true);
            }
    }   
    } 
}
