using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public Camera myCamera;
    public float xBounds = 8f;
    public float yBounds = 4f;

    private Rigidbody2D cameraRigidBody;
    private Rigidbody2D playerRigidBody;
    private float xDif;
    private float yDif;
    // Start is called before the first frame update
    void Start()
    {
        cameraRigidBody = myCamera.GetComponent<Rigidbody2D>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xDif = transform.position.x - myCamera.transform.position.x;
        yDif = transform.position.y - myCamera.transform.position.y;
        if ((Mathf.Abs(xDif) > xBounds && ((xDif > 0 && playerRigidBody.velocity.x > 0) || (xDif < 0 && playerRigidBody.velocity.x < 0)))
          || (Mathf.Abs(yDif) > yBounds && ((yDif > 0 && playerRigidBody.velocity.y > 0) || (yDif < 0 && playerRigidBody.velocity.y < 0))))
        {
            cameraRigidBody.velocity = playerRigidBody.velocity;
        }
        else
        {
            cameraRigidBody.velocity = new Vector2(0f, 0f);
        }
    }
}
