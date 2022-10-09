using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Sphere : MonoBehaviour
{
    [SerializeField] float radius, time, vel;

    Vector3 dst;

    private void Awake()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (dst - transform.position).normalized;
        transform.Translate(dir * vel);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Vector3.zero, radius);
    }

    IEnumerator Move()
    {
        while (true)
        {
            dst = Random.insideUnitSphere * radius;
            yield return new WaitForSeconds(time);
        }
    }
}