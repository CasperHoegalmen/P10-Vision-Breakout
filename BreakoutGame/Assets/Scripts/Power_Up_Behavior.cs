using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Up_Behavior : MonoBehaviour
{
    public GameObject ball;


    void Start()
    {
        ball = GameObject.Find("ball");

    }
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        grant_Buff("slow_Ball");
        Destroy(gameObject);
        
    }

    void grant_Buff(string power)
    {
        if(power == "slow_Ball")
        {
            ball.GetComponent<BallMovement>().speed -= 85;
            ball.GetComponent<Rigidbody2D>().AddForce(transform.forward*ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
        }
    }
}
