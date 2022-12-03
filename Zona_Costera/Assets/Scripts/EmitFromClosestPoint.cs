using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.PlayerLoop;

public class EmitFromClosestPoint : MonoBehaviour
{
    [SerializeField] Transform listener;
    [SerializeField] Transform emisor;
    [SerializeField] Transform parent;
    [SerializeField] Transform[] points;

    [SerializeField]private float updateTime = 0.5f;
    Vector3 closestPoint;

    private void Start() => StartCoroutine(UpdateEmisor());

    IEnumerator UpdateEmisor()
    {
        WaitForSeconds wait = new WaitForSeconds(updateTime);
        while (true)
        {
            closestPoint =  getClosestPoint();
            yield return wait;
        }
    }

    private void Update()
    {
        emisor.position = Vector3.Lerp(emisor.position, closestPoint, Time.deltaTime);
    }

    [ContextMenu("Fill")]
    void fillPointsFromParent()
    {
        points = parent.GetComponentsInChildren<Transform>().SkipWhile(x => x == parent).ToArray();
        points = points.Concat(new Transform[] { points[0] }).ToArray();
    }

    Vector3 getClosestPointOnLine(Vector3 a, Vector3 b, Vector3 p)
    {
        Vector3 ap = p - a;
        Vector3 ab = b - a;
        float magnitudeAB = ab.magnitude;
        ab.Normalize();
        float distance = Vector3.Dot(ap, ab);
        if (distance < 0)
        {
            return a;
        }
        if (distance > magnitudeAB)
        {
            return b;
        }
        return a + ab * distance;
    }

    Vector3 getClosestPoint()
    {
        Vector3 point = Vector3.down;

        // First, we find the closest point on each line.
        var closestPoints = points.Zip(points.Skip(1), (a, b) => getClosestPointOnLine(a.position, b.position, listener.position));

        // Then, we find the closest point to the listener from those points.
        point = closestPoints.Aggregate((a, b) => Vector3.Distance(a, listener.position) < Vector3.Distance(b, listener.position) ? a : b);

        // Finally, we return the closest point.
        return point;
    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;
        Gizmos.color = color;

        for (int i = 0; i < points.Length; i++)
        {
            int currentPoint = i;
            int nextPoint = (i + 1) % points.Length;
            Gizmos.DrawLine(points[currentPoint].position, points[nextPoint].position);
            color = Color.HSVToRGB(360 / points.Length, 85, 100);
        }

        emisor.position = getClosestPoint();
    }
}
