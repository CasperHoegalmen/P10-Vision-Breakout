using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Up_Behavior : MonoBehaviour
{
    public GameObject ball;
    public GameObject racket;
    public GameObject Game_Manager;
    public string Assigned_Power_Up;
    int Paddle_Size;


    void Start()
    {
        Game_Manager = GameObject.Find("GameManager");
        ball = GameObject.Find("ball");
        racket = GameObject.Find("racket");
    }

    private void Update()
    {
        Despawn();
    }

    void Despawn()
    {
        if(gameObject.transform.position.y < -175)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        GameObject.Find("Power_Up_Audio").GetComponent<AudioSource>().Play();
        grant_Buff();
        Destroy(gameObject);

    }

    void grant_Buff()
    {
        switch (Assigned_Power_Up)
        {
            case "Slow_Ball":
                ball.GetComponent<BallMovement>().speed -= 10;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;
            
            case "Fast_Ball":
                ball.GetComponent<BallMovement>().speed += 15;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;

            case "Large_Racket":
                racket.transform.localScale += new Vector3 (0.1f, 0.0f, 0.0f);
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                if(Game_Manager.GetComponent<Random_Power_Up_Manager>().Paddle_Size < 0)
                {
                    Game_Manager.GetComponent<Random_Power_Up_Manager>().Paddle_Size = 0;
                    racket.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                Game_Manager.GetComponent<Random_Power_Up_Manager>().Paddle_Size += 1;
                break;

            case "Small_Racket":
                racket.transform.localScale -= new Vector3(0.1f, 0.0f, 0.0f);
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                if (Game_Manager.GetComponent<Random_Power_Up_Manager>().Paddle_Size > 0)
                {
                    Game_Manager.GetComponent<Random_Power_Up_Manager>().Paddle_Size = 0;
                    racket.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                Game_Manager.GetComponent<Random_Power_Up_Manager>().Paddle_Size -= 1;
                break;

            case "Large_Ball":
                Debug.Log("Spawned Large Ball");
                ball.GetComponent<BallMovement>().speed -= 0;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;
        }
    }
}
