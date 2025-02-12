using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTerrain : MonoBehaviour
{
    public GameObject CanvasTerrain;
    // Start is called before the first frame update
    void Start()
    {
        CanvasTerrain.SetActive(false); 
    }


}
