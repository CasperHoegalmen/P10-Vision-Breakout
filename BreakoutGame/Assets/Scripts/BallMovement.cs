using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public AudioSource Collision_Sound;
    public float speed;
    float Speed_Growth = 4.0f;
    private float Timer;
    float Sticky_Timer;
    float Sticky_Cooldown_Timer;
    GameObject racket;
    // Start is called before the first frame update


    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        racket = GameObject.Find("racket");
    }

    void Update()
    {
        Timer_Update();
        Simulate_Rigidbody();
    }

    void Timer_Update()
    {
       if(Timer + 1.0f <= Time.fixedTime)
        {
            Timer = Time.fixedTime;
            Update_Speed();
        }
       
    }

    void Update_Speed()
    {
        speed += Speed_Growth;
    }


    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketWidth)
    {
        // ascii art:
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Collision_Sound.Play();
        //Direction setting on hitting racket
        if (col.gameObject.name == "racket")
        {
            float x = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.x);
            Vector2 dir = new Vector2(x, 1).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;

            if (GameObject.Find("GameManager").GetComponent<Random_Power_Up_Manager>().Sticky_Racket_Active == true && Sticky_Cooldown_Timer <= Time.fixedTime)
            {
                Sticky_Timer = Time.fixedTime + 2.0f;
                Sticky_Cooldown_Timer = Time.fixedTime + 4.0f;
                racket.GetComponent<RacketMovement>().Sticky_Offset.x = racket.transform.position.x - this.gameObject.transform.position.x;
                this.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            }
        }
    }

    void Simulate_Rigidbody()
    {
        if(Sticky_Timer <= Time.fixedTime)
        {
            this.gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }
    }
}
