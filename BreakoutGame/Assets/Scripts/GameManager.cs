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


    void Start()
    {
        Score = GameObject.Find("Final_Score");
    }

    private void Update()
    {
        Game_Over();
    }

    public void Remove_Block()
    {
        Remaining_Blocks -= 1;
        Score.GetComponent<ScoreManager>().Add_Score();

        if(Remaining_Blocks <= 0)
        {
            Remaining_Blocks = 75;
            foreach (Transform child in Block_Manager.transform)
                foreach (Transform grand_child in child)
                    grand_child.gameObject.SetActive(true);
        }
    }
    
    void Game_Over()
    {
        if(Ball.transform.position.y < -200)
        {
            SceneManager.LoadScene("End");
        }
    }
}
