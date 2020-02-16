using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    public bool isUnderwater = false;
    private float maxSize;
    private float minSize;


    private float visibility = 1.0f;
    private float minVisibility = 0.3f;
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
        circleCollider = GetComponent<CircleCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxSize = transform.localScale.x;
        minSize = maxSize * 0.9f;
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
            transform.Translate(Vector3.down * Time.deltaTime * (visibility - minVisibility) * 3.0f);

            float size = Mathf.Max(minSize, transform.localScale.x * (1 - Time.deltaTime));
            transform.localScale = new Vector3(size, size, size);

            circleCollider.enabled = false;
        }
        else
        {
            setVisibilityFromTarget(1.0f);
            transform.Translate(Vector3.up * Time.deltaTime * (1.0f - visibility) * 3.0f);

            float size = Mathf.Min(maxSize, transform.localScale.x * (1 + Time.deltaTime));
            transform.localScale = new Vector3(size, size, size);

            circleCollider.enabled = true;

        }

        spriteRenderer.color = new Color(visibility, visibility, visibility, visibility);


        // movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rigidBody.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);

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
