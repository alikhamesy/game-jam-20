using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollider : MonoBehaviour
{
    private Vector2 velocity;
    void Start() {
        velocity = GetComponent<Rigidbody2D>().velocity;
    }
    void OnCollisionEnter2D(Collision2D col){
        switch(col.gameObject.tag){
            case "Bear":
                Destroy(col.gameObject);
                Destroy(gameObject);
                break;
            case "Boat":
                Destroy(col.gameObject);
                break;
            case "Ice":
                col.gameObject.GetComponent<IceCollider>().bump(velocity);
                Destroy(gameObject);
                break;
            case "Player":
                Destroy(gameObject);
                break;
        }
    }

}
