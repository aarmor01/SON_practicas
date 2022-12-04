using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]GameObject[] slashes;

    public void OnAttack()
    {
        GameObject.Instantiate(slashes[Random.Range(0, slashes.Length)], transform.position, transform.rotation);
    }
}
