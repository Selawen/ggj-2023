using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private GameObject[] levelsToActivate;
    [SerializeField] private bool completed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (completed)
        {
            foreach (GameObject g in levelsToActivate)
            {
                g.SetActive(true);
            }
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
