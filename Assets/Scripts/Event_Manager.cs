using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Manager : MonoBehaviour
{

    public delegate void BearHandler(float x, float y);
    public static event BearHandler Distraction;
    private float timer;

    void add_distraction()
    {
        print("Get distracted");
        if (Distraction != null) {
            float x = Random.Range(0, 10);
            float y = Random.Range(0, 10);
            Distraction(x, y);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 ) { add_distraction(); timer = 30.0f; }
    }
}
