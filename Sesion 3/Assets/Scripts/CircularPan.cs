using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPan : MonoBehaviour
{
    [SerializeField] Direction direction;
    [SerializeField] float duration;
    float time = 0;

    [Header("Debug")]
    [SerializeField] float offset = 5;
    [SerializeField] int circleCount = 10;
    [Space()]
    [SerializeField] Color color;
    [Range(0.1f, 1)][SerializeField] float debugRadius = 2;

    Vector3 center = Vector3.zero;
    Vector3 startPosition;
    Vector3 endPosition;

    private void Awake()
    {
        center = transform.position;
        startPosition = transform.position + GetVector() * offset;
        endPosition = transform.position + GetVector() * -1 * offset;
        time = duration / 2;
    }

    private void Update()
    {
        time += Time.deltaTime;
        transform.position = Utils.Slerp(startPosition, endPosition, center, Mathf.Max(time / duration, 0));

        if(time >= duration)
        {
            time -= duration;
            Swap();
        }
    }

    void Swap()
    {
        Vector3 tmp = startPosition;
        startPosition = endPosition;
        endPosition = tmp;
    }

    Vector3 GetVector()
    {
        Vector3 v;
        if (direction == Direction.Horizontal)
            v = transform.right;
        else if (direction == Direction.Vertical)
            v = transform.up;
        else
            v = transform.forward;

        return v;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            center = transform.position;
            startPosition = center + GetVector() * offset;
            endPosition = center + GetVector() * -1 * offset;
        }

        Gizmos.color = color;
        foreach (var point in Utils.EvaluateSlerpPoints(startPosition, endPosition, center, circleCount))
        {
            Gizmos.DrawSphere(point, debugRadius);
        }
    }
}
