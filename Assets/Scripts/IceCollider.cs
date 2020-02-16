using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCollider : MonoBehaviour
{

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
        if(col.gameObject.tag == "Land"){
            //win
        }
    }
}
