using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]GameObject[] slashes;

    public void OnAttack()
    {
        var go = GameObject.Instantiate(slashes[Random.Range(0, slashes.Length)], transform.position, transform.rotation);
        go.transform.SetParent(transform);
    }
}
