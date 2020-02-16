using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployShip : MonoBehaviour
{
    public GameObject BoatPrefab;
    public float respawnTime = 5.0f;
    private float[] xAxis;
    private float[] yAxis;

    public float x;
    public float y;



    float check;

    // Use this for initialization
    void Start()
    {
        xAxis = new float[]{-100f, 100f};
        yAxis = new float[]{-100f, 100f};
        StartCoroutine(asteroidWave());
    }
    private void spawnEnemy()
    {
        GameObject a = Instantiate(BoatPrefab) as GameObject;

        check = Mathf.Round(Random.value);
        Debug.Log("check " + check);

        if (check == 1)
        {
            x = Random.Range(-100f, 100f);
            y = yAxis[(int)(Mathf.Round(Random.value))];
            Debug.Log("x was chosen its " + x);
        }
        else
        {
            y = Random.Range(-100f, 100f);
            x = xAxis[(int)((Mathf.Round(Random.value)))];
            Debug.Log("y was chosen its " + y);
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
