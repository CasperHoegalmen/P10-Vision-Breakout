using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public AudioSource Collision_Sound;
    public float speed = 100.0f;
    // Start is called before the first frame update
   

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
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
        //Increase Speed on block collision
        if (col.gameObject.name != "border_right" || col.gameObject.name != "border_left" || col.gameObject.name != "border_top" || col.gameObject.name != "racket")
        {
            speed += 3f;
        }
        //Direction setting on hitting racket
        if (col.gameObject.name == "racket")
        {
            float x = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.x);
            Vector2 dir = new Vector2(x, 1).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
        
    }
}
