using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RessourcesText : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI restext;
    public GameManager gameManager;
    PlayerColor col = PlayerColor.NEUTRE;
    public void setText(string newText) {
    if (restext != null)
            {
                // Modifier le texte du TextMeshProUGUI
                restext.text = newText;
            }
            else
            {
                // Afficher un message d'erreur si le composant n'a pas été trouvé
                Debug.Log("Le composant TextMeshProUGUI avec le nom 'TypeName' n'a pas été trouvé.");
            }
    }
    void Start() {
        if(col==PlayerColor.ROUGE) {
            int res = gameManager.getPlayer1().getRessources();
            string ress = res.ToString();
            setText(ress);
        }
        else {
            int res = gameManager.getPlayer2().getRessources();
            string ress = res.ToString();
            setText(ress);
        }
    }

    void Update() {
        if(col==PlayerColor.ROUGE) {
            int res = gameManager.getPlayer1().getRessources();
            string ress = res.ToString();
            setText(ress);
        }
        else {
            int res = gameManager.getPlayer2().getRessources();
            string ress = res.ToString();
            setText(ress);
        }
    }
    public void setCol(PlayerColor color) {
        col = color;
    } 

}
