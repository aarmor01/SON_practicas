using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepOnCollision : MonoBehaviour
{
    [SerializeField] float restoreTime = 5.0f;

    void RestoreRutine()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Invoke(nameof(RestoreRutine), restoreTime);
        gameObject.SetActive(false);
    }
}
