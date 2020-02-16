using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D whaleCollider;
    private Submerger submerger;

    public bool isUnderwater = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        whaleCollider = GetComponent<CapsuleCollider2D>();
        submerger = GetComponent<Submerger>();
    }

    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            isUnderwater = true;
            whaleCollider.enabled = false;
            submerger.targetDepth = 0.3f;
        }
        else
        {
            isUnderwater = false;
            whaleCollider.enabled = true;
            submerger.targetDepth = 1.0f;
        }

        // movement
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        rigidBody.velocity = new Vector2(xMovement * speed, yMovement * speed);

        if (rigidBody.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (0 < rigidBody.velocity.x)
        {
            spriteRenderer.flipX = false;
        }
    }
}
