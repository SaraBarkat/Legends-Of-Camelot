/*using System;
using System.Collections.Generic;
using UnityEngine;

public class Caserne : PropertyManager, IFactoryBuilding
{
    public static readonly int Offre = 50;
    public static readonly string Name = "Caserne";
    public static readonly int pvn = 10;

    private static Dictionary<TypeUnite, Func<Joueur, Vector2, Unite>> UNIT_LIST;

    static Caserne()
    {
        UNIT_LIST = new Dictionary<TypeUnite, Func<Joueur, Vector2, Unite>>()
        {
           // { typeof(Infanterie), (joueur, point) => new Infanterie(joueur, point) },
            { typeof(Archer), (joueur, point) => new Archer(joueur, point) },
            { typeof(Chevalier), (joueur, point) => new Chevalier(joueur, point) },
            { typeof(Cavalier), (joueur, point) => new Cavalier(joueur, point) },
            { typeof(Catapulte), (joueur, point) => new Catapulte(joueur, point) },
            { typeof(Ballista), (joueur, point) => new Ballista(joueur, point) },
            { typeof(NavireTransport), (joueur, point) => new NavireTransport(joueur, point) },
            { typeof(NavireIncendie), (joueur, point) => new NavireIncendie(joueur, point) },
        };
    }

    public Caserne(Joueur joueur, Vector2 position) : base(joueur, position, pvn, Offre, Name)
    {
    }

    public HashSet<TypeUnite> GetUnitList() 
    {
        return new HashSet<TypeUnite>(Caserne.GetUnits());
    }

    public static HashSet<TypeUnite> GetUnits()
    {
        return new HashSet<TypeUnite>(UNIT_LIST.Keys);
    }

    public bool Create(TypeUnite unitType)
    {
        try
        {
            if (UNIT_LIST.TryGetValue(unitType, out Func<Joueur, Vector2, Unite> constructor) &&
                Universe.Instance.GetUnit((int)Position.x, (int)Position.y) == null &&
                Proprietaire.Spent((int)unitType.GetField("PRICE").GetValue(null)))
            {
                constructor.Invoke(Proprietaire, Position).MoveQuantity = 0;
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return false;
    }
}*/
