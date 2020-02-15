using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    // Variable declarations
    private float x;
    private float y;
    private float action_delay;
    private float stop_delay;
    private float direction;
    private int speed;
    private bool fallen;

    // Start is called before the first frame update
    void Start()
    {
        action_delay = 2;
        stop_delay = 0;
        direction = 0;
        speed = 1;
        fallen = false;
        Event_Manager.Distraction += bear_distracted;
    }

    void bear_distracted(float x, float y)
    {
        print("distracted");
        action_delay = 2.0f;
        stop_delay = 2.0f;
        direction = Mathf.Atan2(this.transform.position.y - y, this.transform.position.x - x) + Mathf.PI;
    }

    // Update is called once per frame
    void Update()
    {
        action_delay -= Time.deltaTime;
        if (action_delay <= 0)
        {
            float theta = Mathf.Atan2(this.transform.position.y, this.transform.position.x) + Mathf.PI;
            if (float.IsNaN(theta)) {
                direction = Random.Range(0, 2 * Mathf.PI);
            }
            else
            {
                direction = Random.Range(theta - Mathf.PI / 4, theta + Mathf.PI / 4 );
            }
            action_delay = Random.Range(1.5f, 3.0f);
            stop_delay = Random.Range(0.1f, 0.5f);
        }


        if ((Mathf.Pow(this.transform.position.x, 2) + Mathf.Pow(this.transform.position.y, 2)) > 4 * 4)
        {
            stop_delay = 0;
            this.transform.Rotate(new Vector3(0, 0, 0.5f));
            fallen = true;
        }

        if (stop_delay > 0)
        {
            this.transform.Translate(new Vector3(speed * Time.deltaTime * Mathf.Cos(direction), speed * Time.deltaTime * Mathf.Sin(direction)));
            stop_delay -= Time.deltaTime;

        }
    }
}