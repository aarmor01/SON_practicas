using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPassCheck : MonoBehaviour
{
    [SerializeField] LayerMask detectorLayers;
    [SerializeField] GameObject character;
    [SerializeField] Vector3 offset;

    const float dist = 40f;
    const string parameter = "Surround";

    void Update()
    {
        RaycastHit ray;
        Vector3 diff = ((character.transform.position + offset) - transform.position);
        bool coll = Physics.Raycast(transform.position, diff.normalized, out ray, diff.magnitude, detectorLayers);
        if (coll)
        {
            var collision = ray.collider;
            if (collision != null)
            {
                Debug.Log(collision.gameObject.name);
                if (collision.isTrigger)
                    RuntimeManager.StudioSystem.setParameterByName(parameter, 0.5f);
                else
                    RuntimeManager.StudioSystem.setParameterByName(parameter, 1);
            }
            else
                RuntimeManager.StudioSystem.setParameterByName(parameter, 0);

            float val;
            RuntimeManager.StudioSystem.getParameterByName(parameter, out val);
            Debug.Log("Low pass: " + val);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, ((character.transform.position + offset) - transform.position).normalized * dist);
    }
}
