using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private List<GameObject> _walkPoints;
    private NavMeshAgent _agent;

    // Update is called once per frame
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        _agent.SetDestination(_walkPoints[0].transform.position);
    }
}
