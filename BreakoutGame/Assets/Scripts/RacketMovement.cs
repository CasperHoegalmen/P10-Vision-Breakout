using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class RacketMovement : MonoBehaviour
{
    public float speed = 150;
    public int Input;
    // Update is called once per frame
    void FixedUpdate()
    {   
//        float horizontalInput = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = Vector2.right * Input * speed;
    }

    void Gaze_Racket_Input()
    {
        if(TobiiAPI.GetGazePoint().Screen.x < this.gameObject.transform.position.x)
        {
            Input = -1;
        }
        else if (TobiiAPI.GetGazePoint().Screen.x > this.gameObject.transform.position.x)
        {
            Input = 1;
        }
        else
        {
            Input = 0;
        }

    }
}
