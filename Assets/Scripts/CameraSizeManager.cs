using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeManager : MonoBehaviour
{
    public float minSize = 0f; // Taille de vue minimale de la caméra
    public float maxSize = 10f; // Taille de vue maximale de la caméra

    void Start()
    {
        // Obtenez la caméra principale de la scène
        Camera mainCamera = Camera.main;

        // Calculez la taille de vue en fonction de la résolution de l'écran
        float targetSize = Mathf.Lerp(minSize, maxSize, (float)Screen.height / Screen.width);

        // Assurez-vous que la taille de vue calculée est dans les limites définies
        targetSize = Mathf.Clamp(targetSize, minSize, maxSize);

        // Mettez à jour la taille de vue de la caméra
        mainCamera.orthographicSize = targetSize;
    }
}
