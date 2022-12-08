using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class ReverbCheck : MonoBehaviour
{
    const string parameter = "Enclosure";

    [SerializeField] bool invertDirection;
    [SerializeField] float transitionSpeed = 4;
    [SerializeField][Range(0,1)] float maxEnclosure = 1;
    [SerializeField][Range(0,1)] float minEnclosure = 0;
    float enclosure = 0;
    bool isInside = false;

    // Update is called once per frame
    void Update()
    {
        enclosure = Mathf.Lerp(enclosure, isInside ? maxEnclosure : minEnclosure, Time.deltaTime * transitionSpeed);
        RuntimeManager.StudioSystem.setParameterByName(parameter, enclosure);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (invertDirection)
            Gizmos.DrawRay(transform.position, (transform.forward) * 5);
        else
            Gizmos.DrawRay(transform.position, (transform.forward * -1) * 5);
    }

    private void OnTriggerExit(Collider other)
    {
        Vector3 outDir = invertDirection ? transform.forward : transform.forward * -1;
        Transform target = other.transform;
        Vector3 dir = target.position - transform.position;

        // Check if the target is aligned with outDir to know if they entered or exited
        isInside = Mathf.Sign(Vector3.Dot(dir, outDir)) < 0;
        GetComponentInParent<ReverbCheckManager>().EnableMe(this);
        //Debug.Log(isInside ? "Inside" : "Out");
    }
}
