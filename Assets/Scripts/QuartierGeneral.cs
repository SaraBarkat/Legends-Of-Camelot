using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonBatiment;
using TMPro;

public class QuartierGeneral : Batiment
{

    public GameObject finJeu;
    public GameObject curseurRouge;
    public GameObject curseurBleu;
    public TextMeshProUGUI nomText;
    public GameManager gameManager;
    public override void Capture(Unit unite)
        {
              if (col != unite.col ||currcol!=unite.col ){
        PlayCaptureAnimation();
        if (unite.col != currcol)
        {
            currcol = unite.col;
            currlife = unite.currentHP;
        }
        else
        {
          
            
            currlife +=unite.currentHP ;
            if(currlife>=maxlife) currlife=maxlife;
    
            Debug.Log("currlife = " + currlife);
            if (currlife >= maxlife)
            {
                if (curseurRouge.activeSelf)
                {
            CursorControl script = curseurBleu.GetComponent<CursorControl>();
            string nom = gameManager.getPlayer1().getNom();
            Debug.Log("Le nom : " + nom);
            nomText.text=nom;
            finJeu.SetActive(true);
        
        }
        else
        {
        CursorControl script = curseurRouge.GetComponent<CursorControl>();
            string nom = gameManager.getPlayer2().getNom();
            Debug.Log("Le nom : " + nom);
            nomText.text=nom;
            finJeu.SetActive(true);
        
        }
            }
        }
        }
        }
}
