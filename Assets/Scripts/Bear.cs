using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    private float action_delay;
    public float stopDelay;
    private float direction;
    private int speed;
    private float center;
    public bool fallen;

    public CircleCollider2D whaleCollider;

    public float fallDelay = 15.0f;
    private SpriteRenderer bearRenderer;

    private CircleCollider2D bearCollider;
    private PolygonCollider2D iceCollider;

    void Start()
    {
        action_delay = 2;
        stopDelay = 0;
        direction = 0;
        speed = 2;
        Event_Manager.Distraction += bear_distracted;
        Event_Manager.Tilt += bear_sliding;
        center = 0.75f;
        fallen = false;
        bearRenderer = GetComponent<SpriteRenderer>();

        bearCollider = GetComponent<CircleCollider2D>();
        iceCollider = GetComponentInParent<PolygonCollider2D>();
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
        stopDelay = 1.0f;
        speed = 1;
        direction = Mathf.Atan2(this.transform.position.y - y, this.transform.position.x - x) + Mathf.PI;
    }

    void bear_sliding(float x, float y)
    {
        //action_delay = 1.0f;
        //stop_delay = 0;
        if (fallen) { return; }
        float slide_dir = Mathf.Atan2(this.transform.position.y - y, this.transform.position.x - x) + Mathf.PI;
        this.transform.Translate(new Vector3(speed / 4f * Time.deltaTime * Mathf.Cos(slide_dir), speed / 2f * Time.deltaTime * Mathf.Sin(slide_dir)));
        //this.transform.Translate(new Vector3(x * Time.deltaTime, y * Time.deltaTime));
    }

    void Update()
    {
        float displacement = (Mathf.Pow(this.transform.localPosition.x - 0.06f, 2)/7.5f + Mathf.Pow(this.transform.localPosition.y-10.0f, 2)/1.0f);
        action_delay -= Time.deltaTime;
        if (action_delay <= 0)
        {
            float theta = Mathf.Atan2(this.transform.localPosition.y, this.transform.localPosition.x) + Mathf.PI;
            if (float.IsNaN(theta) || displacement < center*center) {
                direction = Random.Range(0, 2 * Mathf.PI);
            }
            else
            {
                direction = Random.Range(theta - Mathf.PI / 2, theta + Mathf.PI / 2);
            }
            action_delay = Random.Range(1.5f, 3.0f);
            stopDelay = Random.Range(0.1f, 0.5f);

            speed = 2;
        }

        // if on ice
        if (bearCollider.Distance(iceCollider).distance < 0)
        {
            fallen = false;
            fallDelay = 10f;
        }
        else
        {
            fallen = true;
            this.transform.parent = null;
            stopDelay = 0;
            this.transform.Rotate(new Vector3(0, 0, 0.5f));
        }

        // if on whale
        if (bearCollider.Distance(whaleCollider).distance < 0)
        {
            fallen = false;
            fallDelay = 10;
            stopDelay = 0;
        }

        if (fallen)
        {
            fallDelay -= Time.deltaTime;
        }
        else if (fallDelay < 10f)
        {
            fallDelay += 5 * Time.deltaTime;
            if (fallDelay > 10f)
            {
                fallDelay = 10f;
            }
        }
        else
        {
            fallDelay = 15;
        }

        if (fallDelay < 0)
        {
            Destroy(this.gameObject);
        }

        if (stopDelay > 0)
        {
            this.transform.Translate(new Vector3(speed * Time.deltaTime * Mathf.Cos(direction), speed * Time.deltaTime * Mathf.Sin(direction)));
            stopDelay -= Time.deltaTime;
        }

        bearRenderer.color = new Color(fallDelay / 15, fallDelay / 15, fallDelay / 15, fallDelay / 15);
    }
}