using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public int Gathered_Score;
    public TextMeshProUGUI Score_UI;
    // Start is called before the first frame update

    public void Add_Score()
    {
        Gathered_Score += 100;
        Score_UI.text = "Score: " + Gathered_Score;
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
