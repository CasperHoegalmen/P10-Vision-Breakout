using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Up_Behavior : MonoBehaviour
{
    public GameObject ball;
    public GameObject racket;
    public string Assigned_Power_Up;


    void Start()
    {
        ball = GameObject.Find("ball");
        racket = GameObject.Find("racket");
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        grant_Buff();
        Destroy(gameObject);

    }

    void grant_Buff()
    {
        switch (Assigned_Power_Up)
        {
            case "Slow_Ball":
                Debug.Log("Spawned Slow Ball");
                ball.GetComponent<BallMovement>().speed -= 5;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;
            
            case "Fast_Ball":
                Debug.Log("Spawned Fast Ball");
                ball.GetComponent<BallMovement>().speed += 5;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;

            case "Large_Racket":
                Debug.Log("Spawned Large Racket");
                ball.GetComponent<BallMovement>().speed -= 0;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;

            case "Small_Racket":
                Debug.Log("Spawned Small Racket");
                ball.GetComponent<BallMovement>().speed -= 0;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;

            case "Large_Ball":
                Debug.Log("Spawned Large Ball");
                ball.GetComponent<BallMovement>().speed -= 0;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;
        }
    }
}
