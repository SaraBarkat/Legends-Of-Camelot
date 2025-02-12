using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBuilding : Building
    {
        public GenericBuilding(int x, int y) : base(new Vector2(x, y), "Generic")
        {
        }
    }