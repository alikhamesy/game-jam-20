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
    private float center;
    private float fallen;

    // Start is called before the first frame update
    void Start()
    {
        action_delay = 2;
        stop_delay = 0;
        direction = 0;
        speed = 2;
        fallen = 10f;
        Event_Manager.Distraction += bear_distracted;
        Event_Manager.Tilt += bear_sliding;
        center = 0.75f;
    }

    public void OnDestroy()
    {
        Event_Manager.Distraction -= bear_distracted;
        Event_Manager.Tilt -= bear_sliding;
    }

    void bear_distracted(float x, float y)
    {
        //print("distracted");
        action_delay = 2.5f;
        stop_delay = 1.0f;
        speed = 1;
        direction = Mathf.Atan2(this.transform.position.y - y, this.transform.position.x - x) + Mathf.PI;
    }

    void bear_sliding(float x, float y)
    {
        //action_delay = 1.0f;
        //stop_delay = 0;
        if (fallen!=10f) { return; }
        float slide_dir = Mathf.Atan2(this.transform.position.y - y, this.transform.position.x - x) + Mathf.PI;
        this.transform.Translate(new Vector3(speed/4f * Time.deltaTime * Mathf.Cos(slide_dir), speed/2f * Time.deltaTime * Mathf.Sin(slide_dir)));
        //this.transform.Translate(new Vector3(x * Time.deltaTime, y * Time.deltaTime));
    }

    // Update is called once per frame
    void Update()
    {
        float displacement = (Mathf.Pow(this.transform.position.x, 2) + Mathf.Pow(this.transform.position.y, 2));
        action_delay -= Time.deltaTime;
        if (action_delay <= 0)
        {
            float theta = Mathf.Atan2(this.transform.position.y, this.transform.position.x) + Mathf.PI;
            if (float.IsNaN(theta) || displacement < center*center) {
                direction = Random.Range(0, 2 * Mathf.PI);
            }
            else
            {
                direction = Random.Range(theta - Mathf.PI / 2, theta + Mathf.PI / 2 );
            }
            action_delay = Random.Range(1.5f, 3.0f);
            stop_delay = Random.Range(0.1f, 0.5f);

            speed = 2;
        }


        if (displacement > 2 * 2)
        {
            stop_delay = 0;
            this.transform.Rotate(new Vector3(0, 0, 0.5f));
            fallen -= Time.deltaTime;
        }
        else
        {
            fallen = 10f;
        }

        if (fallen < 0)
        {
            Destroy(this.gameObject);
        }

        if (stop_delay > 0)
        {
            this.transform.Translate(new Vector3(speed * Time.deltaTime * Mathf.Cos(direction), speed * Time.deltaTime * Mathf.Sin(direction)));
            stop_delay -= Time.deltaTime;

        }
    }
}