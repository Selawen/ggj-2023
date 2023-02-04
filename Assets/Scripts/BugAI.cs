using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAI : MonoBehaviour
{
    [SerializeField] private int Speed = 1;

    private static Vector2[] directions = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };

    private Vector2 lastDirection = Vector2.up;

    [SerializeField] private LayerMask wallMask;
     
    private void Start()
    {
        StartCoroutine(TakeStep());
    }

    IEnumerator TakeStep()
    {
        //yield return new WaitForSeconds(stepTime);

        List<int> wallDirections = new List<int>();
        List<int> openDirections = new List<int>();

        for (int x = 0; x < 4; x++)
        {
            //Debug.Log($"trying {x}");
            RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, directions[x], 1f, wallMask);
            if (hitinfo)
            {
                //Debug.Log($"wall at direction {x}, {hitinfo.collider.name}");
                wallDirections.Add(x);
                continue;
            }

            openDirections.Add(x);
        }

        // keep going straight half the time
        if (Random.Range(0, 2.0f) > 1 && !Physics2D.Raycast(transform.position, lastDirection, 1f, wallMask))
        { }
        else
        {
            if (openDirections.Count == 1)
                lastDirection = directions[openDirections[0]];
            else
            {
                int direction;

                do
                {
                    direction = Random.Range(0, openDirections.Count);
                } while (!(Random.Range(0, 2.0f) > 1 && (directions[openDirections[direction]] != lastDirection * -1)));

                lastDirection = directions[openDirections[direction]];
            }
        }

        //transform.Translate(new Vector3(lastDirection.x, lastDirection.y, 0));

        //StartCoroutine(TakeStep());

        Debug.Log(lastDirection);
        StartCoroutine(MoveToGoal(lastDirection));

        yield break;
    }

    IEnumerator MoveToGoal(Vector3 GoalPos)
    {
        yield return null;

        GoalPos += this.transform.position;

        while (true)
        {
            yield return null;
            this.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, GoalPos, Speed * Time.deltaTime);

            if(this.transform.position == GoalPos)
            {
                Debug.Log("Goal");
                break;
            }
        }

        StartCoroutine(TakeStep());
        yield break;
    }

    public IEnumerator Faint(float FaintTime)
    {
        yield return null;

        int BaseSpeed = Speed;
        Speed = 0;

        yield return new WaitForSeconds(FaintTime);

        Speed = BaseSpeed;
        yield break;
    }

    private void OnDrawGizmos()
    {
        if (Physics2D.Raycast(transform.position, lastDirection, 1f, wallMask))
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(lastDirection.x, lastDirection.y, 0));
    }
}
