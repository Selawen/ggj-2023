using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text ScoreText;

    // Update is called once per frame
    void Update()
    {
        ScoreTextControl();
    }

    void ScoreTextControl()
    {
        if (GameManager.In.PlayerWaterCount > GameManager.In.BugWaterCount)
            ScoreText.text = $"<size={140}><color=aqua>{GameManager.In.PlayerWaterCount}</color></size>" +
                $":<size={120}><color=red>{GameManager.In.BugWaterCount}</color></size>";

        else if (GameManager.In.PlayerWaterCount < GameManager.In.BugWaterCount)
            ScoreText.text = $"<size={120}><color=aqua>{GameManager.In.PlayerWaterCount}</color></size>" +
                $":<size={140}><color=red>{GameManager.In.BugWaterCount}</color></size>";

        else
            ScoreText.text = $"<size={120}><color=aqua>{GameManager.In.PlayerWaterCount}</color></size>" +
                $":<size={120}><color=red>{GameManager.In.BugWaterCount}</color></size>";
    }
}
