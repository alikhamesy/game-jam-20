using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BoatMove : MonoBehaviour
{
    float speed=3;
    public float[] xAxis;
    public float[] yAxis;
    float x;
    float y;
    float check;

    void Start()
    {
        xAxis = new float[]{-100f, 100f};
        yAxis = new float[]{-100f, 100f};

        check = Mathf.Round(Random.value);

        if (check == 1){
            x = Random.Range(-100f, 100f);
            y = yAxis[(int)((Mathf.Round(Random.value)))];
            Debug.Log("x was chosen its "+x);
        }
        else{
            y = Random.Range(-100f, 100f);
            x = xAxis[(int)((Mathf.Round(Random.value)))];
            Debug.Log("y was chosen its " + y);
        }
    }
    private void Update()
    {
        float step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(x,y,0f), step);
        if(Mathf.Abs(transform.position.x) > 100f || Mathf.Abs(transform.position.y) > 100f){
            Object.Destroy(gameObject);
        }
    }
}
