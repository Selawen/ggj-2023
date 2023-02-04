using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RootSegment : MonoBehaviour
{
    [SerializeField] private LineRenderer root;
    [SerializeField] private Gradient rootColour;
    [SerializeField] private float startWidth = 0.5f, endWidth =0.1f;
    [SerializeField] private AnimationCurve growProgress;
    [SerializeField] private float maxCurveAngle = 10;

    private void Awake()
    {
        root = GetComponent<LineRenderer>();
        //root.colorGradient = rootColour;
    }

    public void SetColour(Color start, Color end)
    {
        root.startColor = start;
        root.endColor = end;
    }
    public void Grow(Vector3 from, Vector3 to, float duration, float widthMultiplier)
    {
        root.startWidth = startWidth*widthMultiplier;
        root.endWidth = endWidth*widthMultiplier;

        StartCoroutine(SlowGrowth(from, to, duration));
    }

    private IEnumerator SlowGrowth(Vector3 from, Vector3 to, float duration)
    {
        float timer = 0;
        int fidelity = root.positionCount-1;

        duration /= fidelity;

        root.SetPosition(0, from);

        for (int x = 0; x < fidelity; x++)
        {
            Vector3 point;
            if (x == fidelity - 1)
            {
                point = to;
            }
            else
            {
                Vector3 pointdirection = ((to-root.GetPosition(x)) * x / fidelity);
                
                float shiftAngle = Random.Range(-Mathf.Deg2Rad * maxCurveAngle , Mathf.Deg2Rad * maxCurveAngle);
                
                pointdirection = Vector3.RotateTowards(pointdirection, shiftAngle > 0 ? Vector3.right : Vector3.left, Mathf.Abs(shiftAngle), 0);
                
                point = root.GetPosition(x) + pointdirection;
            }


            while (timer < duration)
            {
                Vector3 target = Vector3.Lerp(root.GetPosition(x), point, growProgress.Evaluate(timer / duration));
                for (int i = x+1; i <= fidelity; i++)
                {
                    root.SetPosition(i, target);
                }
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0;
        }

        root.SetPosition(fidelity, to);
    }
}
