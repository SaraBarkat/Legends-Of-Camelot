using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unite : MonoBehaviour
{
    // Propriétés communes à toutes les unités
    public int PointsDeVie;
    public int PointsDeMouvement;
    public int Degats;
    public bool EstAttaquee;
    public bool EstDeplacee;
    public int cout ;
    public TypeUnite TypeUnite;
    public Joueur proprietaire ; // * ajouté par malak 
    
    // Enum pour le type d'armée
    public enum TypeArmee
    {
        Rouge,
        Bleu
    }
    public TypeArmee TypeArme;

    public int GetLife() { // *

        return PointsDeVie;
    }

    public Joueur GetPlayer() {  //*

        return proprietaire ;
    }

    // Méthode pour déplacer l'unité
    public abstract void SeDeplacer(int distance);

    // Méthode pour attaquer une autre unité
    public abstract void Attaquer(Unite uniteCible);

    // Méthode pour recevoir des dégâts
    public abstract void RecevoirDegats(int degats);

    // Méthode pour mettre à jour le sprite des points de vie exemple si HP =0 le sprite va disparaitre 
    public abstract void UpdateSpritePointsDeVie();
}






//----------------------------------------------------------------------------------------------------------------------

// Classe abstraite pour les unités terrestres
public abstract class UniteTerrestre : Unite
{
    // Méthode spécifique pour se déplacer pour les unités terrestres
    public override void SeDeplacer(int distance)
    {
        // Implémentation pour se déplacer sur terre
    }

    // Méthode spécifique pour attaquer pour les unités terrestres
    public override void Attaquer(Unite uniteCible)
    {
        // Implémentation pour attaquer sur terre
    }

    // Méthode spécifique pour recevoir des dégâts pour les unités terrestres
    public override void RecevoirDegats(int degats)
    {
        // Implémentation pour recevoir des dégâts sur terre
    }

    // Méthode spécifique pour mettre à jour le sprite des points de vie pour les unités terrestres
    public override void UpdateSpritePointsDeVie()
    {
        // Implémentation pour mettre à jour le sprite des points de vie sur terre
    }
}



//----------------------------------------------------------------------------------------------------------------------


// Classe abstraite pour les unités navales
public abstract class UniteNavale : Unite
{
    // Méthode spécifique pour se déplacer pour les unités navales
    public override void SeDeplacer(int distance)
    {
        // Implémentation pour se déplacer sur l'eau
    }

    // Méthode spécifique pour attaquer pour les unités navales
    public override void Attaquer(Unite uniteCible)
    {
        // Implémentation pour attaquer sur l'eau
    }

    // Méthode spécifique pour recevoir des dégâts pour les unités navales
    public override void RecevoirDegats(int degats)
    {
        // Implémentation pour recevoir des dégâts sur l'eau
    }

    // Méthode spécifique pour mettre à jour le sprite des points de vie pour les unités navales
    public override void UpdateSpritePointsDeVie()
    {
        // Implémentation pour mettre à jour le sprite des points de vie sur l'eau
    }
}
