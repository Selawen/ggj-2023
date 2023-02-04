using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameManager manager;
    public Text ScoreText;
    public Text levelCompleteText;

    private void Awake()
    {
        manager = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTextControl();
    }

    void ScoreTextControl()
    {
        if (manager.PlayerWaterCount > manager.BugWaterCount)
        {
            ScoreText.text = $"<size={50}><color=aqua>{manager.PlayerWaterCount}</color></size>" +
                $":<size={45}><color=red>{manager.BugWaterCount}</color></size>";
            levelCompleteText.text = $"<size={72}><color=aqua>YOU DID IT!</color></size>";
        }
        else if (manager.PlayerWaterCount < manager.BugWaterCount)
        {
            ScoreText.text = $"<size={45}><color=aqua>{manager.PlayerWaterCount}</color></size>" +
                $":<size={50}><color=red>{manager.BugWaterCount}</color></size>";
            levelCompleteText.text = $"<size={72}><color=red>Too bad, \n try again next time</color></size>";
        }
        else 
        { 
        ScoreText.text = $"<size={45}><color=aqua>{manager.PlayerWaterCount}</color></size>" +
            $":<size={45}><color=red>{manager.BugWaterCount}</color></size>";
        } 
    }
}
