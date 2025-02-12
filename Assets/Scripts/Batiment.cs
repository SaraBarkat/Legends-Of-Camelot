using UnityEngine;
using System.Collections;

namespace MonBatiment {
    public class Batiment : MonoBehaviour
{
     protected int maxlife = 20;
    public int currlife=0;
 
    private Joueur  player;
    public Sprite redSprite;
    public Sprite blueSprite;
    public PlayerColor col;
    public PlayerColor currcol;
    private SpriteRenderer spriteRenderer;

    private Vector3Int initialPosition;
    public Animator villageAnimator;

    void Start()
    {
            spriteRenderer = GetComponent<SpriteRenderer>();
            initialPosition = new Vector3Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z));
    }

    public void PlayCaptureAnimation()
    {
        if (villageAnimator != null)
        {
            villageAnimator.SetTrigger("capture");

        }
        else
        {
            Debug.LogError("Référence à l'Animator du village manquante !");
        }
    }

public Vector3Int getposition(){
    return initialPosition;
}



    public virtual void Capture(Unit unite)
    {
        if (col != unite.col ||currcol!=unite.col ){
        PlayCaptureAnimation();
        if (unite.col != currcol)
        {
            currcol = unite.col;
            currlife = unite.currentHP;
        }
        else
        {
          
        
            currlife +=unite.currentHP ;
            if(currlife>=maxlife) currlife=maxlife;
    
            Debug.Log("currlife = " + currlife);
            if (currlife >= maxlife)
            {
                if (unite.col == PlayerColor.ROUGE)
                {
                    spriteRenderer.sprite = redSprite;
                    col=PlayerColor.ROUGE;
                }
                else if (unite.col == PlayerColor.BLEU)
                {
                    spriteRenderer.sprite = blueSprite;
                     col=PlayerColor.BLEU;
                }
            }
        }
        }

      
    }

    public PlayerColor getCurrColor() {
        return col;
    }
  

}


}
