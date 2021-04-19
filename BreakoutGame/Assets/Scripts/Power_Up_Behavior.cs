using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Up_Behavior : MonoBehaviour
{

    public GameObject Game_Manager;
    public string Assigned_Power_Up;


    void Start()
    {
        Game_Manager = GameObject.Find("GameManager");

    }

    private void Update()
    {
        Despawn();
    }

    void Despawn()
    {
        if(gameObject.transform.position.y < -175)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        GameObject.Find("Power_Up_Audio").GetComponent<AudioSource>().Play();
        Game_Manager.GetComponent<Random_Power_Up_Manager>().grant_Buff(Assigned_Power_Up);
        Destroy(gameObject);
    }

    
}
