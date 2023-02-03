using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    None, Getter, Hunter
}

public class Player : MonoBehaviour
{
    [Header("Player Infos") ,SerializeField]
    //Select PlayerTypes
    PlayerType PlayerType = PlayerType.None;

    //PlayerMoveData
    [Header("PlayerMoves"), SerializeField]
    float movespeed;
    public float MoveSpeed
    {
        get { return movespeed; }
        set { movespeed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    #region PlayerInfoFunction
    //ChangethisPlayerType
    public void ChangePlayerRole(PlayerType Type)
    {
        this.PlayerType = Type;
    }
    #endregion

    #region PlayerMoveFunction
    //CanPlayerMove
    private void Move()
    {
        int Horizontal = (int)Input.GetAxisRaw("Horizontal");
        int Vertical = (int)Input.GetAxisRaw("Vertical");

        Vector2 MoveDir = new Vector2(Horizontal, Vertical);

        //block diagonal movement
        if (MoveDir.x != 0 && MoveDir.y != 0)
            MoveDir = Vector2.zero;

        this.gameObject.transform.Translate(MoveDir * MoveSpeed * Time.deltaTime);
    }
    #endregion
}
