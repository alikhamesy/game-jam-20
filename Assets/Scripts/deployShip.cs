using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployShip : MonoBehaviour
{
    public GameObject BoatPrefab;
    public float respawnTime = 5.0f;
    private Vector2 screenBounds;

    public float x;
    public float y;

    float[] xAxis;
    float[] yAxis;


    float check;

    // Use this for initialization
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));




        StartCoroutine(asteroidWave());
    }
    private void spawnEnemy()
    {
        GameObject a = Instantiate(BoatPrefab) as GameObject;

        xAxis = new float[] { screenBounds.x, -screenBounds.x };
        yAxis = new float[] { screenBounds.y, -screenBounds.y };

        check = Mathf.Round(Random.value);
        Debug.Log("check " + check);

        if (check == 1)
        {
            x = Random.Range(-screenBounds.x, screenBounds.x);
            Debug.Log("x was chosen its " + x);
        }
        else
        {
            y = Random.Range(-screenBounds.y, screenBounds.y);
            Debug.Log("y was chosen its " + y);
        }

        if (check == 1)
        {
            y = yAxis[(int)((Mathf.Round(Random.value)))];
        }
        else
        {
            x = xAxis[(int)((Mathf.Round(Random.value)))];
        }

        a.transform.position = new Vector2(x,y);
    }
    IEnumerator asteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}
