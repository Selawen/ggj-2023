using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<int> completedLevels;

    public float amountOfLevels;

    public Gradient rootColour;

    private void Awake()
    {
        if (amountOfLevels == 0)
        {
            amountOfLevels = FindObjectsOfType(typeof(LevelButton)).Length;
        }

        if (FindObjectsOfType(typeof(LevelManager)).Length < 2)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

    public void LevelComplete(int completedLevel)
    {
        completedLevels.Add(completedLevel);
    }
}
