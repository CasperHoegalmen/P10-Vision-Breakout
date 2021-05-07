using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    public float Additional_Spawn_Chance;
    public GameObject Power_Up_Prefab;
    float[] Power_Up_Spawn_Probability = new float[] { 0, 100 };
    float[] Power_Up_Type_Probability = new float[] { 20, 40, 60, 80, 100 };
    public GameObject GameManager;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }
    
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        spawn_roulette();
        GameManager.GetComponent<GameManager>().Remove_Block();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        spawn_roulette();
        GameManager.GetComponent<GameManager>().Remove_Block();
        this.gameObject.SetActive(false);
    }

    //First rolls to see if power_up spawns. Then rolls to see the type of power_up.
    void spawn_roulette()
    {
        int Spawn_Roll = Random.Range(0, 100);

        if (Spawn_Roll <= Power_Up_Spawn_Probability[0])
        {
        }

        else if (Spawn_Roll <= Power_Up_Spawn_Probability[1])
        {
            //Spawn_Power_Up();
        }

    }
    
    void Spawn_Power_Up()
    {
        int Power_Up_Roll = Random.Range(0, 100);

        if (Power_Up_Roll <= Power_Up_Type_Probability[0])
        {
            Instantiate(Power_Up_Prefab, this.gameObject.transform.position, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Slow_Ball";
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[1])
        {
            Instantiate(Power_Up_Prefab, this.gameObject.transform.position, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Fast_Ball";
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[2])
        {
            Instantiate(Power_Up_Prefab, this.gameObject.transform.position, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Large_Racket";
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[3])
        {
            Instantiate(Power_Up_Prefab, this.gameObject.transform.position, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Small_Racket";
        }
        else if (Power_Up_Roll <= Power_Up_Type_Probability[4])
        {
            Instantiate(Power_Up_Prefab, this.gameObject.transform.position, Quaternion.identity).GetComponent<Power_Up_Behavior>().Assigned_Power_Up = "Large_Ball";
        }
    }
}
