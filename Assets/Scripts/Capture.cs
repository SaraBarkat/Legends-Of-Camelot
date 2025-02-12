using UnityEngine;
using UnityEngine.UI;
using MonBatiment;

public class Capture : MonoBehaviour
{
    public DisableMovement script;
   // public CursorControl curseur;
    public GameObject CanvasTerrain;
    void Start()
    {
        // Récupère le composant Button attaché au bouton
        Button button = GetComponent<Button>();

        // Ajoute un écouteur à l'événement de clic du bouton
        button.onClick.AddListener(OnCaptureButtonClicked);
    }

    // Méthode appelée lorsqu'un clic est détecté sur le bouton
    void OnCaptureButtonClicked()
{        
            PlayerMovement unitScript = script.getpms();
            Unit unite = script.getunit();
            if (unitScript != null )
            {
                Debug.Log("unite detectee");
                // Accéder à la position de l'objet actif
                Vector3 unitPosition = unitScript.rb.transform.position;
                Debug.Log("Position de l'unité active : " + unitPosition);
               if (unite !=null){
               Debug.Log("hp de l unite active : " + unite.currentHP);
               } 
               else{
                Debug.Log("script unite = null " );
               }
                // Trouver le village à cette position
                Batiment batiment = GetBatimentAtPosition(unitPosition);
                if (batiment != null)
                {
                    // Capture du village par l'unité active
                    batiment.Capture(unite);
                    Debug.Log("Le village est capture: "); 
                }
                else
                {
                    Debug.Log("Aucun village trouvé à la position de l'unité active.");
                }
                unitScript.enabled=false;
            }
  CanvasTerrain.SetActive(true); 
//curseur.

}


Batiment GetBatimentAtPosition(Vector3 position)
    {
        // Trouve tous les objets de type Village dans la scène
        Batiment[] batiments = FindObjectsOfType<Batiment>();

        // Parcours tous les villages pour trouver celui à la position spécifiée
        foreach (Batiment batiment in batiments)
        {
            // Récupère la position du village 
            Debug.Log("Le batiment est : " + batiment.transform.position); 
            // Spécifiez un intervalle pour traiter l'incertitude
            float intervalle = 0.5f; // Par exemple, 0.5 unité

            // Vérifie si la distance entre les deux positions est inférieure à l'intervalle
            if (Vector3.Distance(position, batiment.transform.position) <= intervalle)
            {
                // Retourne le village trouvé
                return batiment;
            }
        }

        // Aucun village trouvé à cette position, retourne null
        return null;
    }
}