using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUnit : MonoBehaviour
{
    public int numero;
    public GameObject curseur1;
    public GameObject curseur2;
    private CursorControl scriptcurseur1;
    private CursorControl scriptcurseur2;
    public GameManager gameManager;
    private Joueur player;
    // Start is called before the first frame update
    void Start() 
    {
        // Récupère le composant Button attaché au bouton
        Button button = GetComponent<Button>();

        // Ajoute un écouteur à l'événement de clic du bouton
        button.onClick.AddListener(onbuttonclicked);
        scriptcurseur1 = curseur1.GetComponent<CursorControl>();
        scriptcurseur2 = curseur2.GetComponent<CursorControl>();
    }
    CursorControl activecursor (){
     if (curseur1.activeSelf){
        player = gameManager.getPlayer1();
        return scriptcurseur1;
     }
     else {
        player = gameManager.getPlayer2();
        return scriptcurseur2;
     }
    }
    void onbuttonclicked(){
     CursorControl curseur=activecursor ();
     Caserne caserne=curseur.getcaserne();
     caserne.setPlayer(player);
     caserne.createunit(numero);

     if (curseur1.activeSelf){
        gameManager.getPlayer1().setRessources(caserne.getPlayer().getRessources());
     }
     else {
       gameManager.getPlayer2().setRessources(caserne.getPlayer().getRessources());
     }
    }

  
}
