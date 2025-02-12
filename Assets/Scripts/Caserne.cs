using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonBatiment;

public class Caserne : Batiment
{
    public GameObject[] unitesRougesPrefabs; // Prefabs des unités rouges
    public GameObject[] unitesBleuesPrefabs; // Prefabs des unités bleues
    private GameObject prefab;
    private Joueur player;
    private bool accept = false;
    public void setPlayer(Joueur joueur) {
      player=joueur;
    }
  public override void Capture(Unit unite)
        {
            // Logique de capture spécifique au village
            base.Capture(unite); // Appelez la méthode Capture de la classe de base si nécessaire
        }
public void createunit(int numero){
     switch (numero)
    {
        case 0:
            if(player.getRessources()>=1000){
              accept = true;
            // Créer l'unité appropriée selon la couleur
            prefab = col == PlayerColor.ROUGE ? unitesRougesPrefabs[0] : unitesBleuesPrefabs[0];
            player.setRessources(player.getRessources()-1000);
            }
            else Debug.Log("Ressources insuffisantes");
            break;
              case 1:
            // Créer l'unité appropriée selon la couleur
            if(player.getRessources()>=1500) {
            prefab = col == PlayerColor.ROUGE ? unitesRougesPrefabs[1] : unitesBleuesPrefabs[1];
            accept = true;
            player.setRessources(player.getRessources()-1500);
            }
            else Debug.Log("Ressources insuffisantes");
            break;
              case 2:
            if(player.getRessources()>=2000){
            // Créer l'unité appropriée selon la couleur
            prefab = col == PlayerColor.ROUGE ? unitesRougesPrefabs[2] : unitesBleuesPrefabs[2];
            player.setRessources(player.getRessources()-2000);
            accept = true;}
            else Debug.Log("Ressources insuffisantes");
            break;
              case 3:
            if(player.getRessources()>=3000){
            // Créer l'unité appropriée selon la couleur
            player.setRessources(player.getRessources()-3000);
            prefab = col == PlayerColor.ROUGE ? unitesRougesPrefabs[3] : unitesBleuesPrefabs[3];
            accept = true;}
            else Debug.Log("Ressources insuffisantes");
            break;
              case 4:
              if(player.getRessources()>=3500){
                player.setRessources(player.getRessources()-3500);
            // Créer l'unité appropriée selon la couleur
            prefab = col == PlayerColor.ROUGE ? unitesRougesPrefabs[4] : unitesBleuesPrefabs[4];
            accept = true;}
            else Debug.Log("Ressources insuffisantes");
            break;
              case 5:
              if(player.getRessources()>=4000){
                player.setRessources(player.getRessources()-4000);
            // Créer l'unité appropriée selon la couleur
            prefab = col == PlayerColor.ROUGE ? unitesRougesPrefabs[5] : unitesBleuesPrefabs[5];
            accept=true;}
            else Debug.Log("Ressources insuffisantes");
            break;
              case 6:
              if(player.getRessources()>=5000){
            // Créer l'unité appropriée selon la couleur
            player.setRessources(player.getRessources()-5000);
            prefab = col == PlayerColor.ROUGE ? unitesRougesPrefabs[6] : unitesBleuesPrefabs[6];
            accept=true;}
            else Debug.Log("Ressources insuffisantes");
            break;
        // Ajouter d'autres cas pour les autres numéros d'unité si nécessaire
        default:
            Debug.LogWarning("Numéro d'unité invalide.");
            return;
    }
  if (prefab != null && accept)
    {
      if (prefab.activeSelf==false){

      // Activer le prefab
        prefab.SetActive(true);

        // Créer l'unité à la position de la caserne avec aucune rotation
        GameObject unite = Instantiate(prefab, transform.position, Quaternion.identity);

        // Modifier le nom de l'unité pour supprimer la partie "(clone)"
        unite.name = unite.name.Replace("(Clone)", "");

        // Désactiver le prefab après avoir créé l'unité
        prefab.SetActive(false);
      }
       else {
         // Créer l'unité à la position de la caserne avec aucune rotation
        GameObject unite = Instantiate(prefab, transform.position, Quaternion.identity);

        // Modifier le nom de l'unité pour supprimer la partie "(clone)"
        unite.name = unite.name.Replace("(Clone)", "");
       }

       

    
    }

}
public Joueur getPlayer() {
  return player;
}
}
