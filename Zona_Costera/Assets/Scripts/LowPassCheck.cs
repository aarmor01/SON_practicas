using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] LayerMask detectorLayers;
    [SerializeField] Vector3 offset;

    void Update()
    {
        //RaycastHit ray;
        //var r = Physics.Raycast(transform.position, Vector3.down + offset, out ray, 40, detectorLayers);
        //if (r)
        //{
        //    var collision = ray.collider.gameObject;
        //    if (collision && collision != detected)
        //    {
        //        detected = collision;
        //        surface = detected.GetComponent<SurfaceType>();
        //        //if (surface)
        //        //    Debug.Log("Detected " + detected.name + " with type: " + surface.SurfaceName);
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position + offset, Vector3.down);
    }
}
