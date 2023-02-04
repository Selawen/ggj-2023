using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType(typeof(PauseMenu)).Length < 2)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);

        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        Time.timeScale = panel.activeInHierarchy ? 1 : 0;

        Debug.Log(Time.timeScale);

        panel.SetActive(!panel.activeInHierarchy);
    }
}
