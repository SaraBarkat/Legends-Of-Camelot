using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NomUtilisateur : MonoBehaviour
{
    public string username;
    public InputField inputField;
    public GameManager gameObject=GameManager.Instance;
    // Start is called before the first frame update
    void Start()
    {
        inputField.onValueChanged.AddListener(UpdateNomUtilisateur);
    }

     // Update is called once per frame
    void UpdateNomUtilisateur(string newText)
    {
        inputField.placeholder.GetComponent<Text>().text = newText;
        username=newText;        
        gameObject.getPlayerName1(username);
    }

    public string getUsername() {
        return username;
    }
}
