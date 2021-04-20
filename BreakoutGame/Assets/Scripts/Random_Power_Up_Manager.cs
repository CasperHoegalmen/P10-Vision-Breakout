using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Power_Up_Manager : MonoBehaviour
{

    public GameObject ball;
    public GameObject racket;
    public float timer;
    public float Spawn_Chance;
    public float Spawn_Frequency;
    int Spawn_Pos_X;
    Vector3 Spawn_Pos;
    int Power_Up_Roll;
    float[] Power_Up_Type_Probability;
    public GameObject Slow;
    public GameObject Fast;
    public GameObject Enlarge;
    public GameObject Shrink;
    public GameObject Destructor;
    public GameObject Sticky_Paddle;
    public bool Destructor_Active;
    public float Destructor_Timer;
    public bool Sticky_Racket_Active;
    public float Sticky_Racket_Timer;
    public int Paddle_Size;
    public GameObject[] Blocks = new GameObject[45];
    // Start is called before the first frame update

    private void Start()
    {
        Spawn_Chance = (float)Random.Range(0, Spawn_Frequency);
        Power_Up_Type_Probability = new float[] { 20, 40, 60, 80, 100, 120 };
        ball = GameObject.Find("ball");
        racket = GameObject.Find("racket");
        Get_Block_Arrays();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Spawn_Chance)
        {
            Spawn_Power_Up();
        }
        Buff_Timer_Updates();
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
        else if (Power_Up_Roll <= Power_Up_Type_Probability[4])
        {
            Instantiate(Destructor, Spawn_Pos, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Destructor";
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[5])
        {
            Instantiate(Sticky_Paddle, Spawn_Pos, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Sticky_Racket";
        }
    }


    void Generate_Power_Up_Variables()
    {
        Spawn_Pos_X = Random.Range(-170, 170);
        Spawn_Pos = new Vector3(Spawn_Pos_X, 250, 0);
        Power_Up_Roll = Random.Range(0, 140);
    }

    void Reset_Timer()
    {
        timer = 0;
        Spawn_Chance = (float)Random.Range(0, Spawn_Frequency);
    }

    public void grant_Buff(string Assigned_Power_Up)
    {
        switch (Assigned_Power_Up)
        {
            case "Slow_Ball":
                ball.GetComponent<BallMovement>().speed -= 15;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;

            case "Fast_Ball":
                ball.GetComponent<BallMovement>().speed += 15;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;

            case "Large_Racket":
                racket.transform.localScale += new Vector3(0.1f, 0.0f, 0.0f);
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                if (Paddle_Size < 0)
                {
                    Paddle_Size = 0;
                    racket.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                Paddle_Size += 1;
                break;

            case "Small_Racket":
                racket.transform.localScale -= new Vector3(0.1f, 0.0f, 0.0f);
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                if (Paddle_Size > 0)
                {
                    Paddle_Size = 0;
                    racket.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                Paddle_Size -= 1;
                break;

            case "Destructor":
                Destructor_Timer = Time.fixedTime + 15;
                foreach (GameObject block in Blocks)
                {
                    block.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                break;

            case "Sticky_Racket":
                Sticky_Racket_Timer = Time.fixedTime + 15.0f;
                Sticky_Racket_Active = true;
                break;

        }
    }

    void Buff_Timer_Updates()
    {
        if (Destructor_Timer <= Time.fixedTime)
        {
            foreach (GameObject block in Blocks)
            {
                block.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }

        if (Sticky_Racket_Timer <= Time.fixedTime)
        {
            Sticky_Racket_Active = false;
        }
    }


    void Get_Block_Arrays()
    {
        int i = 0;
        foreach (Transform child in GameObject.Find("BlockManager_Red").transform)
        {
            Blocks[i] = child.gameObject;
            i++;
        }
        foreach (Transform child in GameObject.Find("BlockManager_Grey").transform)
        {
            Blocks[i] = child.gameObject;
            i++;
        }
        foreach (Transform child in GameObject.Find("BlockManager_Blue").transform)
        {
            Blocks[i] = child.gameObject;
            i++;
        }
        foreach (Transform child in GameObject.Find("BlockManager_Purple").transform)
        {
            Blocks[i] = child.gameObject;
            i++;
        }
        foreach (Transform child in GameObject.Find("BlockManager_Green").transform)
        {
            Blocks[i] = child.gameObject;
            i++;
        }
    }
}
