using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]GameObject[] slashes;
    [SerializeField] float cooldown = 0.1f;
    bool canAttack = true;

    IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    public void OnAttack()
    {
        if (!canAttack)
            return;

        StartCoroutine(Cooldown());
        var go = GameObject.Instantiate(slashes[Random.Range(0, slashes.Length)], transform.position, transform.rotation);
        go.transform.SetParent(transform);
    }
}
