using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool gameStarted = false;

    public List<int> completedLevels;

    public float amountOfLevels;

    public Gradient rootColour;

    private void Awake()
    {
        if (FindObjectsOfType(typeof(LevelManager)).Length < 2)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);

        if (amountOfLevels == 0)
        {
            amountOfLevels = FindObjectsOfType(typeof(LevelButton)).Length;
        }
    }

    public void LevelComplete(int completedLevel)
    {
        completedLevels.Add(completedLevel);
    }

    public void ResetProgress()
    {
        completedLevels = new List<int>();
        SceneManager.LoadScene(0);
    }
}
