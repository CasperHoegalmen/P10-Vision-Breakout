﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Button_Behavior : MonoBehaviour
{

    public void OnButtonPress()
    {
        SceneManager.LoadScene("Main");
        Debug.Log("Hello");
    }
}