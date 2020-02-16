using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BoatMove : MonoBehaviour
{
    float speed=3;
    private Vector2 screenBounds;
    float x;
    float y;
    float[] xAxis;
    float[] yAxis;

    float check;
   


    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        xAxis = new float[] { screenBounds.x, -screenBounds.x };
        yAxis = new float[] { screenBounds.y, -screenBounds.y };

        check = Mathf.Round(Random.value);
        Debug.Log("check " + check);

        if (check== 1){
            x = Random.Range(-screenBounds.x, screenBounds.x);
            Debug.Log("x was chosen its "+x);
        }
        else{
            y = Random.Range(-screenBounds.y, screenBounds.y);
            Debug.Log("y was chosen its " + y);
        }

        if (check == 1)
        {
            y = yAxis[(int)((Mathf.Round(Random.value)))];
        }
        else
        {
            x = xAxis[(int)((Mathf.Round(Random.value)))];
        }


       
        

    }
 private void Update()
    {

        float step =  speed * Time.deltaTime;


        transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(x,y,0f), step);

        

    }
}
