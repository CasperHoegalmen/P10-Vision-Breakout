using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class RacketMovement : MonoBehaviour
{
    public float speed = 150;
    public int Move_Input;
    public Camera mainCam;
    public float Racket_Screen_Pos;
    public int Dead_Space;
    // Update is called once per frame
    void FixedUpdate()
    {
        Print_Input();
        Racket_Screen_Pos = mainCam.WorldToScreenPoint(this.gameObject.transform.TransformPoint(this.gameObject.transform.position)).x;
        Gaze_Racket_Input();
        //        float horizontalInput = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = Vector2.right * Move_Input * speed;
    }

    void Gaze_Racket_Input()
    {
        if (TobiiAPI.GetGazePoint().Screen.x < Racket_Screen_Pos && Racket_Screen_Pos - TobiiAPI.GetGazePoint().Screen.x > Dead_Space)
        {
            Move_Input = -1;
        }
        else if (TobiiAPI.GetGazePoint().Screen.x > Racket_Screen_Pos && TobiiAPI.GetGazePoint().Screen.x - Racket_Screen_Pos > Dead_Space)
        {
            Move_Input = 1;
        }
        else
        {
            Move_Input = 0;
        }
    }

    void Print_Input()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Racket_Screen_Pos: " + Racket_Screen_Pos);
            Debug.Log("Gaze Point: " + TobiiAPI.GetGazePoint().Screen.x);

        }
    }

}
