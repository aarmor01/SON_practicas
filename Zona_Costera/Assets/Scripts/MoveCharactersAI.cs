using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCharactersAI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] wayPoints;
    int currentIndex = 0;

    private int RandomIndex => Random.Range(0, wayPoints.Length);

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentIndex = RandomIndex;
        agent.destination = wayPoints[currentIndex].position;
    }

    void Update()
    {
        if (agent.remainingDistance <= 0.1f)
        {
            int aux;
            do
            {
                aux = RandomIndex;
            } while (aux == currentIndex);
            currentIndex = aux;
            agent.destination = wayPoints[currentIndex].position;
        }
    }
}
