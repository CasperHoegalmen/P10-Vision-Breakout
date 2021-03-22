using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class EyeTrackingLog : MonoBehaviour
{
    public float Saccade_Detection_Threshold;
    public Vector2 Eye_Pos;
    public float Passed_Time;
    public int Vect_Length_Buffer_Index;
    public float Interval;
    public Vector2[] Vect_Length_Buffer;
    // Start is called before the first frame update
    void Start()
    {
        Vect_Length_Buffer = new Vector2[20];
    }

    // Update is called once per frame
    void Update()
    {
        Gaze_Pos_Tracking();
    }

    private void Detect_Saccade()
    {
        for(int i = 0; i <= Vect_Length_Buffer.Length; i++)
        {
            if (Vect_Length_Buffer[i].x >= Saccade_Detection_Threshold)
            {
                Debug.Log("Saccade Detected");
                Vect_Length_Buffer[i].x = 0;
            }
        }
            
    }

    private void Gaze_Pos_Tracking()
    {
        Update_Eye_Pos();
        Update_Vect_Buffer();
        Update_Index();
    }

    private void Update_Vect_Buffer()
    {
        for (int i = 0; i <= Vect_Length_Buffer.Length; i++)
        {
            Vect_Length_Buffer[i].y += 0.2f;
            if (Vect_Length_Buffer[i].y <= Interval)
            {
                Vect_Length_Buffer[i].x += Eye_Pos.sqrMagnitude;
            }
        }
    }

    private void Update_Index()
    {
        Vect_Length_Buffer_Index += 1;

        if (Vect_Length_Buffer_Index >= Vect_Length_Buffer.Length)
        {
            Vect_Length_Buffer_Index = 0;
        }
    }

    private void Update_Eye_Pos()
    {
        Eye_Pos = TobiiAPI.GetGazePoint().Screen;
    }



}
