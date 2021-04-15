using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int Score;
    public int Remaining_Blocks = 75;
    public GameObject Ball;
    public GameObject Block_Manager;
    public TextMeshProUGUI Score_UI;

    void Start()
    {
        Score_UI = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Game_Over();
    }

    public void Remove_Block()
    {
        Remaining_Blocks -= 1;
        Score += 100;
        Score_UI.text = "Score: " + Score;

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
            Debug.Log("Game Over");
        }
    }
}
