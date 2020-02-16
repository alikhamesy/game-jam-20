using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IceCollider : MonoBehaviour
{
    private int lives = 5;
    
    void Update(){
        if(lives <= 0 || transform.childCount <= 0){
            SceneManager.LoadScene("Lose");
        }
    }
    
    public void bump(Vector2 velocity){
        StartCoroutine(bumpBears(velocity));
    }

    IEnumerator bumpBears(Vector2 velocity){
        Transform[] bears;
        for(int j = 0; j < 30; j++){
            bears = GetComponentsInChildren<Transform>();
            for(int i = 1; i < bears.Length; i++){
                bears[i].position += -(Vector3)velocity*0.05f*Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    void OnCollisionEnter2D(Collision2D col){
        switch(col.gameObject.tag){
            case "Land":
                SceneManager.LoadScene("Win");
                break;
            case "Boat":
                lives--;
                gameObject.transform.localScale *= 0.9f;
                break;
        }
    }
}
