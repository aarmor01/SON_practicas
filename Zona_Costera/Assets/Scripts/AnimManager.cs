using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private CharacterController rb;
    int speedId = Animator.StringToHash("Speed");

    private void Awake()
    {
        rb = GetComponent<CharacterController>();
    }

    void Update()
    {
        animator.SetFloat(speedId, rb.velocity.magnitude);
    }
}
