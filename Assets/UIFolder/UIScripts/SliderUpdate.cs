using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdate : MonoBehaviour
{
    public Slider progressBar; // Référence vers le Slider à remplir
    public float fillSpeed = 0.5f; // Vitesse de remplissage du slider
    public float targetValue = 1f; // Valeur cible du slider (0-1)
    public GameObject CurrentCanvas;
    public GameObject CanvasToEnable;
    void Start()
    {
        // Démarrer le remplissage automatique du slider
        StartAutoFill();
    }

    void StartAutoFill()
    {
        // Définir la valeur actuelle du slider à zéro
        progressBar.value = 0f;

        // Lancer le remplissage progressif du slider
        StartCoroutine(FillProgressBar());
    }

    IEnumerator FillProgressBar()
    {
        // Tant que la valeur du slider n'a pas atteint la valeur cible
        while (progressBar.value < targetValue)
        {
            // Augmenter progressivement la valeur du slider
            progressBar.value += fillSpeed * Time.deltaTime;
            yield return null; // Attendre la prochaine frame
        }

        CurrentCanvas.SetActive(false);
        CanvasToEnable.SetActive(true);
    }
}
