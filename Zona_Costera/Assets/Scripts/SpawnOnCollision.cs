using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnCollision : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] bool spawnOnTrigger = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!spawnOnTrigger)
            Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (spawnOnTrigger)
            Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
