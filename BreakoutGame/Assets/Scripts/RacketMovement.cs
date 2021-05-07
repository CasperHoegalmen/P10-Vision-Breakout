using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Tobii.Gaming;

public class RacketMovement : MonoBehaviour
{
    public float speed = 300;
    public int Move_Input;
    public Camera mainCam;
    public float Racket_Screen_Pos;
    public Vector2 Current_Gazepoint;
    GameObject Game_Manager;
    public GameObject ball;
    public Vector3 Sticky_Offset;
    [SerializeField] float Gaze_Point_Offset;
    public float Racket_Offset;
    // Update is called once per frame

    private void Start()
    {
        Game_Manager = GameObject.Find("GameManager");
        ball = GameObject.Find("ball");
    }

    void FixedUpdate()
    {
        Racket_Offset = gameObject.transform.position.x * -0.12f - 120 * Game_Manager.GetComponent<Random_Power_Up_Manager>().Paddle_Size;
        Sticky_Movement();
        Current_Gazepoint = TobiiAPI.GetGazePoint().Screen*Gaze_Point_Offset;
        Racket_Screen_Pos = this.gameObject.transform.TransformPoint(this.gameObject.transform.position).x + Racket_Offset;
        Gaze_Racket_Input();
        GetComponent<Rigidbody2D>().velocity = Vector2.right * Move_Input * speed;
        //Debug.Log("Current Gazepoint: " + Current_Gazepoint + " VS Racket Point: " + Racket_Screen_Pos);
    }

    void Gaze_Racket_Input()
    {
        if (Current_Gazepoint.x < Racket_Screen_Pos && Mathf.Abs(Current_Gazepoint.x - Racket_Screen_Pos) > 10)
        {
            Move_Input = -1;
            //Debug.Log("Left" + " Current Gazepoint: " + Current_Gazepoint + " VS Racket Point: " + Racket_Screen_Pos);
        }
        else if (Current_Gazepoint.x > Racket_Screen_Pos && Mathf.Abs(Current_Gazepoint.x - Racket_Screen_Pos) > 10)
        {
            Move_Input = 1;
            //Debug.Log("Right" + " Current Gazepoint: " + Current_Gazepoint + " VS Racket Point: " + Racket_Screen_Pos);
        }
        else
        {
            Move_Input = 0;
        }
    }

    void Sticky_Movement()
    {
        
        if(GameObject.Find("GameManager").GetComponent<Random_Power_Up_Manager>().Sticky_Racket_Active == true && ball.GetComponent<Rigidbody2D>().simulated == false)
        {
            ball.transform.position = GameObject.Find("racket").transform.position + Sticky_Offset;
        }
    }
}
