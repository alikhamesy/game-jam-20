using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployShip : MonoBehaviour
{
    public GameObject BoatPrefab;
    public GameObject Player;
    public float radius = 20f;
    public float respawnTime = 5.0f;
    public float speed = 3f;
    private float[] xAxis;
    private float[] yAxis;


    public float x;
    public float y;
    private float playerX;
    private float playerY;
    private float flip;



    float check;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(asteroidWave());
    }

    private Vector2 randVelocity(float min, float max){
        float rand = Random.Range(min, max) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rand)*speed, Mathf.Sin(rand)*speed);
    }
    private void spawnEnemy()
    {
        GameObject a = Instantiate(BoatPrefab) as GameObject;
        playerX = Player.transform.position.x;
        playerY = Player.transform.position.y;
        xAxis = new float[]{playerX - radius, playerX + radius};
        yAxis = new float[]{playerY - radius, playerY + radius};
        
        check = Mathf.Round(Random.value);
        if (check == 1){
            x = Random.Range(playerX - radius, playerX + radius);
            y = yAxis[(int)(Mathf.Round(Random.value))];
            flip = y < playerY ? 1f : -1f;
            a.GetComponent<Rigidbody2D>().velocity = randVelocity(0, 180) * flip;
        }else{
            y = Random.Range(playerX - radius, playerX + radius);
            x = xAxis[(int)(Mathf.Round(Random.value))];
            flip = x > playerX ? 1f : -1f;
            a.GetComponent<Rigidbody2D>().velocity =  randVelocity(90, 270) * flip;
        }
        a.GetComponent<BoatMove>().Player = Player;
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
