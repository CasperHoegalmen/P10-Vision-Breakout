using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    public int Gathered_Score;
    public TextMeshProUGUI Score_UI;
    // Start is called before the first frame update

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try
        {
            Score_UI = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        }
        catch
        {

        }
        
        
    }

    public void Add_Score()
    {
        Gathered_Score += 100;
        Score_UI.text = "Score: " + Gathered_Score;
    }

    public void Death_Score_Detraction()
    {
        Gathered_Score -= 500;
        Score_UI.text = "Score: " + Gathered_Score;
    }

    void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);
    }
}
