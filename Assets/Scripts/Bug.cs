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

    private void Awake()
    {
        MyAI = this.GetComponent<BugAI>();
    }

    public void GettingWater()
    {
        GameManager.In.AddScore(ObjectType.Enemy);
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
