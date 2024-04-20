using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    private float HorSpeed;

    Rigidbody2D rb;


    private void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate(){
        transform.Translate(HorSpeed, 0, 0);
    }
    public void OnRight(){
        HorSpeed = speed;
    }
    public void OnLeft(){
        HorSpeed = -speed;
    }
    public void OnStop(){
        HorSpeed = 0;
    }
}