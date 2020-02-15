using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;            

    public Rigidbody2D rb2d;


    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
      
        float moveHorizontal = Input.GetAxis("Horizontal");

       
        float moveVertical = Input.GetAxis("Vertical");

       
       // Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        
        rb2d.velocity=new Vector2(moveHorizontal * speed, moveVertical * speed);
        //rb2d.AddForce(movement * speed);
    }
}
