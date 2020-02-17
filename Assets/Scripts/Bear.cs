using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public float action_delay;
    public float stopDelay;
    private float direction;
    private int speed;
    private float center;
    public bool fallen;
    public bool onWhale;
    public bool canRescue;
    public bool toCenter;
    public Animator animator;
    public ParticleSystem splash;
    private SpriteRenderer spriteRenderer;
    private Submerger submerger;

    public CapsuleCollider2D whaleCollider;
    private bool whaleUnderwater;

    public float fallDelay;
    //private SpriteRenderer bearRenderer;

    private CapsuleCollider2D bearCollider;
    private Rigidbody2D rigidBody;

    public PolygonCollider2D iceCollider;

    private AudioSource sound;

    void Start()
    {
        fallDelay = 5f;
        action_delay = 2;
        stopDelay = 0;
        direction = 0;
        speed = 2;
        Event_Manager.Distraction += bear_distracted;
        Event_Manager.Tilt += bear_sliding;
        Event_Manager.Underwater += get_whale_state;
        center = 1f;
        fallen = false;
        whaleUnderwater = false;
        onWhale = false;
        canRescue = false;
        toCenter = false;
        //bearRenderer = GetComponent<SpriteRenderer>();
        bearCollider = GetComponent<CapsuleCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        //iceCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        submerger = GetComponent<Submerger>();

        sound = GetComponent<AudioSource>();
        splash = GetComponent<ParticleSystem>();

    }

    public void OnDestroy()
    {
        Event_Manager.Distraction -= bear_distracted;
        Event_Manager.Tilt -= bear_sliding;
        Event_Manager.Underwater -= get_whale_state;
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

    void get_whale_state(bool whale_state)
    {
        whaleUnderwater = whale_state;
    }

    void Update()
    {

        float displacement = (Mathf.Pow(this.transform.localPosition.x, 2) / 7.5f + Mathf.Pow(this.transform.localPosition.y, 2) / 1.0f);
        action_delay -= Time.deltaTime;
        if (action_delay <= 0)
        {
            float theta = Mathf.Atan2(this.transform.localPosition.y, this.transform.localPosition.x) + Mathf.PI;
            if (float.IsNaN(theta) || displacement < center * center)
            {
                direction = Random.Range(0, 2 * Mathf.PI);
            }
            else
            {
                direction = Random.Range(theta - Mathf.PI / 2, theta + Mathf.PI / 2);
            }
            action_delay = Random.Range(1.5f, 3.0f);
            stopDelay = Random.Range(0.1f, 0.5f);

            speed = 2;

            // meh
            if (!fallen)
            {
                sound.Play();
            }
        }
        //print(bearCollider.Distance(iceCollider).distance);
        //print(bearCollider.Distance(whaleCollider).distance);

        // if on ice
        if (bearCollider.Distance(iceCollider).distance < 0)
        {
            fallen = false;
            fallDelay = 5f;
            rigidBody.velocity = iceCollider.attachedRigidbody.velocity;
            //this.transform.parent = iceCollider.transform.parent;
        }
        // if on whale
        else if (bearCollider.Distance(whaleCollider).distance < 0)
        {
            fallen = true;
            if (whaleUnderwater)
            {
                canRescue = true;
                onWhale = false;
                submerger.submergeTime = fallDelay;
                submerger.targetDepth = 0;
            }
            else if (!whaleUnderwater && canRescue)
            {
                onWhale = true;
                fallDelay = 5f;
                stopDelay = 0;
                rigidBody.velocity = whaleCollider.attachedRigidbody.velocity;

                submerger.submergeTime = 0.5f;
                submerger.targetDepth = 1.0f;
            }
            else
            {
                canRescue = false;
                onWhale = false;
                submerger.submergeTime = fallDelay;
                submerger.targetDepth = 0;
            }
        }
        else
        {
            fallen = true;
            submerger.submergeTime = fallDelay;
            submerger.targetDepth = 0;
            canRescue = false;
            onWhale = false;
            //this.transform.parent = null;
            stopDelay = 0;
        }

        if (whaleCollider.IsTouching(iceCollider) && onWhale)
        {
            toCenter = true;
            canRescue = false;
            onWhale = false;
            submerger.submergeTime = 0.5f;
            submerger.targetDepth = 1.0f;
        }

        if (fallen)
        {
            stopDelay = 0;
            //print(fallDelay);
            if (fallDelay == 5f)
            {
                splash.transform.position = transform.position;
                splash.Play();
            }
            //print("subtract");
            fallDelay -= Time.deltaTime;
            //print(fallDelay);

            if (fallDelay < 0)
            {
                Destroy(this.gameObject);
            }
        }
        else if (fallDelay < 5f)
        {
            fallDelay += 5 * Time.deltaTime;
            if (fallDelay > 5f)
            {
                fallDelay = 5f;
            }
        }/*
        else
        {
            fallDelay = 5f;
        }*/

        if (stopDelay > 0)
        {
            this.transform.Translate(new Vector3(speed * Time.deltaTime * Mathf.Cos(direction), speed * Time.deltaTime * Mathf.Sin(direction)));
            stopDelay -= Time.deltaTime;
            if (Mathf.Round(stopDelay * 10f) / 10f != 0 && Mathf.Cos(direction) != 0)
            {
                animator.SetFloat("Horizontal", Mathf.Cos(direction) < 0 ? -1 : 1);
                animator.SetFloat("Speed", 1f);
            }
            else
            {
                animator.SetFloat("Speed", 0);
                animator.SetFloat("Horizontal", 0);
            }
        }


        if (toCenter)
        {
            if (Mathf.Round(stopDelay * 10f) / 10f != 0 && Mathf.Cos(direction) != 0)
            {
                animator.SetFloat("Horizontal", Mathf.Cos(direction) < 0 ? -1 : 1);
                animator.SetFloat("Speed", 1f);
            }
            else
            {
                Debug.Log("test");
                animator.SetFloat("Speed", 0);
                animator.SetFloat("Horizontal", 0);
            }
            stopDelay = 0f;
            action_delay = 2f;
            submerger.submergeTime = 0.5f;
            submerger.targetDepth = 1.0f;
            if (displacement < center * center)
            {
                toCenter = false;
            }
            this.transform.Translate(speed * Time.deltaTime * (this.transform.parent.position - this.transform.position));
        }
        //bearRenderer.color = new Color(fallDelay / 5f, fallDelay / 5f, fallDelay / 5f, fallDelay / 5f);
        splash.transform.position = transform.position;
        splash.Play();
    }
}