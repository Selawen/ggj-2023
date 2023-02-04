using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField]
    float FaintTime = 5.0f;
    [SerializeField]
    bool IsFaint = false;

    [SerializeField]
    BugAI MyAI;

    GameManager manager;

    private void Awake()
    {
        manager = FindObjectOfType(typeof(GameManager)) as GameManager;
        MyAI = this.GetComponent<BugAI>();
    }

    public void GettingWater()
    {
        manager.AddScore(ObjectType.Enemy);
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        //Touch The Water
        if (Other.CompareTag("Water"))
        {
            GettingWater();
            MyAI.OnFaint(FaintTime);

            Destroy(Other.gameObject);
        }
    }
}
