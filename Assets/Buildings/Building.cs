using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : IAbsBuilding
    {
        protected Vector2 location;
        public readonly string name;

        public Building(Vector2 p, string name)
        {
            this.location = p;
            this.name = name;
           
        }

        public Vector2 GetLocation()
       {
            return new Vector2(location.x, location.y);
       }

        public int GetX()
        {
            return (int)location.x;
        }

        public int GetY()
        {
            return (int)location.y;
        }

        public void SetLocation(Vector2 l)
        {
            this.location = l;
        }

        public string GetName()
        {
            return name;
        }

    }
