using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    float movespeed;
    public float MoveSpeed
    {
        get { return movespeed; }
        set { movespeed = value; }
    }

    Coroutine StateCoroutine;

    private float originalSpeed;

    public void OnFaint(float FaintTime)
    {
        if (TryGetComponent(out Animator a)) 
        {
            originalSpeed = a.speed;
            a.speed = 0;
        }

        if (StateCoroutine == null)
            StateCoroutine = StartCoroutine(Faint(FaintTime));
    }

    void OffFaint()
    {
        if (TryGetComponent(out Animator a))  
            a.speed = originalSpeed;

        StopCoroutine(StateCoroutine);
        StateCoroutine = null;
    }

    IEnumerator Faint(float FaintTime)
    {
        yield return null;

        float BaseSpeed = MoveSpeed;
        MoveSpeed = 0;

        yield return new WaitForSeconds(FaintTime);

        MoveSpeed = BaseSpeed;

        OffFaint();
        yield break;
    }
}
