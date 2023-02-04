using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ObjectType
{
    None, Player, Enemy
}

public class GameManager : MonoBehaviour
{
    public int PlayerWaterCount = 0;
    public int BugWaterCount = 0;

    public List<Player> PlayerList = new List<Player>();
    public static GameManager Ins;

    // Start is called before the first frame update
    void Start()
    {
        //Test
        Ins = this;

        //Test
        BasicSetting();
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
