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

public class GameManager : SingletonMono<GameManager>
{
    [SerializeField] private int level;

    public int PlayerWaterCount = 0;
    public int BugWaterCount = 0;

    public List<Player> PlayerList = new List<Player>();

    public float MaximumSize;
    public float MinimumSize;

    // Start is called before the first frame update
    void Start()
    {
        //Test
        BasicSetting();
    }

    private void FixedUpdate()
    {
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
    }

    // call when level is completed
    public void LevelComplete()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().LevelComplete(level);

        //maybe move this later
        SceneManager.LoadScene(0); // load main menu scene
    }

    //SettingFunctionGroup
    #region SettingFunction
    public void BasicSetting()
    {
        PlayerList = FindObjectsOfType<Player>().ToList();
    }

    public void ResetSetting()
    {
        PlayerList = null;
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
    }
    #endregion
}
