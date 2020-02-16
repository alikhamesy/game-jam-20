using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{

    private CapsuleCollider2D bearCollider;
    public float drownTime;
    public CapsuleCollider2D whaleCollider;
    public PolygonCollider2D iceCollider;

    // public ParticleSystem splash;
    // private bool willSplash = true;


    private Vector2 getRandomNormalVector()
    {
        Vector2 v = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        v.Normalize();
        return v;
    }

    void Start()
    {
        bearCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (bearCollider.IsTouching(iceCollider))
        {
            // if on ice 🍦
            // willSplash = true;
        }
        else if (bearCollider.IsTouching(whaleCollider))
        {
            // if on whale 🐳



            if (bearCollider.Distance(iceCollider).distance < 0.1f)
            {
                //rigidBody.velocity += bearCollider.Distance(iceCollider).normal;
            }
        }
        else
        {
            // if in water 💦


            // if (willSplash)
            // {
            //     splash.transform.position = transform.position;
            //     splash.Stop();

            //     splash.Play();
            //     willSplash = false;
            // }
        }
    }
}
