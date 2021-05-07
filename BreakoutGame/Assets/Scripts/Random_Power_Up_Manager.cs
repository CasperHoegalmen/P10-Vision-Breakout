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
    public float Slow_Effect;
    public float Speed_Effect;
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
    public List<GameObject> Blocks;
    private GameObject Power_Up_Manager;
    private GameObject Game_Manager;
    private int Power_Up_ID_Stamp;
    // Start is called before the first frame update

    private void Start()
    {
        Game_Manager = GameObject.Find("GameManager");
        Blocks = new List<GameObject>();
        Power_Up_Manager = GameObject.Find("Power_Up_Manager");
        Spawn_Chance = (float)Random.Range(0, Spawn_Frequency);
        Power_Up_Type_Probability = new float[6] { 40, 65, 85, 105, 115, 120 };
        ball = GameObject.Find("ball");
        racket = GameObject.Find("racket");
        Populate_Block_List();
    }

    void Update()
    {
        Clean_Block_List();
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

        GameObject var;

        if (Power_Up_Roll <= Power_Up_Type_Probability[0])
        {
            var = Instantiate(Slow, Spawn_Pos, Quaternion.identity);
            var.GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Slow_Ball";
            var.GetComponent<Power_Up_Behavior>().Power_Up_ID = Power_Up_ID_Stamp;
            var.transform.parent = Power_Up_Manager.transform;
            Game_Manager.GetComponent<EyeTrackingLog>().Power_Up_List.Add(var);
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[1])
        {
            var = Instantiate(Fast, Spawn_Pos, Quaternion.identity);
            var.GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Fast_Ball";
            var.GetComponent<Power_Up_Behavior>().Power_Up_ID = Power_Up_ID_Stamp;
            var.transform.parent = Power_Up_Manager.transform;
            Game_Manager.GetComponent<EyeTrackingLog>().Power_Up_List.Add(var);
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[2] && Paddle_Size < 4)
        {
            var = Instantiate(Enlarge, Spawn_Pos, Quaternion.identity);
            var.GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Large_Racket";
            var.GetComponent<Power_Up_Behavior>().Power_Up_ID = Power_Up_ID_Stamp;
            var.transform.parent = Power_Up_Manager.transform;
            Game_Manager.GetComponent<EyeTrackingLog>().Power_Up_List.Add(var);
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[3] && Paddle_Size > -4)
        {
            var = Instantiate(Shrink, Spawn_Pos, Quaternion.identity);
            var.GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Small_Racket";
            var.GetComponent<Power_Up_Behavior>().Power_Up_ID = Power_Up_ID_Stamp;
            var.transform.parent = Power_Up_Manager.transform;
            Game_Manager.GetComponent<EyeTrackingLog>().Power_Up_List.Add(var);
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[4])
        {
            var = Instantiate(Destructor, Spawn_Pos, Quaternion.identity);
            var.GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Destructor";
            var.GetComponent<Power_Up_Behavior>().Power_Up_ID = Power_Up_ID_Stamp;
            var.transform.parent = Power_Up_Manager.transform;
            Game_Manager.GetComponent<EyeTrackingLog>().Power_Up_List.Add(var);
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[5])
        {
            var = Instantiate(Sticky_Paddle, Spawn_Pos, Quaternion.identity);
            var.GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Sticky_Racket";
            var.GetComponent<Power_Up_Behavior>().Power_Up_ID = Power_Up_ID_Stamp;
            var.transform.parent = Power_Up_Manager.transform;
            Game_Manager.GetComponent<EyeTrackingLog>().Power_Up_List.Add(var);
        }

        Power_Up_ID_Stamp++;
    }


    void Generate_Power_Up_Variables()
    {
        Spawn_Pos_X = Random.Range(200, 930);
        Spawn_Pos = new Vector3(Spawn_Pos_X, 700, 0);
        Power_Up_Roll = Random.Range(0, 120);
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
                ball.GetComponent<BallMovement>().speed -= Slow_Effect;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;

            case "Fast_Ball":
                ball.GetComponent<BallMovement>().speed += Speed_Effect;
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                break;

            case "Large_Racket":
                racket.transform.localScale += new Vector3(0.3f, 0.0f, 0.0f);
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                if (Paddle_Size < 0)
                {
                    Paddle_Size = 0;
                    racket.transform.localScale = new Vector3(3.0f, 2.5f, 1.0f);
                }
                Paddle_Size += 1;
                break;

            case "Small_Racket":
                racket.transform.localScale -= new Vector3(0.3f, 0.0f, 0.0f);
                ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * ball.GetComponent<BallMovement>().speed, ForceMode2D.Impulse);
                if (Paddle_Size > 0)
                {
                    Paddle_Size = 0;
                    racket.transform.localScale = new Vector3(3.0f, 2.5f, 1.0f);
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


    void Populate_Block_List()
    {
        foreach (Transform child in GameObject.Find("BlockManager_Red").transform)
        {
            Blocks.Add(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("BlockManager_Grey").transform)
        {
            Blocks.Add(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("BlockManager_Blue").transform)
        {
            Blocks.Add(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("BlockManager_Purple").transform)
        {
            Blocks.Add(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("BlockManager_Green").transform)
        {
            Blocks.Add(child.gameObject);
        }
    }

    void Clean_Block_List()
    {
        Blocks.RemoveAll(item => item == null);
    }
}
