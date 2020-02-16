using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Manager : MonoBehaviour
{

    public delegate void BearHandler(float x, float y);
    public static event BearHandler Distraction;
    public static event BearHandler Tilt;
    private float timer;
    private float end_timer;

    public delegate void WhaleHandler(bool whale_state);
    public static event WhaleHandler Underwater;

    //public delegate void HoldingHandler(GameObject bear, bool holding);
    public GameObject held_bear;
    //public static event HoldingHandler HasBear;

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

    public bool get_holding_bear(GameObject bear){
        return (held_bear == bear);
    }

    public bool change_holding_bear(GameObject bear, bool holding){
        if ( holding ){
            if ( held_bear == null ){
                held_bear = bear;
            }
            if( held_bear == bear ){
                return true;
            }
            else{
                return false;
            }
        }
        else{
            if( held_bear == bear) {
                held_bear = null;
            }
            return false;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 30.0f;
        end_timer = 6f*5f;
        held_bear = null;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        end_timer -= Time.deltaTime;
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
        
        if (end_timer < 0f){
            //print("ggs");
        }
    }
}
