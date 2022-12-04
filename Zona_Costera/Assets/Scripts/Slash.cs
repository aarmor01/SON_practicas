using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] private Transform _start, _center, _end;
    [SerializeField] private int _count = 15;
    [SerializeField] float duration;
    public GameObject followObject;
    public AnimationCurve curve;
    float time = 0.000001f;

    private void Update()
    {
        followObject.SetActive(true);
        //followObject.GetComponent<MeshRenderer>().enabled = true;
        time += Time.deltaTime;
        followObject.transform.position = Slerp(_start.position, _end.position, _center.position, curve.Evaluate(time / duration));
        followObject.transform.LookAt(this.transform);

        if (time >= duration)
            Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 5);

        if (_start == null || _center == null || _end == null)
        {
            return;
        }

        followObject.transform.LookAt(this.transform);

        foreach (var point in EvaluateSlerpPoints(_start.position, _end.position, _center.position, _count))
        {
            Gizmos.DrawSphere(point, 0.1f);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_center.position, 0.2f);
    }

    IEnumerable<Vector3> EvaluateSlerpPoints(Vector3 start, Vector3 end, Vector3 center, int count = 10)
    {
        var f = 1f / count;

        for (var i = 0f; i < 1 + f; i += f)
        {
            yield return Slerp(start, end, center, i);
        }
    }

    Vector3 Slerp(Vector3 start, Vector3 end, Vector3 center, float t)
    {
        var startRelativeCenter = start - center;
        var endRelativeCenter = end - center;

        return Vector3.Slerp(startRelativeCenter, endRelativeCenter, t) + center;
    }
}
