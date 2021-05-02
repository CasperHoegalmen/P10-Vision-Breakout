using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject Score;
    public int Remaining_Blocks = 75;
    public GameObject Ball;
    public GameObject Block_Manager;
    float Game_Timer;
    float Prev_Time;


    void Start()
    {
        Score = GameObject.Find("Final_Score");
        Game_Timer = 20.0f;
        Prev_Time = Game_Timer;
    }

    private void Update()
    {
        Game_Over();

        Game_Timer -= Time.deltaTime;

        if (Prev_Time - 1.0f > Game_Timer)
        {
            GameObject.Find("Time").GetComponent<TextMeshProUGUI>().SetText("Time:  " + (int)Game_Timer);
            Prev_Time = Game_Timer;
        }
        

    }

    public void Remove_Block()
    {
        Score.GetComponent<ScoreManager>().Add_Score();
    }
    
    void Game_Over()
    {
        if(Ball.transform.position.y < -200 || Game_Timer <= 0.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
