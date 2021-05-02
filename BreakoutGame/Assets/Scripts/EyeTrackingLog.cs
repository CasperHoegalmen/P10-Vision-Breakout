
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
    public List<GameObject> Block_List;
    public List<GameObject> Power_Up_List;
    float Eye_Gaze_Distance_Threshold;


    void Buffer_Updates()
    {
        Update_Pos_Buffer();
        Update_Pos_Difference_Buffer();
    }

    void Detect_Saccade()
    {
        Buffer_Updates();
        if (Current_Difference >= Saccade_Threshold)
        {
            Debug.Log("Saccade Detected with Value: " + Current_Difference);
        }

        Current_Difference = 0;


    }

    // Start is called before the first frame update
    void Start()
    {
        Populate_Block_List();
        Pos_Buffer = new Vector2[2];
        Power_Up_List = new List<GameObject>();
        Block_List = new List<GameObject>();
    }

    private void Update()
    {
        Log_Objects();
        if (Time.fixedTime > Prev_Second + 0.3f)
        {
            Detect_Saccade();
            Prev_Second = Time.fixedTime;
        }
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

    void Log_Objects()
    {
        foreach(GameObject block in Block_List)
            if (Vector2.Distance(TobiiAPI.GetGazePoint().Screen, block.gameObject.GetComponent<Collider2D>().ClosestPoint(TobiiAPI.GetGazePoint().Screen)) > Eye_Gaze_Distance_Threshold)
            {
                block.GetComponent<Gaze_Time>().Timer += Time.deltaTime;
            }

        Power_Up_List.RemoveAll(item => item == null);

        for(int i = 0; i < Power_Up_List.Count; i++)
        {
            if (Vector2.Distance(TobiiAPI.GetGazePoint().Screen, Power_Up_List[i].gameObject.GetComponent<Collider2D>().ClosestPoint(TobiiAPI.GetGazePoint().Screen)) > Eye_Gaze_Distance_Threshold)
            {
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Slow_Ball")
                {
                    Power_Up_List[i].transform.parent.gameObject.GetComponent<Gaze_Time_Power_Up>().Slow_Ball_Timer += Time.deltaTime;
                }
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Fast_Ball")
                {
                    Power_Up_List[i].transform.parent.gameObject.GetComponent<Gaze_Time_Power_Up>().Fast_Ball_Timer += Time.deltaTime;
                }
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Large_Racket")
                {
                    Power_Up_List[i].transform.parent.gameObject.GetComponent<Gaze_Time_Power_Up>().Large_Racket_Timer += Time.deltaTime;
                }
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Small_Racket")
                {
                    Power_Up_List[i].transform.parent.gameObject.GetComponent<Gaze_Time_Power_Up>().Small_Racket_Timer += Time.deltaTime;
                }
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Destructor")
                {
                    Power_Up_List[i].transform.parent.gameObject.GetComponent<Gaze_Time_Power_Up>().Destructor_Timer += Time.deltaTime;
                }
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Sticky_Racket")
                {
                    Power_Up_List[i].transform.parent.gameObject.GetComponent<Gaze_Time_Power_Up>().Sticky_Racket_Timer += Time.deltaTime;
                }
            }
            

            }
    }

    void Populate_Block_List()
    {
        foreach (Transform child in GameObject.Find("Block_Manager").transform)
            foreach (Transform grand_child in child)
                Block_List.Add(grand_child.gameObject);
    }
}
   