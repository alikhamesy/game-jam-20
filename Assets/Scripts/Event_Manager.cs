using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Manager : MonoBehaviour
{

    public delegate void BearHandler(float x, float y);
    public static event BearHandler Distraction;
    public static event BearHandler Tilt;
    private float timer;

    public delegate void WhaleHandler(bool whale_state);
    public static event WhaleHandler Underwater;

    void add_distraction(float x, float y)
    {
        //print("Get distracted");
        if (Distraction != null) {
            Distraction(x, y);
        }
    }

    public void change_whale_state(bool whale_state)
    {
        if (Underwater != null){
            Underwater(whale_state);
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
        if (timer <= 0 ) { 
            float x = Random.Range(0, 10);
            float y = Random.Range(0, 10);
            add_distraction(x,y); 
            timer = 10.0f; }

        if (Input.GetMouseButton(0))
        {
            var world_coord = Input.mousePosition;
            world_coord.z = 10;
            world_coord = Camera.main.ScreenToWorldPoint(world_coord);
            Tilt(world_coord.x, world_coord.y);
        }
    }
}
