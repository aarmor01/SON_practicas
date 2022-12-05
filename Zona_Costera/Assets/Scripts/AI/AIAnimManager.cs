using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAnimManager : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent rb;
    int speedId = Animator.StringToHash("Speed");

    private void Awake()
    {
        rb = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat(speedId, rb.velocity.magnitude);
    }
}
