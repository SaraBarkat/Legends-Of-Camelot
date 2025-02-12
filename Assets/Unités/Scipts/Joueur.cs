using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class Joueur : MonoBehaviour
{
    public string nom;
    public int ressources;
    public string couleur;
    public string getNom() {
        return nom;
    }

    public int getRessources() {
        return ressources;
    }

    public string getCouleur() {
        return couleur;
    }

    public void setNom(string nom) {
        this.nom=nom;
    }

    public void setRessources(int res) {
        ressources=res;
    }

    public void setCor(string couleur) {
        this.couleur = couleur;
    }
/*



     public class QuartierGeneral {
        public int PointsDeVie  ;

     }

     
    public string Nom { get; set; }
    public string Couleur { get; set; }   /// ArmeType
    /// 

    public List<Unite> Unites { get; set; }
    public QuartierGeneral QG { get; set; } // 
     //public int PointsDeCommandement { get; set; } // Points d'action pour le tour
    public int Ressources { get; set; } // Ressources pour la construction d'unités

    public Joueur(string nom, string couleur) //Le constructeur 
    {
        Nom = nom;
        Couleur = couleur;
        Unites = new List<Unite>();
        QG = null; // Initialiser le QG à null
        //PointsDeCommandement = 10; // Valeur par défaut
        Ressources = 500; // Exmple de Valeur par défaut
    }

    public bool ConstruireUnite(Unite unite, int x, int y)
    {
        // Vérifier les ressources //et les points de commandement
        if (Ressources < unite.Cout )
        {
            return false; // Ressources ou points insuffisants
        }

        // Créer une nouvelle unité du type spécifié
        Unite nouvelleUnite = new Unite(unite, x, y);

        // Déduire le coût de construction
        Ressources -= unite.cout;


        // Ajouter l'unité à la liste des unités du joueur
        Unites.Add(nouvelleUnite);

        return true; // Unité construite avec succès
    }

    public void DeplacerUnite(Unite unite, int x, int y)
    {
        // Valider les points de mouvement et la destination
        if (unite.PointsDeMouvement < GetDistance(unite.X, unite.Y, x, y) || !CaseEstAccessible(x, y))
        {
            return; // Déplacement impossible
        }

        // Déplacer l'unité
        unite.Deplacer(x, y);

        // Déduire les points de mouvement utilisés
        unite.PointsDeMouvement -= GetDistance(unite.X, unite.Y, x, y);

        // L'unité ne peut plus attaquer après un déplacement
        unite.PeutAttaquer = false;
    }

    public void AttaquerUnite(Unite uniteAttaquante, Unite uniteCible)
    {
        // Vérifier si l'unité peut attaquer
        if (!uniteAttaquante.PeutAttaquer)
        {
            return; // Unité a déjà attaqué
        }

        // Calculer les dégâts infligés
        int degats = uniteAttaquante.Attaque - uniteCible.Defense;
        if (degats < 0)
        {
            degats = 0; // Dégâts minimums de 0
        }

        // Appliquer les dégâts à l'unité cible
        uniteCible.SubirAttaque(degats);

        // L'unité a attaqué
        uniteAttaquante.PeutAttaquer = false;

        // Si l'unité cible est détruite, la supprimer
        if (uniteCible.PointsDeVie <= 0)
        {
            Unites.Remove(uniteCible);
        }
    }

    public void FinTour()
    {
        // Réinitialiser les points de mouvement et d'attaque des unités
        foreach (Unite unite in Unites)
        {
            unite.PointsDeMouvement = unite.TypeUnite.PointsDeMouvement;
            unite.PeutAttaquer = true;
        }

        // Gagner des points de commandement
       // PointsDeCommandement = Math.Min(PointsDeCommandement + 2, 10); // Limite à 10 points

        // Recharger les ressources
        Ressources += 50; // Gain de ressources par tour
    }

    public bool EstVaincu()
    {
        // Déterminer si le joueur est vaincu (QG détruit ou toutes les unités détruites)
        if (QG == null || QG.PointsDeVie <= 0)
        {
            return true; // QG détruit
        }

        if (Unites.Count == 0)
        {
            return true; // Aucune unité restante
        }

        return false; // Le joueur
    }
   
   
   
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
//}
