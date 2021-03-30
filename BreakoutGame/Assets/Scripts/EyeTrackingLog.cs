using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class EyeTrackingLog : MonoBehaviour
{
    [SerializeField] Vector2[] Pos_Buffer;
    float Current_Difference = 0;
    [SerializeField] float Saccade_Threshold;
    float Prev_Second;

    // Start is called before the first frame update
    void Start()
    {
        Pos_Buffer = new Vector2[2];
    }

    private void Update()
    {
        if(Time.fixedTime > Prev_Second + 0.3f)
        {
            Detect_Saccade();
            Prev_Second = Time.fixedTime;
        }
    }

    void Buffer_Updates()
    {
        Update_Pos_Buffer();
        Update_Pos_Difference_Buffer();
    }
    void Update_Pos_Buffer()
    {
        Pos_Buffer[1] = GameObject.Find("racket").GetComponent<RacketMovement>().Current_Gazepoint;
    }

    void Update_Pos_Difference_Buffer()
    {
        //Adds up the gaze position difference from last frame. If above 0 adds it to the Pos_Difference buffer.
        Current_Difference += Mathf.Abs(Pos_Buffer[0].x - Pos_Buffer[1].x);
        Current_Difference += Mathf.Abs(Pos_Buffer[0].y - Pos_Buffer[1].y);
    }

    void Detect_Saccade()
    {
        Buffer_Updates();
        if (Current_Difference >= Saccade_Threshold)
        {
           // Debug.Log("Saccade Detected with Value: " + Current_Difference);
        }

        Current_Difference = 0;


    }
}
