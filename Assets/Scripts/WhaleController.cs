using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public bool isUnderwater = false;

    private float visibility = 1.0f;
    private float minVisibility = 0.3f;

    private bool isRight = true;

    private void setVisibilityFromTarget(float target)
    {
        if (visibility < target)
        {
            visibility += Mathf.Min(2 * Time.deltaTime, target - visibility);
        }
        else if (target < visibility)
        {
            visibility -= Mathf.Min(2 * Time.deltaTime, visibility - target);
        }
    }



    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            isUnderwater = true;
        }
        else
        {
            isUnderwater = false;
        }

        if (isUnderwater)
        {
            setVisibilityFromTarget(minVisibility);
            transform.Translate(Vector3.down * Time.deltaTime * (visibility - minVisibility) * 10.0f);
        }
        else
        {
            setVisibilityFromTarget(1.0f);
            transform.Translate(Vector3.up * Time.deltaTime * (1.0f - visibility) * 10.0f);
        }
        spriteRenderer.color = new Color(visibility, visibility, visibility, visibility);

        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        // spriteRenderer.sprite.uv = Vector2.dow


        // Vector2 movement = new Vector2(moveHorizontal, moveVertical);


        rigidBody.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
        //rb2d.AddForce(movement * speed);

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
