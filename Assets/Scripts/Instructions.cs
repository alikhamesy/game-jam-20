using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject map;
    private bool s = false;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            s = true;
            map.GetComponent<Canvas>().enabled = true;
        }
        if(s){
            color.a -= 1.5f * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = color;
            if(color.a <= 0f) Destroy(gameObject);
        }
    }
}
