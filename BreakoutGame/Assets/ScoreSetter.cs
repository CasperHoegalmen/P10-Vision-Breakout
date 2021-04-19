using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Visible_Score").GetComponent<TextMeshProUGUI>().SetText("" + GameObject.Find("Score").GetComponent<ScoreManager>().Gathered_Score);
    }

}
