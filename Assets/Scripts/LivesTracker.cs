using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesTracker : MonoBehaviour
{
    public Sprite[] lifeSprites;
    public void decrease(int i) {
        if(i > 1){
            Debug.Log(i);
            GetComponent<SpriteRenderer>().sprite = lifeSprites[i-2];
        }
    }
}
