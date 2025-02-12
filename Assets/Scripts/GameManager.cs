using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Propriété statique pour accéder à l'instance unique de GameManager
    public static GameManager Instance { get; private set; }
    private string playerName1;
    private string playerName2;
    private Joueur player1;
    private Joueur player2;
    private PlayerColor tour=PlayerColor.NEUTRE;
    public GameObject CursorRouge;
    public GameObject CursorBleu;
    public static int nbtours=0;
    public static int nbjours=1;

    public void getPlayerName1(string nomUtilisateur) {
        if (nomUtilisateur != null)
        {
            // Utiliser la méthode getUsername de l'instance de NomUtilisateur pour obtenir le nom du joueur
            Instance.player1= new Joueur(nomUtilisateur,PlayerColor.ROUGE);
            
        }
        else
        {
            Debug.LogWarning("Aucune instance de NomUtilisateur trouvée dans la scène.");
        }
    }
    public void getPlayerName2(string nomUtilisateur) {
        if (nomUtilisateur != null)
        {
            // Utiliser la méthode getUsername de l'instance de NomUtilisateur pour obtenir le nom du joueur
            Instance.player2 = new Joueur(nomUtilisateur,PlayerColor.BLEU);
            
        }
        else
        {
            Debug.LogWarning("Aucune instance de NomUtilisateur trouvée dans la scène.");
        }
    }
    
    

    // Autres propriétés et méthodes de GameManager

    // Méthode appelée lors de la création de l'objet GameManager
    private void Awake()
    {
        // S'assurer qu'il n'y a qu'une seule instance de GameManager
        if (Instance == null)
        {
            Instance = this;
            // Ne pas détruire l'objet GameManager lors du chargement d'une nouvelle scène
            DontDestroyOnLoad(gameObject);
        }
       // else
        //{
            // Détruire l'objet si une autre instance de GameManager existe déjà
          //  Destroy(gameObject);
       // }
    }
    void setStartingPlayer() {
        Debug.Log("Les informations du joueur 1 : "+Instance.player1.getNom()+" "+Instance.player1.getCouleur());
        Debug.Log("Les informations du joueur 2 : "+Instance.player2.getNom()+" "+Instance.player2.getCouleur());
        float randomValue = Random.Range(0f,1f);
        if(randomValue<=0.5f) {
            //if(player1.getCouleur()!=null)
            tour=PlayerColor.ROUGE;
        }
        else{
            tour=PlayerColor.BLEU;
        }
    }
    public void testStartingPlayer() {
        //setStartingPlayer();
        Debug.Log("Le joueur qui commence est : "+tour);
        //if(tour!=null) {
        //if(tour==PlayerColor.ROUGE) Debug.Log("Il s'agit du joueur ROUGE ");
        //else Debug.Log("Il s'agit du joueur BLEU ");
        //}
        //else Debug.Log("Erreur dans tour");
    }

    public void startGame() {
        Instance.setStartingPlayer();
        if(tour!=PlayerColor.NEUTRE){
        if(tour==PlayerColor.ROUGE){
            CursorRouge.SetActive(true);
            Debug.Log("Le nom : "+Instance.player1.getNom());
        }
        else{
            Debug.Log("Le nom : "+Instance.player2.getNom());
            CursorBleu.SetActive(true);
        }
        }
        else{
            Debug.Log("Erreur dans tour");
        }

    }
    public Joueur getPlayer1() {
        return Instance.player1;
    }
    public Joueur getPlayer2() {
        return Instance.player2;
    }
}
