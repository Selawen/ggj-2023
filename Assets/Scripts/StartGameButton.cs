using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    
    public GameObject panel;

    private void Start()
    {
        if(!GameObject.Find("LevelManager").GetComponent<LevelManager>().gameStarted)
            Time.timeScale = 0;
        else
        {
            panel.SetActive(false);
        }
    }

    public void DeactivatePanel()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().gameStarted = true;
        Time.timeScale = 1; 
        panel.SetActive(false);
    }
}
