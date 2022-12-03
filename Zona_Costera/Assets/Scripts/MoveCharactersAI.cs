using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCharactersAI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]Transform[] wayPoints;

    private int RandomIndex => Random.Range(0, wayPoints.Length);

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = wayPoints[RandomIndex].position;
    }

    void Update()
    {
        if (agent.remainingDistance <= 0.1f)
            agent.destination = wayPoints[RandomIndex].position;
    }
}
