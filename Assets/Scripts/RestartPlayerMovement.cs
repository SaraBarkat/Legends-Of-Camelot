using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPlayerMovement : MonoBehaviour
{
    public void RestartMovement()
    {
        // Trouver tous les objets avec le tag "Unit"
        GameObject[] unitObjects = GameObject.FindGameObjectsWithTag("Unit");

        foreach (GameObject unitObject in unitObjects)
        {
            // Obtenir le composant PlayerMovement
            PlayerMovement playerMovement = unitObject.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                if(playerMovement.enabled==true) {
                // Décrémenter la valeur de movementRange
                playerMovement.movementRange--;

                playerMovement.RestoreOriginalTiles();
                // Réinitialiser le mouvement
                playerMovement.Restart();
                }
            }
        }
    }
}
