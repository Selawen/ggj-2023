using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ObjectType
{
    None, Player, Enemy
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private int level;

    public int totalWaterInLevel = -1;
    public int PlayerWaterCount = 0;
    public int BugWaterCount = 0;

    public List<Player> PlayerList = new List<Player>();

    public float MaximumSize;
    public float MinimumSize;

    public GameObject finishPanel;

    public float waitBeforeReturningToMain = 3;

    // Start is called before the first frame update
    void Start()
    {
        finishPanel.SetActive(false);
        //Test
        ResetSetting();
        BasicSetting();
    }

    private void FixedUpdate()
    {
        if (PlayerList == null)
            return; 

        if (PlayerList.Count != 0)
        {
            if (Vector2.Distance(PlayerList[0].transform.position, PlayerList[1].transform.position) / 1.8f >= MinimumSize
                && Vector2.Distance(PlayerList[0].transform.position, PlayerList[1].transform.position) / 1.8f <= MaximumSize)
            {
                Camera.main.orthographicSize = Vector2.Distance(PlayerList[0].transform.position, PlayerList[1].transform.position) / 1.8f;
            }

            Camera.main.transform.position = PlayerList[0].transform.position +
                ((PlayerList[1].transform.position - PlayerList[0].transform.position) / 2) + new Vector3(0, 0, -10);
        }

        if (totalWaterInLevel == 0)
        {
            LevelComplete();
        }
    }

    // call when level is completed
    public void LevelComplete()
    {
        PlayerList = null;

        if (PlayerWaterCount > BugWaterCount)
            GameObject.Find("LevelManager").GetComponent<LevelManager>().LevelComplete(level);  


        if (finishPanel != null)
        {
            finishPanel.SetActive(true);
        }

        Time.timeScale = 0;
        StartCoroutine(DelayCompletion());
    }

    //SettingFunctionGroup
    #region SettingFunction
    public void BasicSetting()
    {
        level = SceneManager.GetActiveScene().buildIndex;

        PlayerList = FindObjectsOfType<Player>().ToList();
        
        if (totalWaterInLevel <= 0)
        {
            totalWaterInLevel = GameObject.FindGameObjectsWithTag("Water").Length;
        }
    }

    public void ResetSetting()
    {
        PlayerList = null;

        PlayerWaterCount = 0;
        BugWaterCount = 0;
        totalWaterInLevel = -1;
    }
    #endregion
    
    //InterationFunctionGroup
    #region GameInterationFunction
    public void ChangerType()
    {
        foreach (var Player in PlayerList)
        {
            Player.ChangePlayerType();
        }
    }

    public void AddScore(ObjectType Type)
    {
        if(Type == ObjectType.Player)
            PlayerWaterCount++;

        else
            BugWaterCount++;

        totalWaterInLevel--;
    }
    #endregion

    private IEnumerator DelayCompletion()
    {
        yield return new WaitForSecondsRealtime(waitBeforeReturningToMain);

        Time.timeScale = 1;

        SceneManager.LoadScene(0); // load main menu scene
    }
}
