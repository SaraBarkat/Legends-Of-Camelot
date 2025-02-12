using MonBatiment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : Batiment
{
   private int income=1000;
    public int getincome(){
    return income;
   }

   public override void Capture(Unit unite)
        {
            // Logique de capture spécifique au village
            base.Capture(unite); // Appelez la méthode Capture de la classe de base si nécessaire
        }
}
