﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BoatMove : MonoBehaviour
{
    public float speed=3;
    public GameObject Player;
    public float radius = 20f;
    public float[] xAxis;
    public float[] yAxis;
    float x;
    float y;
    float check;

    void Start(){
    }
    private void Update()
    {
        if(Mathf.Abs(transform.position.x - Player.transform.position.x) > radius+2 
        || Mathf.Abs(transform.position.y - Player.transform.position.y) > radius+2){
            Object.Destroy(gameObject);
        }
    }
}
