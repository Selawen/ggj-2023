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

    public float MaximumSize;
    public float MinimumSize;

    // Start is called before the first frame update
    void Start()
    {
        //Test
        Ins = this;

        //Test
        BasicSetting();
    }

    private void FixedUpdate()
    {
        //Debug.Log(Vector2.Distance(PlayerList[0].transform.position, PlayerList[1].transform.position));

        if (Vector2.Distance(PlayerList[0].transform.position, PlayerList[1].transform.position) / 1.7f >= MinimumSize 
            && Vector2.Distance(PlayerList[0].transform.position, PlayerList[1].transform.position) / 1.7f <= MaximumSize)
        {
            Camera.main.orthographicSize = Vector2.Distance(PlayerList[0].transform.position, PlayerList[1].transform.position) / 1.7f;

            Debug.Log((PlayerList[1].transform.position - PlayerList[0].transform.position) / 2);
            Camera.main.transform.position = PlayerList[0].transform.position +
                ((PlayerList[1].transform.position - PlayerList[0].transform.position) / 2) + new Vector3(0, 0, -10);
        }
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
