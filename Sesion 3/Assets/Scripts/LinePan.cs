using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePan : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] Direction direction;
    [SerializeField] Color color;
    [SerializeField] float duration;
    float time = 0;
    Vector3 center = Vector3.zero;
    Vector3 startPosition;
    Vector3 endPosition;

    private void Awake()
    {
        center = transform.position;
        startPosition = center + GetVector() * offset / 2;
        endPosition = center - GetVector() * offset / 2;
        time = duration / 2;
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

    private void Update()
    {
        time += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.Max(time / duration, 0));

        if (time >= duration)
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

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            center = transform.position;
            startPosition = center + GetVector() * offset / 2;
            endPosition = center - GetVector() * offset / 2;
        }

        Gizmos.color = color;
        
        Gizmos.DrawLine(center, startPosition);
        Gizmos.DrawLine(center, endPosition);
    }
}
