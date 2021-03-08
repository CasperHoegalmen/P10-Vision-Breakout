using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Up_Manager : MonoBehaviour
{
    public float Additional_Spawn_Chance;
    public GameObject Power_up_Prefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    //First rolls to see if power_up spawns. Then rolls to see the type of power_up.
    void spawn_roulette()
    {
        float[] spawn_probability = new float[] { 85, 15 };
        float[] spawn_type_probability = new float[] { 20, 40, 60, 80, 100 };

        if(Random.Range(0, 100) >= spawn_probability[0])
        {
            if(Random.Range(0,100) >= spawn_type_probability[0])
            {
                Instantiate(Power_up_Prefab);
            }
            if(Random.Range(0, 100) >= spawn_type_probability[1])
            {
                Instantiate(Power_up_Prefab);
            }
            if (Random.Range(0, 100) >= spawn_type_probability[2])
            {
                Instantiate(Power_up_Prefab);
            }
            if (Random.Range(0, 100) >= spawn_type_probability[3])
            {
                Instantiate(Power_up_Prefab);
            }
            if (Random.Range(0, 100) >= spawn_type_probability[4])
            {
                Instantiate(Power_up_Prefab);
            }
        } 

    }

}
