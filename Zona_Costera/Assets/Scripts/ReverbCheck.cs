using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class ReverbCheck : MonoBehaviour
{
    const string parameter = "Enclosure";

    [SerializeField] LayerMask detectorLayers;
    [SerializeField] Vector3 offset;

    Vector3[] directions = { Vector3.up, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    // Update is called once per frame
    void Update()
    {
        float enclosure = 0f;

        RaycastHit ray;
        for (int i = 0; i < directions.Length; i++)
        {
            bool coll = Physics.Raycast(transform.position + offset, transform.InverseTransformDirection(directions[i]), out ray, 40, detectorLayers);
            if (coll)
            {
                enclosure += 1f / directions.Length;
                Debug.Log("Collision in " + directions[i] + " direction");
            }
        }

        Debug.Log("Enclosure: " + enclosure);
        RuntimeManager.StudioSystem.setParameterByName(parameter, enclosure);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < directions.Length; i++)
            Gizmos.DrawRay(transform.position + offset, transform.InverseTransformDirection(directions[i]));
    }
}
