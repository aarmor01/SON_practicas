using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnDrown : MonoBehaviour
{
    private FirstPersonController controller;
    [SerializeField] private float updateTime = 2.0f;
    Vector3 lastSafeGroundedPos;

    private void Awake()
    {
        controller = GetComponent<FirstPersonController>();
        lastSafeGroundedPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdatePosition());
    }

    IEnumerator UpdatePosition()
    {
        while (true)
        {
            if (controller.Grounded)
                lastSafeGroundedPos = transform.position;

            yield return new WaitForSeconds(updateTime);
        }
    }

    private void Respawn() => transform.position = this.lastSafeGroundedPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
            Respawn();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Finish"))
            Respawn();
    }
}
