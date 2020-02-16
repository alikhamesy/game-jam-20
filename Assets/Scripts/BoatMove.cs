using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BoatMove : MonoBehaviour
{
    public GameObject Player;
    public float radius = 20f;

    void Start(){
    }
    private void Update()
    {
        if(Mathf.Abs(transform.position.x - Player.transform.position.x) > radius+2 
        || Mathf.Abs(transform.position.x) > 99 
        || Mathf.Abs(transform.position.y - Player.transform.position.y) > radius+2
        || Mathf.Abs(transform.position.y) > 99){
            Object.Destroy(gameObject);
        }
    }
}
