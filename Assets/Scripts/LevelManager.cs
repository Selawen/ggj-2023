using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<int> completedLevels;

    private void Awake()
    {
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
