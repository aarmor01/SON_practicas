using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnCollision : MonoBehaviour
{
    [SerializeField]GameObject prefab;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
