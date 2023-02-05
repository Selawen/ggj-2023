using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public GameObject panel;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void DeactivatePanel()
    {
        Time.timeScale = 1; 
        panel.SetActive(false);
    }
}
