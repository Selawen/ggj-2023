using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNumber
{
    None, Player1, Player2
}

public enum PlayerType
{
    None, Getter, Hunter
}

public class Player : MonoBehaviour
{
    [Header("Player Infos"), SerializeField]
    PlayerNumber PlayerNumber = PlayerNumber.None;

    //Select PlayerTypes
    [SerializeField]
    PlayerType PlayerType = PlayerType.None;

    //PlayerMoveData
    [Header("PlayerMoves"), SerializeField]
    float movespeed;
    public float MoveSpeed
    {
        get { return movespeed; }
        set { movespeed = value; }
    }

    //PlayerInteration
    Action PlayerMove;

    // Start is called before the first frame update
    void Start()
    {
        BasicSetting();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMove != null)
            PlayerMove();
    }

    #region PlayerInfoFunction
    public void BasicSetting()
    {
        //Test
        MoveSpeed = 5.0f;

        if (this.PlayerNumber == PlayerNumber.Player1)
            PlayerMove = MovePlayer1;

        else if (this.PlayerNumber == PlayerNumber.Player2)
            PlayerMove = MovePlayer2;
    }

    //ChangethisPlayerType
    public void ChangePlayerType(PlayerType Type)
    {
        this.PlayerType = Type;
    }
    #endregion

    #region PlayerMoveFunction
    //PlayerMove, Depends on the Number
    private void MovePlayer1()
    {
        int Horizontal = 0;
        int Vertical = 0;

        #region KeyPress
        if (Input.GetKey(KeyCode.W))
            Vertical = 1;
        else if (Input.GetKey(KeyCode.S))
            Vertical = -1;
        else if (Input.GetKey(KeyCode.D))
            Horizontal = 1;
        else if(Input.GetKey(KeyCode.A))
            Horizontal = -1;
        #endregion

        Vector2 MoveDir = new Vector2(Horizontal, Vertical);

        this.gameObject.transform.Translate(MoveDir * MoveSpeed * Time.deltaTime);
    }

    private void MovePlayer2()
    {
        int Horizontal = 0;
        int Vertical = 0;

        #region KeyPress
        if (Input.GetKey(KeyCode.UpArrow))
            Vertical = 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            Vertical = -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            Horizontal = 1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            Horizontal = -1;
        #endregion

        Vector2 MoveDir = new Vector2(Horizontal, Vertical);

        this.gameObject.transform.Translate(MoveDir * MoveSpeed * Time.deltaTime);
    }
    #endregion

    #region PlayerInterationFunction
    //CanChangerPlayerRule
    private void ChangePlayerRule()
    {
        
    }

    private void GetterRule()
    {

    }

    private void HunterRule()
    {

    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        
    }
    #endregion
}
