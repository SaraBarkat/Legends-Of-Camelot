using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PropertyManager : Building
{
    private int PVA;
    public readonly int PVN;
    public readonly int offre;
    protected Joueur proprietaire;

    public PropertyManager(Joueur p, Vector2 pos, int PVN, int offre, string name) : base(pos, name)
    {
        this.PVN = PVN; // points de vie actuels du batiment
        this.PVA = PVN; // pv maximum ( nécessaire pour la capture )
        this.offre = offre; // nbr de pièdes d'or 
        this.proprietaire = p;

    }

    public override string ToString()
    {
        return name;
    }

    public Joueur Getproprietaire()
    {
        return proprietaire;
    }

    public virtual void  Setproprietaire(Joueur p) 
    {
        proprietaire = p;
        
    }

    public bool IsNeutral()
    {
        return proprietaire == null;
    }

    public int GetPVA()
    {
        return PVA;
    }

    public int Getoffre() // cas du coffre trésor 
    {
        return offre;
    }

    public bool RemoveLife(Joueur player, int damage ) // 'damage' est le domage causé lors d'une capture
    {
        if (this.PVA <= damage )
        {
            Setproprietaire(player);
            GoBackToPVN();
            return true; // si le batiment est capturé
        }
        else
        {
            this.PVA -= damage;
            return false; // sinon
        }
    }

    public bool RemoveLife(Unite u)
    {
        return RemoveLife(u.GetPlayer(), u.GetLife());
    }

    public void GoBackToPVN()
    {
        PVA = PVN;
    }
}
