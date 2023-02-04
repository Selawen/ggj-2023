using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

public enum PlayerNumber
{
    None, Player1, Player2
}

public enum PlayerType
{
    None, Getter, Hunter
}

public class Player : Unit
{
    [SerializeField] private LayerMask wallMask;

    [Header("Player Infos"), SerializeField]
    PlayerNumber PlayerNumber = PlayerNumber.None;
    [SerializeField]
    float FaintTime = 5.0f;
    [SerializeField]
    bool IsFaint = false;

    //Select PlayerTypes
    [SerializeField]
    PlayerType PlayerType = PlayerType.None;

    //PlayerMoveData
    [Header("PlayerMoves"), SerializeField]
    Vector2 MoveDir;

    //PlayerMoveMethod
    Action PlayerMove;

    [Header("PlayerAnimation"), SerializeField]
    Animator PlayerAnimator = null;
    [SerializeField]
    List<RuntimeAnimatorController> Animators;

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

        PlayerAnimator = this.gameObject.GetComponent<Animator>();
    }

    //ChangethisPlayerType
    public void ChangePlayerType()
    {
        if (this.PlayerType == PlayerType.Getter)
        {
            PlayerType = PlayerType.Hunter;
        }

        else
        {
            PlayerType = PlayerType.Getter;
        }
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
        {
            Vertical = 1;
            PlayerAnimator.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vertical = -1;
            PlayerAnimator.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Horizontal = 1;

            this.GetComponent<SpriteRenderer>().flipX = false;
            PlayerAnimator.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Horizontal = -1;

            this.GetComponent<SpriteRenderer>().flipX = true;
            PlayerAnimator.SetBool("Walk", true);
        }

        else
        {
            PlayerAnimator.SetBool("Walk", false);
        }
        #endregion

        MoveDir = new Vector2(Horizontal, Vertical);

        //Collision test with wall
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, MoveDir, 0.8f, wallMask);
        if (hitinfo)
        {
            Debug.Log($"Wall!");
        }

        else
        {

            this.gameObject.transform.Translate(MoveDir * MoveSpeed * Time.deltaTime);
        }
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

        MoveDir = new Vector2(Horizontal, Vertical);

        //Collision test with wall
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, MoveDir, 0.8f, wallMask);
        if (hitinfo)
        {
            Debug.Log($"Wall!"); 
        }

        else
        {
            this.gameObject.transform.Translate(MoveDir * MoveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (Physics2D.Raycast(transform.position, MoveDir, 0.5f, wallMask))
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawLine(transform.position, transform.position + (new Vector3(MoveDir.x, MoveDir.y, 0) * 0.6f));
    }
    #endregion

    #region PlayerInterationFunction
    private void HittingEnemy(GameObject OtherEnemy)
    {
        OtherEnemy.GetComponent<BugAI>().OnFaint(4.0f);
    }

    private void GettingWater(GameObject OtherWater)
    {
        //TODO:Make a Getting Water

        GameManager.In.AddScore(ObjectType.Player);
        Destroy(OtherWater);
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        //Touch The ChangeButton
        if (Other.CompareTag("ChangeButton"))
        {
            GameManager.In.ChangerType();
        }

        //Touch The Enemy
        if (Other.CompareTag("Enemy") && this.PlayerType == PlayerType.Hunter)
        {
            HittingEnemy(Other.gameObject);
        }

        else if(Other.CompareTag("Enemy") && this.PlayerType == PlayerType.Getter)
        {
            OnFaint(3.0f);
        }    

        //Touch The Water
        if (Other.CompareTag("Water") && this.PlayerType == PlayerType.Getter)
        {
            GettingWater(Other.gameObject);
        }
    }
    #endregion
}
