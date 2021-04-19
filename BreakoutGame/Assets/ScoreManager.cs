using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public int Gathered_Score;
    // Start is called before the first frame update

    public void Gather_Score()
    {
        Gathered_Score = GameObject.Find("GameManager").GetComponent<GameManager>().Score;
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
