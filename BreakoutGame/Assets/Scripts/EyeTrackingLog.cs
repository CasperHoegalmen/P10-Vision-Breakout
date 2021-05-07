
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Tobii.Gaming;

public class EyeTrackingLog : MonoBehaviour
{
    [SerializeField] TextAsset Text_Log;
    [SerializeField] GameObject Racket;
    [SerializeField] GameObject Ball;
    public List<GameObject> Block_List;
    public List<GameObject> Power_Up_List;
    List<String> Log_Array;
    private int Level_Num;
    public int Death_Count;
    public float Racket_Gaze_Timer;
    public float Ball_Gaze_Timer;
    public float Eye_Gaze_Distance_Threshold;
    public float Block_Gaze_Timer;
    public float Slow_Gaze_Timer;
    public float Fast_Gaze_Timer;
    public float Enlarge_Gaze_Timer;
    public float Shrink_Gaze_Timer;
    public float Destructor_Gaze_Timer;
    public float Sticky_Gaze_Timer;

    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.Find("ball");
        Racket = GameObject.Find("racket");
        Log_Array = new List<string>();
        Power_Up_List = new List<GameObject>();
        Block_List = new List<GameObject>();
        Populate_Block_List();
    }

    void Update()
    {
        Log_Objects();
    }

    void Log_Objects()
    {
        int Logged = 0;

        Block_List.RemoveAll(item => item == null);

        for (int i = 0; i < Block_List.Count; i++)
        {
            if (Vector2.Distance(TobiiAPI.GetGazePoint().Screen, Block_List[i].gameObject.GetComponent<Collider2D>().ClosestPoint(TobiiAPI.GetGazePoint().Screen)) < Eye_Gaze_Distance_Threshold && Logged == 0)
            {
                Block_Gaze_Timer += Time.deltaTime;
                Logged = 1;
            }
        }

        Power_Up_List.RemoveAll(item => item == null);

        for (int i = 0; i < Power_Up_List.Count; i++)
        {
            if (Vector2.Distance(TobiiAPI.GetGazePoint().Screen, Power_Up_List[i].gameObject.GetComponent<Collider2D>().ClosestPoint(TobiiAPI.GetGazePoint().Screen)) < Eye_Gaze_Distance_Threshold)
            {
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Slow_Ball")
                {
                    Slow_Gaze_Timer += Time.deltaTime;
                }
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Fast_Ball")
                {
                    Fast_Gaze_Timer += Time.deltaTime;
                }
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Large_Racket")
                {
                    Enlarge_Gaze_Timer += Time.deltaTime;
                }
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Small_Racket")
                {
                    Shrink_Gaze_Timer += Time.deltaTime;
                }
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Destructor")
                {
                    Destructor_Gaze_Timer += Time.deltaTime;
                }
                if (Power_Up_List[i].GetComponent<Power_Up_Behavior>().Assigned_Power_Up == "Sticky_Racket")
                {
                    Sticky_Gaze_Timer += Time.deltaTime;
                }
            }

            if (Vector2.Distance(TobiiAPI.GetGazePoint().Screen, Ball.GetComponent<Collider2D>().ClosestPoint(TobiiAPI.GetGazePoint().Screen)) < Eye_Gaze_Distance_Threshold + 20)
            {
                Ball_Gaze_Timer += Time.deltaTime;
            }

            if (Vector2.Distance(TobiiAPI.GetGazePoint().Screen, Racket.GetComponent<Collider2D>().ClosestPoint(TobiiAPI.GetGazePoint().Screen)) < Eye_Gaze_Distance_Threshold + 20)
            {
                Racket_Gaze_Timer += Time.deltaTime;
            }


        }
    }

    void Populate_Block_List()
    {
        foreach (Transform child in GameObject.Find("BlockManager_Red").transform)
        {
            Block_List.Add(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("BlockManager_Grey").transform)
        {
            Block_List.Add(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("BlockManager_Blue").transform)
        {
            Block_List.Add(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("BlockManager_Purple").transform)
        {
            Block_List.Add(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("BlockManager_Green").transform)
        {
            Block_List.Add(child.gameObject);
        }
    }

    public void Write_To_File()
    {
        string path = "Assets/Text Assets/Log.txt";
        Level_Num += 1;
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Level " + Level_Num + " Gaze Logging" + System.Environment.NewLine + "Ball: " + Ball_Gaze_Timer + System.Environment.NewLine + "Racket: " + Racket_Gaze_Timer + System.Environment.NewLine +  "Blocks: " + Block_Gaze_Timer + System.Environment.NewLine + "Slow: " + Slow_Gaze_Timer + System.Environment.NewLine + "Fast: " + Fast_Gaze_Timer + System.Environment.NewLine + "Enlarge: " + Enlarge_Gaze_Timer + System.Environment.NewLine + "Shrink: " + Shrink_Gaze_Timer + System.Environment.NewLine + "Destructor: " + Destructor_Gaze_Timer + System.Environment.NewLine + "Sticky: " + Sticky_Gaze_Timer + System.Environment.NewLine + System.Environment.NewLine);
        writer.Close();

    }
}
   