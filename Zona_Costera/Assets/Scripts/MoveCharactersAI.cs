using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MoveCharactersAI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform wayPointParents;
    [SerializeField] List<Transform> wayPoints;
    int currentIndex = 0;

    [ContextMenu("Generate")]
    private void generatePointsFromParent()
    {
        if (!wayPointParents)
            return;

        wayPoints = wayPointParents.GetComponentsInChildren<Transform>().SkipWhile(x => x == wayPointParents).ToList();
    }

    private int RandomIndex => Random.Range(0, wayPoints.Count);

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentIndex = RandomIndex;
        agent.destination = wayPoints[currentIndex].position;
    }

    bool reached()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    return true;
                }
            }
        }

        return false;
    }

    void Update()
    {
        int attemps = 1000;
        if (reached())
        {
            int aux;
            do
            {
                aux = RandomIndex;
                if (attemps <= 0)
                    break;

                attemps--;
            } while (aux == currentIndex);
            currentIndex = aux;
            agent.destination = wayPoints[currentIndex].position;
        }
    }
}
