using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Chateau :  PropertyManager 
    {
        public static readonly int Offre = 0;
        public static readonly string Name = "QG";
        public static readonly int pvn = 20; 

        public Chateau (Joueur p, Vector2 pos) : base(p, pos, pvn, Offre, Name)
        {
        }

        public override void Setproprietaire (Joueur p) // cas de capture 
        {
            if (proprietaire != null)
            { 
               // proprietaire.Loose();
            }
           // Universe.Get().SetBuilding((int)GetPosition().x, (int)GetPosition().y, new City(p, new Vector2((int)GetPosition().x, (int)GetPosition().y)));
        }

       
    }


