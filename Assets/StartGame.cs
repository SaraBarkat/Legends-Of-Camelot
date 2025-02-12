using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameManager gameObject;

    public void onButtonClicked(){
        gameObject.startGame();
    }
}
