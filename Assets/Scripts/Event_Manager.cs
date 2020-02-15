using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Manager : MonoBehaviour
{

    public delegate void BearHandler(float x, float y);
    public static event BearHandler Distraction;
    private float timer;
    private bool once;

    void add_distraction()
    {
        print("Get distracted");
        if (Distraction != null) { Distraction(0,10); }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 1.0f;
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !once) { add_distraction(); once = true; }
    }
}
