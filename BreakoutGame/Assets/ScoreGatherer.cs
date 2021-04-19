using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGatherer : MonoBehaviour
{

    public int Score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Score = GameObject.Find("GameManager").GetComponent<GameManager>().Score;
    }
}
