using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
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
            Destroy(Other.gameObject);
        }
    }
}
