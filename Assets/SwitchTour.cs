using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchTour : MonoBehaviour
{
    public GameObject curseurRouge;
    public GameObject curseurBleu;
    public TextMeshProUGUI terrainText;
    public GameObject nbjour;
    public GameManager gameManager;
    public GameObject finJeu;

    public TextMeshProUGUI nomText;
    public float activeDuration = 0.3f; // Durée pendant laquelle le composant reste activé

    public void finTour()
    {
        if (curseurRouge.activeSelf)
        {
            CursorControl script = curseurBleu.GetComponent<CursorControl>();
            if(!script.unitesIsEmpty()){
            curseurRouge.SetActive(false);
            curseurBleu.SetActive(true);
            }
            else{
            string nom = gameManager.getPlayer1().getNom();
            Debug.Log("Le nom : " + nom);
            nomText.text=nom;
            finJeu.SetActive(true);
            }
        }
        else
        {
        CursorControl script = curseurRouge.GetComponent<CursorControl>();
        if(!script.unitesIsEmpty()){
        curseurBleu.SetActive(false);
        curseurRouge.SetActive(true);
        }
        else {
            string nom = gameManager.getPlayer2().getNom();
            Debug.Log("Le nom : " + nom);
            nomText.text=nom;
            finJeu.SetActive(true);
        }
        }

        GameManager.nbtours++;
        if (GameManager.nbtours % 2 == 0)
        {
            GameManager.nbjours++;
            GameObject[] villageObjects = GameObject.FindGameObjectsWithTag("Village");
            foreach(GameObject village in villageObjects ) {
                Village vil = village.GetComponent<Village>();
                Debug.Log("Village color : "+vil.getCurrColor());
                if(vil.getCurrColor()==PlayerColor.ROUGE) {
                    gameManager.getPlayer1().addressources(500);
                    Debug.Log("Ressources du joueur 1 : "+gameManager.getPlayer1().getRessources());
                }
                else if(vil.getCurrColor()==PlayerColor.BLEU) {
                    gameManager.getPlayer2().addressources(500);
                }
            }
            Debug.Log("le nombre de jours : " + GameManager.nbjours);
            if (terrainText != null)
            {
                // Modifier le texte du TextMeshProUGUI
                terrainText.text = GameManager.nbjours.ToString();
            }
            else
            {
                // Afficher un message d'erreur si le composant n'a pas été trouvé
                Debug.Log("Le composant TextMeshProUGUI avec le nom 'TypeName' n'a pas été trouvé.");
            }
        }
    }

}
