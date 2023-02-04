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
        if (GameManager.Ins.PlayerWaterCount > GameManager.Ins.BugWaterCount)
            ScoreText.text = $"<size={130}><color=aqua>{GameManager.Ins.PlayerWaterCount}</color></size>" +
                $":<size={110}><color=red>{GameManager.Ins.BugWaterCount}</color></size>";

        else if (GameManager.Ins.PlayerWaterCount < GameManager.Ins.BugWaterCount)
            ScoreText.text = $"<size={110}><color=aqua>{GameManager.Ins.PlayerWaterCount}</color></size>" +
                $":<size={130}><color=red>{GameManager.Ins.BugWaterCount}</color></size>";

        else
            ScoreText.text = $"<size={110}><color=aqua>{GameManager.Ins.PlayerWaterCount}</color></size>" +
                $":<size={110}><color=red>{GameManager.Ins.BugWaterCount}</color></size>";
    }
}
