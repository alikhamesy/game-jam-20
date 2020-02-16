using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    private CapsuleCollider2D bearCollider;
    public float drownTime;
    public CapsuleCollider2D whaleCollider;
    public PolygonCollider2D iceCollider;
    private Submerger submerger;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        bearCollider = GetComponent<CapsuleCollider2D>();
        submerger = GetComponent<Submerger>();
    }

    void Update()
    {
        if (bearCollider.IsTouching(iceCollider))
        {
            // if on ice 🍦
            // willSplash = true;
            rigidBody.velocity = iceCollider.attachedRigidbody.velocity;
        }
        else if (bearCollider.IsTouching(whaleCollider))
        {
            // if on whale 🐳

            rigidBody.velocity = whaleCollider.attachedRigidbody.velocity;

            submerger.submergeTime = 0.5f;
            submerger.targetDepth = 1.0f;

            if (bearCollider.Distance(iceCollider).distance < 0.1f)
            {
                rigidBody.velocity += bearCollider.Distance(iceCollider).normal;
            }
        }
        else
        {
            // if in water 💦

            submerger.submergeTime = drownTime;
            submerger.targetDepth = 0;

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
