using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public GameObject panel;

    public void DeactivatePanel()
    {
        panel.SetActive(false);
    }
}
