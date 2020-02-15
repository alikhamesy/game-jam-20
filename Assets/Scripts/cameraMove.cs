using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public GameObject camera;
    public float xbounds = 8f;
    public float ybounds = 6f;
    private Rigidbody2D _rb;
    private Transform _t;
    private Rigidbody2D _playerRB;
    private float _xdif;
    private float _ydif;    
    private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _t = GetComponent<Transform>();
        _rb = camera.GetComponent<Rigidbody2D>();
        _speed = GetComponent<move>().speed;
        _playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _xdif = camera.transform.position.x - _t.position.x;
        _ydif = camera.transform.position.y - _t.position.y;
        if((Mathf.Abs(_xdif) > xbounds && _playerRB.velocity.x > 0) || Mathf.Abs(_ydif) > ybounds){
            _rb.velocity = _playerRB.velocity;
        }else{
            _rb.velocity = new Vector2(0f, 0f);
        }
    }
}
