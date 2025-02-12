using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject canvasToDisable;
    public GameObject canvasToEnable;
    public PlayerColor tour;


    public void OnButtonClick(){
        canvasToDisable.SetActive(false);
        canvasToEnable.SetActive(true);

    }

  
}
