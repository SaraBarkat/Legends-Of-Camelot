using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnulerSave : MonoBehaviour
{
    public GameObject CanvasToDisable ;
    public void OnButtonClick(){
        CanvasToDisable.SetActive(false);
    }
}
