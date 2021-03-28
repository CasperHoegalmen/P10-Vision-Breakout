using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class EyeTrackingLog : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] Vector2[] Pos_Buffer;
    [SerializeField] float[] Pos_Difference_Buffer;
    int[] Cycle_Count;
    [SerializeField] float Saccade_Threshold;
    float Prev_Second;

    // Start is called before the first frame update
    void Start()
    {
        Saccade_Threshold = 1;
        Pos_Buffer = new Vector2[2];
        Pos_Difference_Buffer = new float[2];
        Cycle_Count = new int[Pos_Difference_Buffer.Length];
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
        Update_Cycle_Count();
        Update_Pos_Difference_Buffer();
        Update_Index();
    }
    void Update_Pos_Buffer()
    {
        Pos_Buffer[index] = GameObject.Find("racket").GetComponent<RacketMovement>().Current_Gazepoint;
    }

    void Update_Pos_Difference_Buffer()
    {
        //Adds up the gaze position difference from last frame. If above 0 adds it to the Pos_Difference buffer.
        float Current_Difference = 0;
        if (index > 0)
        {
            Current_Difference += Mathf.Abs(Pos_Buffer[index - 1].x - Pos_Buffer[index].x);
            Current_Difference += Mathf.Abs(Pos_Buffer[index - 1].y - Pos_Buffer[index].y);
        }

        if (index == 0)
        {
            Current_Difference += Mathf.Abs(Pos_Buffer[Pos_Buffer.Length - 1].x - Pos_Buffer[index].x);
            Current_Difference += Mathf.Abs(Pos_Buffer[Pos_Buffer.Length - 1].y - Pos_Buffer[index].y);
        }

        if(Current_Difference > 0)
        {
            Pos_Difference_Buffer[index] += Current_Difference;
        }

        //Adds the difference of each new position, provided it has a higher Cycle_Count.
        for (int i = 0; i < Pos_Difference_Buffer.Length - 1; i++)
       {
         for(int x = 0; x < Pos_Difference_Buffer.Length - 1; x++ )
            {
                if (Cycle_Count[x] > Cycle_Count[index])
                {
                    Pos_Difference_Buffer[index] += Pos_Difference_Buffer[x];
                }
            }
       }
    }

    //Each Va
    void Update_Cycle_Count()
    {
        Cycle_Count[index] += 1;
    }

    void Update_Index()
    {
        index++;

        if(index > Pos_Buffer.Length - 1)
        {
            index = 0;
        }
    }

    void Detect_Saccade()
    {
        Buffer_Updates();
        if (Pos_Difference_Buffer[index] >= Saccade_Threshold)
        {
            Debug.Log("Saccade Detected with Value: " + Pos_Difference_Buffer[index]);
        }
    }
}
