using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class DetailsTerrains : MonoBehaviour
{
    public TextMeshPro terrainText;
    void Start()
    {
        // Vérifier si le composant a été trouvé
        if (terrainText != null)
        {
            // Modifier le texte du TextMeshProUGUI
            terrainText.text = "Nouveau texte";
        }
        else
        {
            // Afficher un message d'erreur si le composant n'a pas été trouvé
            Debug.Log("Le composant TextMeshProUGUI avec le nom 'TypeName' n'a pas été trouvé.");
        }
    }
}
