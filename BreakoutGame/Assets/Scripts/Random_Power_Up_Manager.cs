using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Power_Up_Manager : MonoBehaviour
{
    public float timer;
    public float Spawn_Chance;
    public float Spawn_Frequency;
    int Spawn_Pos_X;
    Vector3 Spawn_Pos;
    int Power_Up_Roll;
    float[] Power_Up_Type_Probability;
    public int Paddle_Size;
    public GameObject Slow;
    public GameObject Fast;
    public GameObject Enlarge;
    public GameObject Shrink;
    // Start is called before the first frame update

    private void Start()
    {
        Spawn_Chance = (float)Random.Range(0, Spawn_Frequency);
        Power_Up_Type_Probability = new float[] { 20, 40, 60, 80, 100 };
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Spawn_Chance)
        {
            Spawn_Power_Up();
        }
    }

    //First rolls to see if power_up spawns. Then rolls to see the type of power_up.
    void Spawn_Power_Up()
    {
        Generate_Power_Up_Variables();
        Reset_Timer();
       
        if (Power_Up_Roll <= Power_Up_Type_Probability[0])
        {
            Instantiate(Slow, Spawn_Pos, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Slow_Ball";
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[1])
        {
            Instantiate(Fast, Spawn_Pos, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Fast_Ball";
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[2] && Paddle_Size < 4)
        {
            Instantiate(Enlarge, Spawn_Pos, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Large_Racket";
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[3] && Paddle_Size > -4)
        {
            Instantiate(Shrink, Spawn_Pos, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Small_Racket";
        }
    }


    void Generate_Power_Up_Variables()
    {
        Spawn_Pos_X = Random.Range(-170, 170);
        Spawn_Pos = new Vector3(Spawn_Pos_X,  250, 0);
        Power_Up_Roll = Random.Range(0, 100);
    }

    void Reset_Timer()
    {
        timer = 0;
        Spawn_Chance = (float)Random.Range(0, Spawn_Frequency);
    }
}
